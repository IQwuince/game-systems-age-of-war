using UnityEngine;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public PlayerMoney pm;
    public TurnSystem ts;

    // Troop 1
    [Header("Troop 1")]
    public int troop1Health = 100;
    public int troop1Damage = 20;
    public float troop1Range = 0f;

    public TextMeshProUGUI troop1HealthText;
    public TextMeshProUGUI troop1DamageText;
    public TextMeshProUGUI troop1RangeText;

    public TextMeshProUGUI troop1HealthUpgradeButtonText;
    public TextMeshProUGUI troop1DamageUpgradeButtonText;
    public TextMeshProUGUI troop1RangeUpgradeButtonText;

    private int troop1HealthUpgradePrice = 4;
    private int troop1DamageUpgradePrice = 4;
    private int troop1RangeUpgradePrice = 4;

    // Troop 2
    [Header("Troop 2")]
    public int troop2Health = 120;
    public int troop2Damage = 15;
    public float troop2Range = 0f;

    public TextMeshProUGUI troop2HealthText;
    public TextMeshProUGUI troop2DamageText;
    public TextMeshProUGUI troop2RangeText;

    public TextMeshProUGUI troop2HealthUpgradeButtonText;
    public TextMeshProUGUI troop2DamageUpgradeButtonText;
    public TextMeshProUGUI troop2RangeUpgradeButtonText;

    private int troop2HealthUpgradePrice = 4;
    private int troop2DamageUpgradePrice = 4;
    private int troop2RangeUpgradePrice = 4;

    // Troop 3
    [Header("Troop 3")]
    public int troop3Health = 80;
    public int troop3Damage = 25;
    public float troop3Range = 5.0f;

    public TextMeshProUGUI troop3HealthText;
    public TextMeshProUGUI troop3DamageText;
    public TextMeshProUGUI troop3RangeText;

    public TextMeshProUGUI troop3HealthUpgradeButtonText;
    public TextMeshProUGUI troop3DamageUpgradeButtonText;
    public TextMeshProUGUI troop3RangeUpgradeButtonText;

    private int troop3HealthUpgradePrice = 4;
    private int troop3DamageUpgradePrice = 4;
    private int troop3RangeUpgradePrice = 4;

    // Troop 4
    [Header("Troop 4")]
    public int troop4Health = 150;
    public int troop4Damage = 10;
    public float troop4Range = 0f;

    public TextMeshProUGUI troop4HealthText;
    public TextMeshProUGUI troop4DamageText;
    public TextMeshProUGUI troop4RangeText;

    public TextMeshProUGUI troop4HealthUpgradeButtonText;
    public TextMeshProUGUI troop4DamageUpgradeButtonText;
    public TextMeshProUGUI troop4RangeUpgradeButtonText;

    private int troop4HealthUpgradePrice = 4;
    private int troop4DamageUpgradePrice = 4;
    private int troop4RangeUpgradePrice = 4;

    [Header("Double Trouble Upgrade")]
    public TextMeshProUGUI doubleTroubleButtonText;
    public int doubleTroubleUpgradePrice = 10; 
    public float doubleTroubleChance = 0f; // percent, e.g. 5 means 5%
    public TextMeshProUGUI doubleTroubleChanceText;

    void Start()
    {
        UpdateTroopStatsUI();
        UpdateUpgradeButtonTexts();
        UpdateDoubleTroubleUI();
        pm.doubleTroubleChance = doubleTroubleChance;
    }

    // Troop 1 upgrades
    public void UpgradeTroop1Health()
    {
        if (pm.money >= troop1HealthUpgradePrice)
        {
            pm.SubtractMoney(troop1HealthUpgradePrice);
            troop1Health += 10;
            troop1HealthUpgradePrice += 2;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }
    public void UpgradeTroop1Damage()
    {
        if (pm.money >= troop1DamageUpgradePrice)
        {
            pm.SubtractMoney(troop1DamageUpgradePrice);
            troop1Damage += 2;
            troop1DamageUpgradePrice += 2;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }
    public void UpgradeTroop1Range()
    {
        if (pm.money >= troop1RangeUpgradePrice)
        {
            pm.SubtractMoney(troop1RangeUpgradePrice);
            troop1Range += 1f;
            troop1RangeUpgradePrice += 2;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }

    // Troop 2 upgrades
    public void UpgradeTroop2Health()
    {
        if (pm.money >= troop2HealthUpgradePrice)
        {
            pm.SubtractMoney(troop2HealthUpgradePrice);
            troop2Health += 10;
            troop2HealthUpgradePrice += 2;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }
    public void UpgradeTroop2Damage()
    {
        if (pm.money >= troop2DamageUpgradePrice)
        {
            pm.SubtractMoney(troop2DamageUpgradePrice);
            troop2Damage += 2;
            troop2DamageUpgradePrice += 2;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }
    public void UpgradeTroop2Range()
    {
        if (pm.money >= troop2RangeUpgradePrice)
        {
            pm.SubtractMoney(troop2RangeUpgradePrice);
            troop2Range += 1f;
            troop2RangeUpgradePrice += 2;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }

    // Troop 3 upgrades
    public void UpgradeTroop3Health()
    {
        if (pm.money >= troop3HealthUpgradePrice)
        {
            pm.SubtractMoney(troop3HealthUpgradePrice);
            troop3Health += 10;
            troop3HealthUpgradePrice += 2;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }
    public void UpgradeTroop3Damage()
    {
        if (pm.money >= troop3DamageUpgradePrice)
        {
            pm.SubtractMoney(troop3DamageUpgradePrice);
            troop3Damage += 2;
            troop3DamageUpgradePrice += 2;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }
    public void UpgradeTroop3Range()
    {
        if (pm.money >= troop3RangeUpgradePrice)
        {
            pm.SubtractMoney(troop3RangeUpgradePrice);
            troop3Range += 1f;
            troop3RangeUpgradePrice += 2;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }

    // Troop 4 upgrades
    public void UpgradeTroop4Health()
    {
        if (pm.money >= troop4HealthUpgradePrice)
        {
            pm.SubtractMoney(troop4HealthUpgradePrice);
            troop4Health += 10;
            troop4HealthUpgradePrice += 2;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }
    public void UpgradeTroop4Damage()
    {
        if (pm.money >= troop4DamageUpgradePrice)
        {
            pm.SubtractMoney(troop4DamageUpgradePrice);
            troop4Damage += 2;
            troop4DamageUpgradePrice += 2;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }
    public void UpgradeTroop4Range()
    {
        if (pm.money >= troop4RangeUpgradePrice)
        {
            pm.SubtractMoney(troop4RangeUpgradePrice);
            troop4Range += 1f;
            troop4RangeUpgradePrice += 2;
            UpdateTroopStatsUI();
            UpdateUpgradeButtonTexts();
        }
    }

    public void UpdateTroopStatsUI()
    {
        troop1HealthText.text = $"Health: {troop1Health}";
        troop1DamageText.text = $"Damage: {troop1Damage}";
        troop1RangeText.text = $"Range: {troop1Range}";

        troop2HealthText.text = $"Health: {troop2Health}";
        troop2DamageText.text = $"Damage: {troop2Damage}";
        troop2RangeText.text = $"Range: {troop2Range}";

        troop3HealthText.text = $"Health: {troop3Health}";
        troop3DamageText.text = $"Damage: {troop3Damage}";
        troop3RangeText.text = $"Range: {troop3Range}";

        troop4HealthText.text = $"Health: {troop4Health}";
        troop4DamageText.text = $"Damage: {troop4Damage}";
        troop4RangeText.text = $"Range: {troop4Range}";
    }

    private void UpdateUpgradeButtonTexts()
    {
        troop1HealthUpgradeButtonText.text = $"Upgrade ({troop1HealthUpgradePrice})";
        troop1DamageUpgradeButtonText.text = $"Upgrade ({troop1DamageUpgradePrice})";
        troop1RangeUpgradeButtonText.text = $"Upgrade ({troop1RangeUpgradePrice})";

        troop2HealthUpgradeButtonText.text = $"Upgrade ({troop2HealthUpgradePrice})";
        troop2DamageUpgradeButtonText.text = $"Upgrade ({troop2DamageUpgradePrice})";
        troop2RangeUpgradeButtonText.text = $"Upgrade ({troop2RangeUpgradePrice})";

        troop3HealthUpgradeButtonText.text = $"Upgrade ({troop3HealthUpgradePrice})";
        troop3DamageUpgradeButtonText.text = $"Upgrade ({troop3DamageUpgradePrice})";
        troop3RangeUpgradeButtonText.text = $"Upgrade ({troop3RangeUpgradePrice})";

        troop4HealthUpgradeButtonText.text = $"Upgrade ({troop4HealthUpgradePrice})";
        troop4DamageUpgradeButtonText.text = $"Upgrade ({troop4DamageUpgradePrice})";
        troop4RangeUpgradeButtonText.text = $"Upgrade ({troop4RangeUpgradePrice})";
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
}
