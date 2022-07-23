using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject coinText;
    [SerializeField] private GameObject lvlText;
    [SerializeField] private GameObject expText;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject pauseRecordText;
    [SerializeField] private GameObject lostRecordText;
    [SerializeField] private GameObject lostDistanceText;

    [SerializeField] private GameObject airBonusText;
    [SerializeField] private GameObject flipBonusText;

    [SerializeField] private Image fuelBar;
    [SerializeField] private TextMeshProUGUI fuelText;

    private int airBonusCount = 0;
    private int highScore = 0;

    void Start()
    {
        airBonusText.SetActive(false);
        flipBonusText.SetActive(false);

        coinText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("coins").ToString();
        scoreText.GetComponent<TextMeshProUGUI>().text = ("0 m");

        lvlText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("lvl").ToString();
        expText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("exp").ToString() + "/" + PlayerPrefs.GetInt("nextlvlexp");
    }
    private void LateUpdate()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = ((int)Camera.main.transform.position.x).ToString() + " m";
        if (Camera.main.transform.position.x > highScore)
            highScore = (int)Camera.main.transform.position.x;
    }

    public void UpdateUI()
    {
        coinText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("coins").ToString();
    }

    public void UpdateExp()
    {
        expText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("exp").ToString() + "/" + PlayerPrefs.GetInt("nextlvlexp");

        if (PlayerPrefs.GetInt("exp") > PlayerPrefs.GetInt("nextlvlexp"))
        {
            if (PlayerPrefs.GetInt("lvl") >= 100)
            {
                expText.GetComponent<TextMeshProUGUI>().text = "max level";
                lvlText.GetComponent<TextMeshProUGUI>().text = "100";

                PlayerPrefs.SetInt("exp", 0);
            }
            else
            {
                PlayerPrefs.SetInt("lvl", PlayerPrefs.GetInt("lvl") + 1);
                PlayerPrefs.SetInt("exp", PlayerPrefs.GetInt("exp") - PlayerPrefs.GetInt("nextlvlexp"));
                PlayerPrefs.SetInt("nextlvlexp", (int)(PlayerPrefs.GetInt("nextlvlexp") * 1.1f));

                lvlText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("lvl").ToString();
                expText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("exp").ToString() + "/" + PlayerPrefs.GetInt("nextlvlexp");
            }
        }
    }

    // Fuel
    public void FuelBar()
    {
        if (PlayerPrefs.GetFloat("currentFuel") == 0)
            fuelBar.fillAmount = 0;
        else
            fuelBar.fillAmount = PlayerPrefs.GetFloat("currentFuel") / PlayerPrefs.GetInt("maxFuel");
    }

    public void FuelDistance()
    {
        if (PlayerPrefs.GetInt("distanceFuel") == 1000)
            fuelText.text = "";
        else
            fuelText.text = PlayerPrefs.GetInt("distanceFuel").ToString() + " m";
    }

    // Air bonus coins.
    float airBonusCoin = 1;

    public void AirText()
    {
        airBonusText.SetActive(true);
        airBonusCount += 1 * (int)airBonusCoin;

        airBonusText.GetComponent<TextMeshProUGUI>().text = "Air bonus\ncoin + " + airBonusCount;

        StartCoroutine("AirTextAnim");
        airBonusCoin += .5f;
    }
    private IEnumerator AirTextAnim()
    {
        airBonusText.transform.localScale = Vector3.Lerp(airBonusText.transform.localScale, new Vector3(1.5f, 1.5f, 1.5f), 150f * Time.deltaTime);
        yield return new WaitForSeconds(.1f);
        airBonusText.transform.localScale = Vector3.Lerp(airBonusText.transform.localScale, new Vector3(1f, 1f, 1f), 100f * Time.deltaTime);
    }

    public void AirBonusReset()
    {
        airBonusText.transform.localScale = new Vector3(1, 1, 1);
        airBonusText.SetActive(false);

        b_flipCount = 1;
        f_flipCount = 1;
        airBonusCoin = 1;
        airBonusCount = 0;
    }

    // Flip bonus coins.

    float b_flipCount = 1;
    float f_flipCount = 1;

    public void Frontflip()
    {
        flipBonusText.SetActive(true);

        if (f_flipCount == 1)
            flipBonusText.GetComponent<TextMeshProUGUI>().text = "Frontflip\n+ " + (int)(5 * f_flipCount);
        if (f_flipCount == 1.5f)
            flipBonusText.GetComponent<TextMeshProUGUI>().text = "Double Frontflip\n+ " + (int)(5 * f_flipCount);
        if (f_flipCount == 2)
            flipBonusText.GetComponent<TextMeshProUGUI>().text = "Triple Frontflip\n+ " + (int)(5 * f_flipCount);
        if (f_flipCount > 2)
            flipBonusText.GetComponent<TextMeshProUGUI>().text = "Insane Frontflip\n+ " + (int)(5 * f_flipCount);

        f_flipCount += .5f;
        b_flipCount = 1;

        StartCoroutine("FlipTextAnim");
    }
    public void Backflip()
    {
        flipBonusText.SetActive(true);

        if (b_flipCount == 1)
            flipBonusText.GetComponent<TextMeshProUGUI>().text = "Backflip\n+ " + (int)(5 * b_flipCount);
        if (b_flipCount == 1.5f)
            flipBonusText.GetComponent<TextMeshProUGUI>().text = "Double Backflip\n+ " + (int)(5 * b_flipCount);
        if (b_flipCount == 2)
            flipBonusText.GetComponent<TextMeshProUGUI>().text = "Triple Backflip\n+ " + (int)(5 * b_flipCount);
        if (b_flipCount > 2)
            flipBonusText.GetComponent<TextMeshProUGUI>().text = "Insane Backflip\n+ " + (int)(5 * b_flipCount);

        b_flipCount += .5f;
        f_flipCount = 1;

        StartCoroutine("FlipTextAnim");
    }
    private IEnumerator FlipTextAnim()
    {
        flipBonusText.transform.localScale = new Vector3(1, 1, 1);
        flipBonusText.GetComponent<TextMeshProUGUI>().color = new Color(flipBonusText.GetComponent<TextMeshProUGUI>().color.r,
                flipBonusText.GetComponent<TextMeshProUGUI>().color.g, flipBonusText.GetComponent<TextMeshProUGUI>().color.b, 1f);

        flipBonusText.transform.localScale = Vector3.Lerp(flipBonusText.transform.localScale, new Vector3(1.5f, 1.5f, 1.5f), 300f * Time.deltaTime);
        yield return new WaitForSeconds(.1f);
        flipBonusText.transform.localScale = Vector3.Lerp(flipBonusText.transform.localScale, new Vector3(1f, 1f, 1f), 100f * Time.deltaTime);
        yield return new WaitForSeconds(.1f);

        for (float i = 1; i > 0; i -= 0.1f)
        {
            flipBonusText.GetComponent<TextMeshProUGUI>().color = new Color(flipBonusText.GetComponent<TextMeshProUGUI>().color.r,
                flipBonusText.GetComponent<TextMeshProUGUI>().color.g, flipBonusText.GetComponent<TextMeshProUGUI>().color.b, i);
            yield return new WaitForSeconds(.05f);
        }

        flipBonusText.SetActive(false);
    }

    // Pause menu.
    public void Resume()
    {
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        
        if (highScore > PlayerPrefs.GetInt("stage" + PlayerPrefs.GetInt("currentStageId") + "score"))
        {
            PlayerPrefs.SetInt("stage" + PlayerPrefs.GetInt("currentStageId") + "score", highScore);
        }

        pauseRecordText.GetComponent<TextMeshProUGUI>().text = "record: " + PlayerPrefs.GetInt("stage" + PlayerPrefs.GetInt("currentStageId") + "score").ToString() + " m";
        lostRecordText.GetComponent<TextMeshProUGUI>().text = "record: " + PlayerPrefs.GetInt("stage" + PlayerPrefs.GetInt("currentStageId") + "score").ToString() + " m";
        lostDistanceText.GetComponent<TextMeshProUGUI>().text = "distance: " + highScore + " m";
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage" + PlayerPrefs.GetInt("currentStageId"));
    }
    public void ExitMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
