using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TuneMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button upgradeButton1;
    [SerializeField] private TextMeshProUGUI upgradeButton1text;

    [SerializeField] private Button upgradeButton2;
    [SerializeField] private TextMeshProUGUI upgradeButton2text;

    [SerializeField] private Button upgradeButton3;
    [SerializeField] private TextMeshProUGUI upgradeButton3text;

    [SerializeField] private Button upgradeButton4;
    [SerializeField] private TextMeshProUGUI upgradeButton4text;

    [SerializeField] private Button upgradeButton5;
    [SerializeField] private TextMeshProUGUI upgradeButton5text;

    [Header("Text")]
    [SerializeField] private GameObject vehicleName;
    [SerializeField] private GameObject statsText;

    [Header("Bars and Points")]
    [SerializeField] private Sprite fullPoint;
    [SerializeField] private Sprite emptyPoint;

    [SerializeField] private Image[] points1;
    [SerializeField] private Image[] points2;
    [SerializeField] private Image[] points3;
    [SerializeField] private Image[] points4;
    [SerializeField] private Image[] points5;

    [Header("Coins")]
    [SerializeField]
    private GameObject coinsText;


    private void Start()
    {
        UpgradeUI(1);
        UpgradeUI(2);
        UpgradeUI(3);
        UpgradeUI(4);
        UpgradeUI(5);

        PriceCheck();
    }

    public void UpdateUI()
    {
        UpdateStats();

        UpgradeUI(1);
        UpgradeUI(2);
        UpgradeUI(3);
        UpgradeUI(4);
        UpgradeUI(5);
    }

    private void Update()
    {
        PriceCheck();
        UpdateButtonsCheck();
        UpdateButtonsText();
    }

    private void UpdateButtonsCheck()
    {
        if (PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 1) < 9)
            upgradeButton1.gameObject.SetActive(true);
        else
            upgradeButton1.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 2) < 9)
            upgradeButton2.gameObject.SetActive(true);
        else
            upgradeButton2.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 3) < 9)
            upgradeButton3.gameObject.SetActive(true);
        else
            upgradeButton3.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 4) < 9)
            upgradeButton4.gameObject.SetActive(true);
        else
            upgradeButton4.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 5) < 9)
            upgradeButton5.gameObject.SetActive(true);
        else
            upgradeButton5.gameObject.SetActive(false);
    }

    // Bar points.
    public void UpgradeUI(int index)
    {
        for (int i = 0; i < points1.Length; i++)
        {
            if (i < PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + index))
            {
                if (index == 1)
                    points1[i].sprite = fullPoint;
                if (index == 2)
                    points2[i].sprite = fullPoint;
                if (index == 3)
                    points3[i].sprite = fullPoint;
                if (index == 4)
                    points4[i].sprite = fullPoint;
                if (index == 5)
                    points5[i].sprite = fullPoint;
            }
            else
            {   
                if (index == 1)
                    points1[i].sprite = emptyPoint;
                if (index == 2)
                    points2[i].sprite = emptyPoint;
                if (index == 3)
                    points3[i].sprite = emptyPoint;
                if (index == 4)
                    points4[i].sprite = emptyPoint;
                if (index == 5)
                    points5[i].sprite = emptyPoint;
            }
        }
    }

    public void Upgrade(int index)
    {
        AddMoney(-(PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + index) * 50 + 50));
        VehicleStatsUpgrade(index);

        UpdateStats();

        PlayerPrefs.SetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + index, PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + index) + 1);
    }

    // Update Buttons Text.
    int lvlText;
    private void UpdateButtonsText()
    {
        if (PlayerPrefs.GetInt("currentVehicleId") == 0)
        {
            lvlText = 1;
        }
        if (PlayerPrefs.GetInt("currentVehicleId") == 1)
        {
            lvlText = 3;
        }
        if (PlayerPrefs.GetInt("currentVehicleId") == 2)
        {
            lvlText = 5;
        }

        if (PlayerPrefs.GetInt("lvl") >= PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 1) + lvlText)
        {
            upgradeButton1text.fontSize = 10;
            upgradeButton1text.text = (PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 1) * 50 + 50).ToString() + " coins";
        }
        else
        {
            upgradeButton1text.fontSize = 9;
            upgradeButton1text.text = "need " + (PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 1) + lvlText) + "\nlevel";
        }   

        if (PlayerPrefs.GetInt("lvl") >= PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 2) + lvlText)
        {
            upgradeButton2text.fontSize = 10;
            upgradeButton2text.text = (PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 2) * 50 + 50).ToString() + " coins";
        }
        else
        {
            upgradeButton2text.fontSize = 9;
            upgradeButton2text.text = "need " + (PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 2) + lvlText) + "\nlevel";
        } 

        if (PlayerPrefs.GetInt("lvl") >= PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 3) + lvlText)
        {
            upgradeButton3text.fontSize = 10;
            upgradeButton3text.text = (PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 3) * 50 + 50).ToString() + " coins";
        }
        else
        {
            upgradeButton3text.fontSize = 9;
            upgradeButton3text.text = "need " + (PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 3) + lvlText) + "\nlevel";
        }

        if (PlayerPrefs.GetInt("lvl") >= PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 4) + lvlText)
        {
            upgradeButton4text.fontSize = 10;
            upgradeButton4text.text = (PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 4) * 50 + 50).ToString() + " coins";
        }
        else
        {
            upgradeButton4text.fontSize = 9;
            upgradeButton4text.text = "need " + (PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 4) + lvlText) + "\nlevel";
        }

        if (PlayerPrefs.GetInt("lvl") >= PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 5) + lvlText)
        {
            upgradeButton5text.fontSize = 10;
            upgradeButton5text.text = (PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 5) * 50 + 50).ToString() + " coins";
        }
        else
        {
            upgradeButton5text.fontSize = 9;
            upgradeButton5text.text = "need " + (PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 5) + lvlText) + "\nlevel";
        }
    }

    private void AddMoney(int value)
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + value);
        coinsText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("coins").ToString();
    }

    int lvlCheck;
    private void PriceCheck()
    {
        if (PlayerPrefs.GetInt("currentVehicleId") == 0)
        {
            lvlCheck = 1;
        }
        if (PlayerPrefs.GetInt("currentVehicleId") == 1)
        {
            lvlCheck = 3;
        }
        if (PlayerPrefs.GetInt("currentVehicleId") == 2)
        {
            lvlCheck = 5;
        }

        upgradeButton1.interactable = PlayerPrefs.GetInt("coins") >= PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 1) * 50 + 50
            && PlayerPrefs.GetInt("lvl") >= PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 1) + lvlCheck;
        upgradeButton2.interactable = PlayerPrefs.GetInt("coins") >= PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 2) * 50 + 50
            && PlayerPrefs.GetInt("lvl") >= PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 2) + lvlCheck;
        upgradeButton3.interactable = PlayerPrefs.GetInt("coins") >= PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 3) * 50 + 50
            && PlayerPrefs.GetInt("lvl") >= PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 3) + lvlCheck;
        upgradeButton4.interactable = PlayerPrefs.GetInt("coins") >= PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 4) * 50 + 50
            && PlayerPrefs.GetInt("lvl") >= PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 4) + lvlCheck;
        upgradeButton5.interactable = PlayerPrefs.GetInt("coins") >= PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 5) * 50 + 50
            && PlayerPrefs.GetInt("lvl") >= PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 5) + lvlCheck;
    }

    private void UpdateStats()
    {
        vehicleName.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "name");

        statsText.GetComponent<TextMeshProUGUI>().text = "speed: " + PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "speed").ToString()
            + "\nsuspension: " + PlayerPrefs.GetFloat("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "suspension").ToString("0.00")
            + "\nfriction: " + PlayerPrefs.GetFloat("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "friction").ToString("0.00")
            + "\nrotation speed: " + PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "rotation").ToString()
            + "\nfuel: " + PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "fuel").ToString();
    }

    // Stats formulas.
    private void VehicleStatsUpgrade(int index)
    {
        if (index == 1)
            PlayerPrefs.SetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "speed",
            PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "speed") + (147 + (3 * (1 + PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 1)))));

        if (index == 2)
            PlayerPrefs.SetFloat("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "suspension",
            PlayerPrefs.GetFloat("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "suspension") + (.15f + (.01f * (1 + PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 2)))));

        if (index == 3)
            PlayerPrefs.SetFloat("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "friction",
            PlayerPrefs.GetFloat("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "friction") + (.025f + (.005f * (1 + PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 3)))));

        if (index == 4)
            PlayerPrefs.SetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "rotation",
            PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "rotation") + (10 + (1 + PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 4))/4));

        if (index == 5)
            PlayerPrefs.SetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "fuel",
            PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "fuel") + (2 + (1 + PlayerPrefs.GetInt("vehicle" + PlayerPrefs.GetInt("currentVehicleId") + "Up" + 5))/4));
    }
}
