using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class VehicleHolder : MonoBehaviour
{
    [Header("Exp")]
    [SerializeField] private GameObject lvlText;
    [SerializeField] private GameObject expText;

    [Header("Coins")]
    [SerializeField] private GameObject coinsText;

    [Header("Vehicle Shop")]
    [SerializeField] private int chosenVehicle;
    [SerializeField] private int currentVehicle;

    [SerializeField] private Button prevButtonVehicle;
    [SerializeField] private Button nextButtonVehicle;
    [SerializeField] private GameObject vehicleText;

    [SerializeField] private Button buyButtonVehicle;
    [SerializeField] private Button chooseButtonVehicle;
    [SerializeField] private TextMeshProUGUI vehiclePriceText;
    private int[] vehicleUnlocked = new int[3] { 1, 0, 0};
    [SerializeField] private int[] vehiclePrices;
    [SerializeField] private int[] vehicleLvl;

    [Header("Stage Shop")]
    [SerializeField] private GameObject stages;
    [SerializeField] private int chosenStage;
    [SerializeField] private int currentStage;

    [SerializeField] private Button prevButtonStage;
    [SerializeField] private Button nextButtonStage;
    [SerializeField] private GameObject stageText;
    [SerializeField] private GameObject stageRecordText;

    [SerializeField] private Button buyButtonStage;
    [SerializeField] private Button chooseButtonStage;
    [SerializeField] private TextMeshProUGUI stagePriceText;
    private int[] stageUnlocked = new int[3] { 1, 0, 0 };
    [SerializeField] private int[] stagePrices;
    [SerializeField] private int[] stageLvl;

    private void Start()
    {
        PlayerPrefs.DeleteAll();

        //PLAYERPREFS VARIABLES.
        #region
        // COINS.
        if (PlayerPrefs.HasKey("coins") == false)
            PlayerPrefs.SetInt("coins", 0);

        // EXP.
        if (PlayerPrefs.HasKey("exp") == false)
            PlayerPrefs.SetInt("exp", 0);

        if (PlayerPrefs.HasKey("lvl") == false)
            PlayerPrefs.SetInt("lvl", 1);

        if (PlayerPrefs.HasKey("nextlvlexp") == false)
            PlayerPrefs.SetInt("nextlvlexp", 500);

        // STAGES.
        // Current stage.
        if (PlayerPrefs.HasKey("currentStageId") == false)
            PlayerPrefs.SetInt("currentStageId", 0);

        // Unlockes stage.
        if (PlayerPrefs.HasKey("stageUnlocked0") == false)
            PlayerPrefs.SetInt("stageUnlocked0", 1);
        if (PlayerPrefs.HasKey("stageUnlocked1") == false)
            PlayerPrefs.SetInt("stageUnlocked1", 0);

        // Stages highscores.
        if (PlayerPrefs.HasKey("stage0score") == false)
            PlayerPrefs.SetInt("stage0score", 0);
        if (PlayerPrefs.HasKey("stage1score") == false)
            PlayerPrefs.SetInt("stage1score", 0);

        // VEHICLES.
        // Current vehicle.
        if (PlayerPrefs.HasKey("currentVehicleId") == false)
            PlayerPrefs.SetInt("currentVehicleId", 0);

        // Unlocked vehicles.
        if (PlayerPrefs.HasKey("vehicleUnlocked0") == false)
            PlayerPrefs.SetInt("vehicleUnlocked0", 1);
        if (PlayerPrefs.HasKey("vehicleUnlocked1") == false)
            PlayerPrefs.SetInt("vehicleUnlocked1", 0);
        if (PlayerPrefs.HasKey("vehicleUnlocked2") == false)
            PlayerPrefs.SetInt("vehicleUnlocked2", 0);

        // Vehicles stats.
        // Vehicle 0 stats (zhiga).
        if (PlayerPrefs.HasKey("vehicle0name") == false)
            PlayerPrefs.SetString("vehicle0name", "zhiga");
        if (PlayerPrefs.HasKey("vehicle0speed") == false)
            PlayerPrefs.SetInt("vehicle0speed", 1800);
        if (PlayerPrefs.HasKey("vehicle0suspension") == false)
            PlayerPrefs.SetFloat("vehicle0suspension", 2.5f);
        if (PlayerPrefs.HasKey("vehicle0friction") == false)
            PlayerPrefs.SetFloat("vehicle0friction", .3f);
        if (PlayerPrefs.HasKey("vehicle0rotation") == false)
            PlayerPrefs.SetInt("vehicle0rotation", 100);
        if (PlayerPrefs.HasKey("vehicle0fuel") == false)
            PlayerPrefs.SetInt("vehicle0fuel", 40);

        // Vehicle 0 Upgrades (zhiga).
        if (PlayerPrefs.HasKey("vehicle0Up1") == false)
            PlayerPrefs.SetInt("vehicle0Up1", 0);
        if (PlayerPrefs.HasKey("vehicle0Up2") == false)
            PlayerPrefs.SetInt("vehicle0Up2", 0);
        if (PlayerPrefs.HasKey("vehicle0Up3") == false)
            PlayerPrefs.SetInt("vehicle0Up3", 0);
        if (PlayerPrefs.HasKey("vehicle0Up4") == false)
            PlayerPrefs.SetInt("vehicle0Up4", 0);
        if (PlayerPrefs.HasKey("vehicle0Up5") == false)
            PlayerPrefs.SetInt("vehicle0Up5", 0);

        // Vehicle 1 stats (spectra).
        if (PlayerPrefs.HasKey("vehicle1name") == false)
            PlayerPrefs.SetString("vehicle1name", "spectra");
        if (PlayerPrefs.HasKey("vehicle1speed") == false)
            PlayerPrefs.SetInt("vehicle1speed", 6000);
        if (PlayerPrefs.HasKey("vehicle1suspension") == false)
            PlayerPrefs.SetFloat("vehicle1suspension", 10);
        if (PlayerPrefs.HasKey("vehicle1friction") == false)
            PlayerPrefs.SetFloat("vehicle1friction", .5f);
        if (PlayerPrefs.HasKey("vehicle1rotation") == false)
            PlayerPrefs.SetInt("vehicle1rotation", 150);
        if (PlayerPrefs.HasKey("vehicle1fuel") == false)
            PlayerPrefs.SetInt("vehicle1fuel", 40);

        // Vehicle 1 Upgrades (spectra).
        if (PlayerPrefs.HasKey("vehicle1Up1") == false)
            PlayerPrefs.SetInt("vehicle1Up1", 0);
        if (PlayerPrefs.HasKey("vehicle1Up2") == false)
            PlayerPrefs.SetInt("vehicle1Up2", 0);
        if (PlayerPrefs.HasKey("vehicle1Up3") == false)
            PlayerPrefs.SetInt("vehicle1Up3", 0);
        if (PlayerPrefs.HasKey("vehicle1Up4") == false)
            PlayerPrefs.SetInt("vehicle1Up4", 0);
        if (PlayerPrefs.HasKey("vehicle1Up5") == false)
            PlayerPrefs.SetInt("vehicle1Up5", 0);

        // Vehicle 2 stats (corsa).
        if (PlayerPrefs.HasKey("vehicle2name") == false)
            PlayerPrefs.SetString("vehicle2name", "corsa");
        if (PlayerPrefs.HasKey("vehicle2speed") == false)
            PlayerPrefs.SetInt("vehicle2speed", 5700);
        if (PlayerPrefs.HasKey("vehicle2suspension") == false)
            PlayerPrefs.SetFloat("vehicle2suspension", 6);
        if (PlayerPrefs.HasKey("vehicle2friction") == false)
            PlayerPrefs.SetFloat("vehicle2friction", .5f);
        if (PlayerPrefs.HasKey("vehicle2rotation") == false)
            PlayerPrefs.SetInt("vehicle2rotation", 400);
        if (PlayerPrefs.HasKey("vehicle2fuel") == false)
            PlayerPrefs.SetInt("vehicle2fuel", 40);

        // Vehicle 2 Upgrades (corsa).
        if (PlayerPrefs.HasKey("vehicle2Up1") == false)
            PlayerPrefs.SetInt("vehicle2Up1", 0);
        if (PlayerPrefs.HasKey("vehicle2Up2") == false)
            PlayerPrefs.SetInt("vehicle2Up2", 0);
        if (PlayerPrefs.HasKey("vehicle2Up3") == false)
            PlayerPrefs.SetInt("vehicle2Up3", 0);
        if (PlayerPrefs.HasKey("vehicle2Up4") == false)
            PlayerPrefs.SetInt("vehicle2Up4", 0);
        if (PlayerPrefs.HasKey("vehicle2Up5") == false)
            PlayerPrefs.SetInt("vehicle2Up5", 0);
        #endregion

        coinsText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("coins").ToString();

        lvlText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("lvl").ToString();
        expText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("exp").ToString() + "/" + PlayerPrefs.GetInt("nextlvlexp");

        chosenVehicle = PlayerPrefs.GetInt("currentVehicleId");
        PlayerPrefs.SetInt("currentVehicleId", chosenVehicle);

        SelectVehicle(chosenVehicle);
        currentVehicle = chosenVehicle;

        vehicleText.GetComponent<TextMeshProUGUI>().text = transform.GetChild(currentVehicle).name;
        vehicleUnlocked = new int[] { PlayerPrefs.GetInt("vehicleUnlocked0"), PlayerPrefs.GetInt("vehicleUnlocked1"), PlayerPrefs.GetInt("vehicleUnlocked2") };

        buyButtonVehicle.gameObject.SetActive(false);
        chooseButtonVehicle.gameObject.SetActive(true);

        chosenStage = PlayerPrefs.GetInt("currentStageId");
        PlayerPrefs.SetInt("currentStageId", chosenStage);

        SelectStage(chosenStage);
        currentStage = chosenStage;

        stageText.GetComponent<TextMeshProUGUI>().text = stages.transform.GetChild(currentStage).name;
        stageRecordText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("stage" + currentStage + "score").ToString() + " m";
        stageUnlocked = new int[] { PlayerPrefs.GetInt("stageUnlocked0"), PlayerPrefs.GetInt("stageUnlocked1") };

        buyButtonStage.gameObject.SetActive(false);
        chooseButtonStage.gameObject.SetActive(true);
    }

    private void Update()
    {
        // Cheats.
        if (Input.GetKeyDown(KeyCode.M))
            AddMoney(1000);

        if (Input.GetKeyDown(KeyCode.E))
            AddExp(400);

        LevelCheck();

        if (buyButtonVehicle.gameObject.activeInHierarchy)
            buyButtonVehicle.interactable = PlayerPrefs.GetInt("coins") >= vehiclePrices[currentVehicle] && PlayerPrefs.GetInt("lvl") >= vehicleLvl[currentVehicle];

        if (PlayerPrefs.GetInt("lvl") >= vehicleLvl[currentVehicle])
        {
            vehicleText.GetComponent<TextMeshProUGUI>().text = transform.GetChild(currentVehicle).name;
            transform.GetChild(currentVehicle).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            transform.GetChild(currentVehicle).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            transform.GetChild(currentVehicle).gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            transform.GetChild(currentVehicle).gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }
        else
        {
            vehicleText.GetComponent<TextMeshProUGUI>().text = "need level " + vehicleLvl[currentVehicle] + " to unlock";
            transform.GetChild(currentVehicle).gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            transform.GetChild(currentVehicle).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            transform.GetChild(currentVehicle).gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            transform.GetChild(currentVehicle).gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        }

        if (buyButtonStage.gameObject.activeInHierarchy)
            buyButtonStage.interactable = PlayerPrefs.GetInt("coins") >= stagePrices[currentStage] && PlayerPrefs.GetInt("lvl") >= stageLvl[currentStage];

        if (PlayerPrefs.GetInt("lvl") >= stageLvl[currentStage])
        {
            stageText.GetComponent<TextMeshProUGUI>().text = stages.transform.GetChild(currentStage).name;
            stageRecordText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("stage" + currentStage + "score").ToString() + " m";
            stages.transform.GetChild(currentStage).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }
        else
        {
            stageText.GetComponent<TextMeshProUGUI>().text = "need level " + stageLvl[currentStage] + " to unlock";
            stageRecordText.GetComponent<TextMeshProUGUI>().text = "";
            stages.transform.GetChild(currentStage).gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        }
    }

    private void SelectVehicle(int index)
    {
        prevButtonVehicle.interactable = (index != 0);
        nextButtonVehicle.interactable = (index != transform.childCount - 1);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }

        UpdateVehicleUI();
    }

    private void UpdateVehicleUI()
    {
        if (vehicleUnlocked[currentVehicle] == 1)
        {
            buyButtonVehicle.gameObject.SetActive(false);
            chooseButtonVehicle.gameObject.SetActive(true);
        }
        else
        {
            buyButtonVehicle.gameObject.SetActive(true);
            chooseButtonVehicle.gameObject.SetActive(false);
            vehiclePriceText.text = vehiclePrices[currentVehicle].ToString();
        }
    }

    public void ChangeVehicle(int change)
    {
        currentVehicle += change;
        SelectVehicle(currentVehicle);
    }
    public void BackVehicle()
    {
        currentVehicle = chosenVehicle;
        SelectVehicle(currentVehicle);
    }

    public void ChosenVehicle()
    {
        chosenVehicle = currentVehicle;
        SelectVehicle(currentVehicle);
        PlayerPrefs.SetInt("currentVehicleId", chosenVehicle);
    }

    public void BuyVehicle()
    {
        AddMoney(-vehiclePrices[currentVehicle]);
        PlayerPrefs.SetInt("vehicleUnlocked" + currentVehicle, 1);
        vehicleUnlocked[currentVehicle] = 1;
        chosenVehicle = currentVehicle;
        PlayerPrefs.SetInt("currentVehicleId", chosenVehicle);

        UpdateVehicleUI();
    }

    private void SelectStage(int index)
    {
        prevButtonStage.interactable = (index != 0);
        nextButtonStage.interactable = (index != stages.transform.childCount - 1);

        for (int i = 0; i < stages.transform.childCount; i++)
        {
            stages.transform.GetChild(i).gameObject.SetActive(i == index);
        }

        UpdateStageUI();
    }

    private void UpdateStageUI()
    {
        if (stageUnlocked[currentStage] == 1)
        {
            buyButtonStage.gameObject.SetActive(false);
            chooseButtonStage.gameObject.SetActive(true);
        }
        else
        {
            buyButtonStage.gameObject.SetActive(true);
            chooseButtonStage.gameObject.SetActive(false);
            stagePriceText.text = stagePrices[currentStage].ToString();
        }
    }

    public void ChangeStage(int change)
    {
        currentStage += change;
        SelectStage(currentStage);
    }
    public void BackStage()
    {
        currentStage = chosenStage;
        SelectStage(currentStage);
    }

    public void ChosenStage()
    {
        chosenStage = currentStage;
        SelectStage(currentStage);
        PlayerPrefs.SetInt("currentStageId", chosenStage);
    }

    public void BuyStage()
    {
        AddMoney(-stagePrices[currentStage]);
        PlayerPrefs.SetInt("stageUnlocked" + currentStage, 1);
        stageUnlocked[currentStage] = 1;
        chosenStage = currentStage;
        PlayerPrefs.SetInt("currentStageId", chosenStage);

        UpdateStageUI();
    }

    public void AddMoney(int value)
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + value);
        coinsText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("coins").ToString();
    }
    public void AddExp(int value)
    {
        if (PlayerPrefs.GetInt("lvl") < 99)
        {
            PlayerPrefs.SetInt("exp", PlayerPrefs.GetInt("exp") + value);
            expText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("exp").ToString() + "/" + PlayerPrefs.GetInt("nextlvlexp");
        }
    }

    public void LevelCheck()
    {
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
}
