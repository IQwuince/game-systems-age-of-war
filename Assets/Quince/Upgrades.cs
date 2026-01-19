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

    [Header("Tank Upgrade Price Curves")]
    public AnimationCurve tankHealthUpgradePriceCurve;
    public AnimationCurve tankDamageUpgradePriceCurve;
    public AnimationCurve tankRangeUpgradePriceCurve;

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

    [Header("Range Upgrade Price Curves")]
    public AnimationCurve rangeHealthUpgradePriceCurve;
    public AnimationCurve rangeDamageUpgradePriceCurve;
    public AnimationCurve rangeRangeUpgradePriceCurve;


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

    [Header("Upgrade Price Curves")]
    public AnimationCurve doubleTroubleUpgradePriceCurve;
    public AnimationCurve marketIncomeUpgradePriceCurve;

    private int tankHealthUpgradeLevel;
    private int tankDamageUpgradeLevel;
    private int tankRangeUpgradeLevel;
    private int rangeHealthUpgradeLevel;
    private int rangeDamageUpgradeLevel;
    private int rangeRangeUpgradeLevel;
    private int doubleTroubleUpgradeLevel;
    private int marketIncomeUpgradeLevel;

    void Start()
    {
        InitializeUpgradePrices();
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
            tankHealthUpgradeLevel++;
            tankHealthUpgradePrice = GetCurvePrice(tankHealthUpgradePriceCurve, tankHealthUpgradeLevel, tankHealthUpgradePrice);
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
            tankDamageUpgradeLevel++;
            tankDamageUpgradePrice = GetCurvePrice(tankDamageUpgradePriceCurve, tankDamageUpgradeLevel, tankDamageUpgradePrice);
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
            tankRangeUpgradeLevel++;
            tankRangeUpgradePrice = GetCurvePrice(tankRangeUpgradePriceCurve, tankRangeUpgradeLevel, tankRangeUpgradePrice);
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
            rangeHealthUpgradeLevel++;
            rangeHealthUpgradePrice = GetCurvePrice(rangeHealthUpgradePriceCurve, rangeHealthUpgradeLevel, rangeHealthUpgradePrice);
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
            rangeDamageUpgradeLevel++;
            rangeDamageUpgradePrice = GetCurvePrice(rangeDamageUpgradePriceCurve, rangeDamageUpgradeLevel, rangeDamageUpgradePrice);
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
            rangeRangeUpgradeLevel++;
            rangeRangeUpgradePrice = GetCurvePrice(rangeRangeUpgradePriceCurve, rangeRangeUpgradeLevel, rangeRangeUpgradePrice);
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
            doubleTroubleUpgradeLevel++;
            doubleTroubleUpgradePrice = GetCurvePrice(doubleTroubleUpgradePriceCurve, doubleTroubleUpgradeLevel, doubleTroubleUpgradePrice);
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
            marketIncomeUpgradeLevel++;
            marketIncomeUpgradePrice = GetCurvePrice(marketIncomeUpgradePriceCurve, marketIncomeUpgradeLevel, marketIncomeUpgradePrice);
            pm.marketIncomeMultiplier = 1f + marketIncomeBonusPercent / 100f;
            UpdateMarketIncomeUpgradeUI();
        }
    }
    private void UpdateMarketIncomeUpgradeUI()
    {
        marketIncomeUpgradeButtonText.text = $"Market Income ({marketIncomeUpgradePrice})";
        marketIncomeBonusText.text = $"Bonus: {marketIncomeBonusPercent}%";
    }

    private void InitializeUpgradePrices()
    {
        tankHealthUpgradePrice = GetCurvePrice(tankHealthUpgradePriceCurve, tankHealthUpgradeLevel, tankHealthUpgradePrice);
        tankDamageUpgradePrice = GetCurvePrice(tankDamageUpgradePriceCurve, tankDamageUpgradeLevel, tankDamageUpgradePrice);
        tankRangeUpgradePrice = GetCurvePrice(tankRangeUpgradePriceCurve, tankRangeUpgradeLevel, tankRangeUpgradePrice);

        rangeHealthUpgradePrice = GetCurvePrice(rangeHealthUpgradePriceCurve, rangeHealthUpgradeLevel, rangeHealthUpgradePrice);
        rangeDamageUpgradePrice = GetCurvePrice(rangeDamageUpgradePriceCurve, rangeDamageUpgradeLevel, rangeDamageUpgradePrice);
        rangeRangeUpgradePrice = GetCurvePrice(rangeRangeUpgradePriceCurve, rangeRangeUpgradeLevel, rangeRangeUpgradePrice);

        doubleTroubleUpgradePrice = GetCurvePrice(doubleTroubleUpgradePriceCurve, doubleTroubleUpgradeLevel, doubleTroubleUpgradePrice);
        marketIncomeUpgradePrice = GetCurvePrice(marketIncomeUpgradePriceCurve, marketIncomeUpgradeLevel, marketIncomeUpgradePrice);
    }

    private int GetCurvePrice(AnimationCurve curve, int level, int fallback)
    {
        if (curve == null || curve.length == 0)
        {
            return fallback;
        }

        return Mathf.Max(1, Mathf.RoundToInt(curve.Evaluate(level)));
    }
}

