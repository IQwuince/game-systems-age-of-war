using UnityEngine;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public PlayerMoney pm;
    public TurnSystem ts;

    // Troop
    [Header("Troop")]
    public int troopHealth = 100;
    public int troopDamage = 20;
    public float troopRange = 0f;

    public TextMeshProUGUI troopHealthText;
    public TextMeshProUGUI troopDamageText;
    public TextMeshProUGUI troopRangeText;

    public TextMeshProUGUI troopHealthUpgradeButtonText;
    public TextMeshProUGUI troopDamageUpgradeButtonText;
    public TextMeshProUGUI troopRangeUpgradeButtonText;

    private int troopHealthUpgradePrice = 4;
    private int troopDamageUpgradePrice = 4;
    private int troopRangeUpgradePrice = 4;

    // Tank
    [Header("Tank")]
    public int tankHealth = 120;
    public int tankDamage = 15;
    public float tankRange = 0f;

    public TextMeshProUGUI tankHealthText;
    public TextMeshProUGUI tankDamageText;
    public TextMeshProUGUI tankRangeText;

    public TextMeshProUGUI tankHealthUpgradeButtonText;
    public TextMeshProUGUI tankDamageUpgradeButtonText;
    public TextMeshProUGUI tankRangeUpgradeButtonText;

    private int tankHealthUpgradePrice = 4;
    private int tankDamageUpgradePrice = 4;
    private int tankRangeUpgradePrice = 4;

    // Range
    [Header("Range")]
    public int rangeHealth = 80;
    public int rangeDamage = 25;
    public float rangeRange = 5.0f;

    public TextMeshProUGUI rangeHealthText;
    public TextMeshProUGUI rangeDamageText;
    public TextMeshProUGUI rangeRangeText;

    public TextMeshProUGUI rangeHealthUpgradeButtonText;
    public TextMeshProUGUI rangeDamageUpgradeButtonText;
    public TextMeshProUGUI rangeRangeUpgradeButtonText;

    public int rangeHealthUpgradePrice = 30;
    public int rangeDamageUpgradePrice = 30;
    public int rangeRangeUpgradePrice = 30;

    public int increaseUpgradePrice = 10;


    [Header("Double Trouble Upgrade")]
    public TextMeshProUGUI doubleTroubleButtonText;
    public int doubleTroubleUpgradePrice = 10;
    public float doubleTroubleChance = 0f; // percent, e.g. 5 means 5%
    public TextMeshProUGUI doubleTroubleChanceText;

    [Header("Market Income Upgrade")]
    public TextMeshProUGUI marketIncomeUpgradeButtonText;
    public TextMeshProUGUI marketIncomeBonusText;
    public int marketIncomeUpgradePrice = 20; // Set initial price in Inspector
    public float marketIncomeBonusPercent = 0f; // e.g. 10 means +10%

    void Start()
    {
        UpdateTroopStatsUI();
        UpdateUpgradeButtonTexts();
        UpdateDoubleTroubleUI();
        UpdateMarketIncomeUpgradeUI();
        pm.doubleTroubleChance = doubleTroubleChance;
        pm.marketIncomeMultiplier = 1f + marketIncomeBonusPercent / 100f;
    }

    // Troop upgrades
    public void UpgradeTroopHealth()
    {
        if (pm.money >= troopHealthUpgradePrice)
        {
            pm.SubtractMoney(troopHealthUpgradePrice);
            troopHealth += 10;
            troopHealthUpgradePrice += increaseUpgradePrice;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }
    public void UpgradeTroopDamage()
    {
        if (pm.money >= troopDamageUpgradePrice)
        {
            pm.SubtractMoney(troopDamageUpgradePrice);
            troopDamage += 2;
            troopDamageUpgradePrice += increaseUpgradePrice;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }
    public void UpgradeTroopRange()
    {
        if (pm.money >= troopRangeUpgradePrice)
        {
            pm.SubtractMoney(troopRangeUpgradePrice);
            troopRange += 1f;
            troopRangeUpgradePrice += increaseUpgradePrice;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }

    // Tank upgrades
    public void UpgradeTankHealth()
    {
        if (pm.money >= tankHealthUpgradePrice)
        {
            pm.SubtractMoney(tankHealthUpgradePrice);
            tankHealth += 10;
            tankHealthUpgradePrice += increaseUpgradePrice;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }
    public void UpgradeTankDamage()
    {
        if (pm.money >= tankDamageUpgradePrice)
        {
            pm.SubtractMoney(tankDamageUpgradePrice);
            tankDamage += 2;
            tankDamageUpgradePrice += increaseUpgradePrice;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }
    public void UpgradeTankRange()
    {
        if (pm.money >= tankRangeUpgradePrice)
        {
            pm.SubtractMoney(tankRangeUpgradePrice);
            tankRange += 1f;
            tankRangeUpgradePrice += increaseUpgradePrice;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }

    // Range upgrades
    public void UpgradeRangeHealth()
    {
        if (pm.money >= rangeHealthUpgradePrice)
        {
            pm.SubtractMoney(rangeHealthUpgradePrice);
            rangeHealth += 10;
            rangeHealthUpgradePrice += increaseUpgradePrice;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }
    public void UpgradeRangeDamage()
    {
        if (pm.money >= rangeDamageUpgradePrice)
        {
            pm.SubtractMoney(rangeDamageUpgradePrice);
            rangeDamage += 2;
            rangeDamageUpgradePrice += increaseUpgradePrice;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }
    public void UpgradeRangeRange()
    {
        if (pm.money >= rangeRangeUpgradePrice)
        {
            pm.SubtractMoney(rangeRangeUpgradePrice);
            rangeRange += 1f;
            rangeRangeUpgradePrice += increaseUpgradePrice;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }


    public void UpdateTroopStatsUI()
    {
        troopHealthText.text = $"Health: {troopHealth}";
        troopDamageText.text = $"Damage: {troopDamage}";

        tankHealthText.text = $"Health: {tankHealth}";
        tankDamageText.text = $"Damage: {tankDamage}";

        rangeHealthText.text = $"Health: {rangeHealth}";
        rangeDamageText.text = $"Damage: {rangeDamage}";
        rangeRangeText.text = $"Range: {rangeRange}";
    }

    private void UpdateUpgradeButtonTexts()
    {
        troopHealthUpgradeButtonText.text = $"Upgrade ({troopHealthUpgradePrice})";
        troopDamageUpgradeButtonText.text = $"Upgrade ({troopDamageUpgradePrice})";

        tankHealthUpgradeButtonText.text = $"Upgrade ({tankHealthUpgradePrice})";
        tankDamageUpgradeButtonText.text = $"Upgrade ({tankDamageUpgradePrice})";

        rangeHealthUpgradeButtonText.text = $"Upgrade ({rangeHealthUpgradePrice})";
        rangeDamageUpgradeButtonText.text = $"Upgrade ({rangeDamageUpgradePrice})";
        rangeRangeUpgradeButtonText.text = $"Upgrade ({rangeRangeUpgradePrice})";
    }

    public void UpgradeDoubleTrouble()
    {
        if (pm.money >= doubleTroubleUpgradePrice)
        {
            pm.SubtractMoney(doubleTroubleUpgradePrice);
            doubleTroubleChance += 5f;
            doubleTroubleUpgradePrice += 20;
            UpdateDoubleTroubleUI();
            pm.doubleTroubleChance = doubleTroubleChance; // Sync to PlayerMoney
        }
    }

    private void UpdateDoubleTroubleUI()
    {
        doubleTroubleButtonText.text = $"Double Trouble ({doubleTroubleUpgradePrice})";
        doubleTroubleChanceText.text = $"Chance: {doubleTroubleChance}%";
    }

    public void UpgradeMarketIncome()
    {
        if (pm.money >= marketIncomeUpgradePrice)
        {
            pm.SubtractMoney(marketIncomeUpgradePrice);
            marketIncomeBonusPercent += 10f;
            marketIncomeUpgradePrice += 20;
            pm.marketIncomeMultiplier = 1f + marketIncomeBonusPercent / 100f;
            UpdateMarketIncomeUpgradeUI();
        }
    }
    private void UpdateMarketIncomeUpgradeUI()
    {
        marketIncomeUpgradeButtonText.text = $"Market Income ({marketIncomeUpgradePrice})";
        marketIncomeBonusText.text = $"Bonus: {marketIncomeBonusPercent}%";
    }
}


