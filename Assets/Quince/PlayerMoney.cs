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

    [Header("Troop Counts")]
    public int troop1Count = 0;
    public int troop2Count = 0;
    public int troop3Count = 0;
    public int troop4Count = 0;

    [Header("Troop Prices")]
    public int troop1Price = 15;
    public int troop2Price = 20;
    public int troop3Price = 25;
    public int troop4Price = 30;

    [Header("Troop Count UI")]
    public TextMeshProUGUI troop1Text;
    public TextMeshProUGUI troop2Text;
    public TextMeshProUGUI troop3Text;
    public TextMeshProUGUI troop4Text;

    [Header("Troop Cost UI")]
    public TextMeshProUGUI troop1CostText;
    public TextMeshProUGUI troop2CostText;
    public TextMeshProUGUI troop3CostText;
    public TextMeshProUGUI troop4CostText;

    [Header("Market")]
    public int money = 0;
    public int Markets = 0;
    public int marketPrice = 10;
    public int MarketIncome = 0;

    [Header("Upgrades")]
    public float doubleTroubleChance = 0f; // percent chance for double troop

    void Start()
    {
        UpdateUI();
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
            marketPrice += 5;
            UpdateUI();
        }
    }

    public void BuyTroop1()
    {
        if (money >= troop1Price)
        {
            money -= troop1Price;
            troop1Count++;
            // Double Trouble logic
            if (doubleTroubleChance > 0f && Random.value < doubleTroubleChance / 100f)
            {
                troop1Count++;
            }
            UpdateUI();
        }
    }

    public void BuyTroop2()
    {
        if (money >= troop2Price)
        {
            money -= troop2Price;
            troop2Count++;
            if (doubleTroubleChance > 0f && Random.value < doubleTroubleChance / 100f)
            {
                troop2Count++;
            }
            UpdateUI();
        }
    }

    public void BuyTroop3()
    {
        if (money >= troop3Price)
        {
            money -= troop3Price;
            troop3Count++;
            if (doubleTroubleChance > 0f && Random.value < doubleTroubleChance / 100f)
            {
                troop3Count++;
            }
            UpdateUI();
        }
    }

    public void BuyTroop4()
    {
        if (money >= troop4Price)
        {
            money -= troop4Price;
            troop4Count++;
            if (doubleTroubleChance > 0f && Random.value < doubleTroubleChance / 100f)
            {
                troop4Count++;
            }
            UpdateUI();
        }
    }

    public void RemoveTroop1()
    {
        if (troop1Count > 0)
        {
            troop1Count--;
            UpdateUI();
        }
    }

    public void RemoveTroop2()
    {
        if (troop2Count > 0)
        {
            troop2Count--;
            UpdateUI();
        }
    }

    public void RemoveTroop3()
    {
        if (troop3Count > 0)
        {
            troop3Count--;
            UpdateUI();
        }
    }

    public void RemoveTroop4()
    {
        if (troop4Count > 0)
        {
            troop4Count--;
            UpdateUI();
        }
    }

    public void CollectMarketIncome()
    {
        if (Markets > 0 && MarketIncome > 0)
        {
            AddMoney(Markets * MarketIncome);
        }
    }

    private void UpdateUI()
    {
        moneyText.text = $"Money: {money}";
        marketText.text = $"Markets: {Markets}";
        marketPriceText.text = $"Buy ({marketPrice})";

        troop1Text.text = $"Troop 1: {troop1Count}";
        troop2Text.text = $"Troop 2: {troop2Count}";
        troop3Text.text = $"Troop 3: {troop3Count}";
        troop4Text.text = $"Troop 4: {troop4Count}";

        troop1CostText.text = $"Troop 1: {troop1Price}";
        troop2CostText.text = $"Troop 2: {troop2Price}";
        troop3CostText.text = $"Troop 3: {troop3Price}";
        troop4CostText.text = $"Troop 4: {troop4Price}";
    }
}
