using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class PlayerMoney : MonoBehaviour
{
    [Header("UI Texts")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI marketText;
    public TextMeshProUGUI marketPriceText;

    [Header("Unit Counts")]
    public int troopCount = 0;
    public int tankCount = 0;
    public int rangeCount = 0;

    [Header("Unit Prices")]
    public int troopPrice = 15;
    public int tankPrice = 20;
    public int rangePrice = 25;

    [Header("Unit Count UI")]
    public TextMeshProUGUI troopText;
    public TextMeshProUGUI tankText;
    public TextMeshProUGUI rangeText;

    [Header("Unit Cost UI")]
    public TextMeshProUGUI troopCostText;
    public TextMeshProUGUI tankCostText;
    public TextMeshProUGUI rangeCostText;

    [Header("Market")]
    public int money = 0;
    public int Markets = 0;
    public int marketPrice = 10;
    public int MarketIncome = 0;
    public int MarketIncrease;

    [Header("Upgrades")]
    public float doubleTroubleChance = 0f;
    public float marketIncomeMultiplier = 1f; // Default multiplier

    [Header("Base Health")]
    public int baseHealth = 100;
    void Start()
    {
        UpdateUI();
    }
    public void RemoveBaseHealth(int amount)
    {
        baseHealth -= amount;
        if (baseHealth < 0) baseHealth = 0;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    public void SubtractMoney(int amount)
    {
        if (money < amount) return;
        money -= amount;
        UpdateUI();
    }

    public void BuyMarket()
    {
        if (money >= marketPrice)
        {
            money -= marketPrice;
            Markets++;
            marketPrice += MarketIncrease;
            UpdateUI();
        }
    }

    public void BuyTroop()
    {
        if (money >= troopPrice)
        {
            money -= troopPrice;
            troopCount++;
            if (doubleTroubleChance > 0f && Random.value < doubleTroubleChance / 100f)
            {
                troopCount++;
            }
            UpdateUI();
        }
    }

    public void BuyTank()
    {
        if (money >= tankPrice)
        {
            money -= tankPrice;
            tankCount++;
            if (doubleTroubleChance > 0f && Random.value < doubleTroubleChance / 100f)
            {
                tankCount++;
            }
            UpdateUI();
        }
    }

    public void BuyRange()
    {
        if (money >= rangePrice)
        {
            money -= rangePrice;
            rangeCount++;
            if (doubleTroubleChance > 0f && Random.value < doubleTroubleChance / 100f)
            {
                rangeCount++;
            }
            UpdateUI();
        }
    }

    public void RemoveTroop()
    {
        if (troopCount > 0)
        {
            troopCount--;
            UpdateUI();
        }
    }

    public void RemoveTank()
    {
        if (tankCount > 0)
        {
            tankCount--;
            UpdateUI();
        }
    }

    public void RemoveRange()
    {
        if (rangeCount > 0)
        {
            rangeCount--;
            UpdateUI();
        }
    }

    public void CollectMarketIncome()
    {
        if (Markets > 0 && MarketIncome > 0)
        {
            int totalIncome = Mathf.RoundToInt(Markets * MarketIncome * marketIncomeMultiplier);
            AddMoney(totalIncome);
        }
    }

    private void UpdateUI()
    {
        moneyText.text = $"Money: {money}";
        marketText.text = $"Markets: {Markets}";
        marketPriceText.text = $"Buy ({marketPrice})";

        troopText.text = $"Troop: {troopCount}";
        tankText.text = $"Tank: {tankCount}";
        rangeText.text = $"Range: {rangeCount}";

        troopCostText.text = $"Troop: {troopPrice}";
        tankCostText.text = $"Tank: {tankPrice}";
        rangeCostText.text = $"Range: {rangePrice}";
    }
}
