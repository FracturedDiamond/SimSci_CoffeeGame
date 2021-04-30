using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Button UpgradeCoffeeMachine;
    public Button UpgradeWorker;
    public Button UpgradePrices;

    public GameObject[] CoffeePotUpgrades;
    public GameObject[] MoneyUpgrades;
    public GameObject[] WorkerUpgrades;

    public TextMeshProUGUI PlayerMoney;
    public TextMeshProUGUI WaitTime;

    public TextMeshProUGUI UpgradeMachine;
    public TextMeshProUGUI UpgradeWorkers;
    public TextMeshProUGUI UpgradePrice;

    public int upgradeCountCoffeeMachines = 0;
    public int upgradeCountWorkers = 0;
    public int upgradeCountMoney = 0;

    public float averageWaitTime = 120;
    public float timeScale = 10f;

    public float money;

    private int upgradeCost;
    private int CoffeeMachineUpgrade;
    private int WorkerUpgrade;
    private int PricesUpgrade;

    private float coffeeMachines;
    private float prices;
    private float workerEfficiency;

    private float CoffeeTime;
    private int upgradeCountWorkersStatus;
    private int upgradeCountMoneyStatus;
    private int upgradeCountCoffeeMachinesStatus;

    private void Start()
    {
        coffeeMachines = 1.00f;
        prices = 5f;
        workerEfficiency = 1.5f;

        money = 100;
        PlayerMoney.text = "$" + money;

        upgradeCost = 50;

        CoffeeMachineUpgrade = 50;

        WorkerUpgrade = 50;

        PricesUpgrade = 50;

        CoffeeTime = (averageWaitTime / timeScale) * workerEfficiency;
        Debug.Log("Coffee Time is: " + CoffeeTime);

        PrintUpgrades();

        StartCoroutine(CoffeeSale());
    }

    private void Update()
    {
        PrintUpgrades();
        CheckButtons();
        CheckUpgradeStatus();
        PrintWaitTime();
    }

    public void onUpgradeCoffeeMachine()
    {
        upgradeCountCoffeeMachines += 1;

        coffeeMachines += .25f;
        money -= CoffeeMachineUpgrade;
        CoffeeMachineUpgrade += upgradeCost;

        PrintUpgrades();

        //Debug.Log("Coffee Machine Upgrade Price: " + CoffeeMachineUpgrade);
        //Debug.Log("Player Money: " + money);

        PlayerMoney.text = "$" + money;

        if (upgradeCountCoffeeMachines == 20)
        {
            upgradeCountCoffeeMachinesStatus = 2;
        }

        if (upgradeCountCoffeeMachines == 10)
        {
            upgradeCountCoffeeMachinesStatus = 1;
        }
    }

    public void onUpgradeWorker()
    {
        upgradeCountWorkers += 1;

        workerEfficiency -= .04f;

        CoffeeTime = (averageWaitTime / timeScale) * workerEfficiency;
        Debug.Log("Coffee Making Time is: " + CoffeeTime);

        money -= WorkerUpgrade;
        WorkerUpgrade += upgradeCost;

        PrintUpgrades();

        //Debug.Log("Worker Upgrade Price: " + WorkerUpgrade);
        //Debug.Log("Player Money: " + money);

        //Debug.Log("Worker Efficiency: " + workerEfficiency);

        PlayerMoney.text = "$" + money;

        if (upgradeCountWorkers == 20)
        {
            upgradeCountWorkersStatus = 2;
        }

        if (upgradeCountWorkers == 10)
        {
            upgradeCountWorkersStatus = 1;
        }
    }

    public void onUpgradeCoffeePrices()
    {
        upgradeCountMoney += 1;

        prices += .1f;
        money -= PricesUpgrade;
        PricesUpgrade += upgradeCost;

        PrintUpgrades();

        //Debug.Log("Prices Upgrade: " + PricesUpgrade);
        //Debug.Log("Player Money: " + money);

        //Debug.Log("Coffee Price: " + prices);

        PlayerMoney.text = "$" + money;

        if(upgradeCountMoney == 20)
        {
            upgradeCountMoneyStatus = 2;
        }

        if(upgradeCountMoney == 10)
        {
            upgradeCountMoneyStatus = 1;
        }
    }

    IEnumerator CoffeeSale()
    {
        Debug.Log("Sale in Progress");

        yield return new WaitForSeconds(CoffeeTime);
        float Revenue = money + (coffeeMachines * prices);
        money = Mathf.Round(Revenue * 100f) / 100f;
        PlayerMoney.text = "$" + money;

        StartCoroutine(CoffeeSale());
    }

    public void CheckButtons()
    {
        if (money < CoffeeMachineUpgrade || upgradeCountCoffeeMachines == 30)
        {
            UpgradeCoffeeMachine.interactable = false;
        }
        else
        {
            UpgradeCoffeeMachine.interactable = true;
        }

        if (money < WorkerUpgrade || upgradeCountWorkers == 30)
        {
            UpgradeWorker.interactable = false;
        }
        else
        {
            UpgradeWorker.interactable = true;
        }

        if (money < PricesUpgrade || upgradeCountMoney == 30)
        {
            UpgradePrices.interactable = false;
        }
        else
        {
            UpgradePrices.interactable = true;
        }
    }

    public void PrintUpgrades()
    {
        UpgradeMachine.text = "Upgrade Cost: " + CoffeeMachineUpgrade;
        UpgradeWorkers.text = "Upgrade Cost: " + WorkerUpgrade;
        UpgradePrice.text = "Upgrade Cost: " + PricesUpgrade;
    }

    public void CheckUpgradeStatus()
    {
        switch (upgradeCountCoffeeMachinesStatus)
        {
            case 2:
                CoffeePotUpgrades[1].SetActive(false);
                CoffeePotUpgrades[2].SetActive(true);
                break;

            case 1:
                CoffeePotUpgrades[0].SetActive(false);
                CoffeePotUpgrades[1].SetActive(true);
                break;

            default:
                CoffeePotUpgrades[2].SetActive(false);
                CoffeePotUpgrades[1].SetActive(false);
                CoffeePotUpgrades[0].SetActive(true);
                break;
        }

        switch (upgradeCountMoneyStatus)
        {
            case 2:
                MoneyUpgrades[1].SetActive(false);
                MoneyUpgrades[2].SetActive(true);
                break;

            case 1:
                MoneyUpgrades[0].SetActive(false);
                MoneyUpgrades[1].SetActive(true);
                break;

            default:
                MoneyUpgrades[2].SetActive(false);
                MoneyUpgrades[1].SetActive(false);
                MoneyUpgrades[0].SetActive(true);
                break;
        }

        switch (upgradeCountWorkersStatus)
        {
            case 2:
                WorkerUpgrades[1].SetActive(false);
                WorkerUpgrades[2].SetActive(true);
                break;

            case 1:
                WorkerUpgrades[0].SetActive(false);
                WorkerUpgrades[1].SetActive(true);
                break;

            default:
                WorkerUpgrades[2].SetActive(false);
                WorkerUpgrades[1].SetActive(false);
                WorkerUpgrades[0].SetActive(true);
                break;
        }
    }

    public void PrintWaitTime()
    {
        WaitTime.text = (averageWaitTime * workerEfficiency) / 60 + " Minutes";
    }
}
