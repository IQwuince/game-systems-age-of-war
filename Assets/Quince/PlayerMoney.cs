using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class PlayerMoney : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI marketText;
    public TextMeshProUGUI marketPriceText;

    // Troop counts
    public int troop1Count = 0;
    public int troop2Count = 0;
    public int troop3Count = 0;
    public int troop4Count = 0;

    // Troop prices
    public int troop1Price = 15;
    public int troop2Price = 20;
    public int troop3Price = 25;
    public int troop4Price = 30;

    // Troop UI Texts
    [Header("Troops")]
    public TextMeshProUGUI troop1Text;
    public TextMeshProUGUI troop2Text;
    public TextMeshProUGUI troop3Text;
    public TextMeshProUGUI troop4Text;

    public int money = 0;
    public int Markets = 0;
    public int marketPrice = 10;
    public int MarketIncome;

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
        if (money - amount < 0) return;
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
            UpdateUI();
        }
    }

    public void BuyTroop2()
    {
        if (money >= troop2Price)
        {
            money -= troop2Price;
            troop2Count++;
            UpdateUI();
        }
    }

    public void BuyTroop3()
    {
        if (money >= troop3Price)
        {
            money -= troop3Price;
            troop3Count++;
            UpdateUI();
        }
    }

    public void BuyTroop4()
    {
        if (money >= troop4Price)
        {
            money -= troop4Price;
            troop4Count++;
            UpdateUI();
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
    }

    public void CollectMarketIncome()
    {
        if (Markets > 0)
        {
            AddMoney(Markets * MarketIncome);
        }
    }
}
