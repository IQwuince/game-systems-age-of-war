using UnityEngine;
using TMPro;

public class Upgrades : MonoBehaviour
{
    [Header("Configuration")]
    public GameConfig gameConfig;
    
    [Header("References")]
    public PlayerMoney pm;
    public TurnSystem ts;

    // Soldier upgrade levels (starting at 1)
    [Header("Soldier Upgrade Levels")]
    public int soldierHealthLevel = 1;
    public int soldierDamageLevel = 1;

    // Tank upgrade levels
    [Header("Tank Upgrade Levels")]
    public int tankHealthLevel = 1;
    public int tankDamageLevel = 1;

    // Super Troop upgrade levels
    [Header("Super Troop Upgrade Levels")]
    public int superTroopHealthLevel = 1;
    public int superTroopDamageLevel = 1;

    // Soldier UI
    [Header("Soldier UI")]
    public TextMeshProUGUI soldierHealthText;
    public TextMeshProUGUI soldierDamageText;
    public TextMeshProUGUI soldierHealthUpgradeButtonText;
    public TextMeshProUGUI soldierDamageUpgradeButtonText;

    // Tank UI
    [Header("Tank UI")]
    public TextMeshProUGUI tankHealthText;
    public TextMeshProUGUI tankDamageText;
    public TextMeshProUGUI tankHealthUpgradeButtonText;
    public TextMeshProUGUI tankDamageUpgradeButtonText;

    // Super Troop UI
    [Header("Super Troop UI")]
    public TextMeshProUGUI superTroopHealthText;
    public TextMeshProUGUI superTroopDamageText;
    public TextMeshProUGUI superTroopHealthUpgradeButtonText;
    public TextMeshProUGUI superTroopDamageUpgradeButtonText;

    // Market Income Upgrade
    [Header("Market Income Upgrade")]
    public TextMeshProUGUI marketIncomeUpgradeButtonText;
    public TextMeshProUGUI marketIncomeBonusText;
    public int marketIncomeUpgradeLevel = 1;
    public float marketIncomeBonusPercent = 0f;

    // Current upgrade costs (calculated)
    private int soldierHealthUpgradeCost;
    private int soldierDamageUpgradeCost;
    private int tankHealthUpgradeCost;
    private int tankDamageUpgradeCost;
    private int superTroopHealthUpgradeCost;
    private int superTroopDamageUpgradeCost;
    private int marketIncomeUpgradeCost;

    void Start()
    {
        // Initialize market income upgrade cost from config
        if (gameConfig != null)
        {
            marketIncomeUpgradeCost = gameConfig.CalculateUpgradeCost(gameConfig.marketBaseCost, marketIncomeUpgradeLevel);
        }
        else
        {
            marketIncomeUpgradeCost = 30; // Fallback default
        }
        
        UpdateAllUpgradeCosts();
        UpdateAllStatsUI();
        UpdateAllUpgradeButtonTexts();
        UpdateMarketIncomeUpgradeUI();
        
        if (pm != null)
        {
            pm.marketIncomeMultiplier = 1f + marketIncomeBonusPercent / 100f;
        }
    }

    /// <summary>
    /// Recalculates all upgrade costs based on current levels.
    /// </summary>
    private void UpdateAllUpgradeCosts()
    {
        if (gameConfig == null) return;
        
        soldierHealthUpgradeCost = gameConfig.CalculateUpgradeCost(gameConfig.soldierHealthUpgradeBaseCost, soldierHealthLevel);
        soldierDamageUpgradeCost = gameConfig.CalculateUpgradeCost(gameConfig.soldierDamageUpgradeBaseCost, soldierDamageLevel);
        
        tankHealthUpgradeCost = gameConfig.CalculateUpgradeCost(gameConfig.tankHealthUpgradeBaseCost, tankHealthLevel);
        tankDamageUpgradeCost = gameConfig.CalculateUpgradeCost(gameConfig.tankDamageUpgradeBaseCost, tankDamageLevel);
        
        superTroopHealthUpgradeCost = gameConfig.CalculateUpgradeCost(gameConfig.superTroopHealthUpgradeBaseCost, superTroopHealthLevel);
        superTroopDamageUpgradeCost = gameConfig.CalculateUpgradeCost(gameConfig.superTroopDamageUpgradeBaseCost, superTroopDamageLevel);
    }

    // ===== SOLDIER UPGRADES =====
    
    public void UpgradeSoldierHealth()
    {
        if (pm.money >= soldierHealthUpgradeCost)
        {
            pm.SubtractMoney(soldierHealthUpgradeCost);
            soldierHealthLevel++;
            UpdateAllUpgradeCosts();
            UpdateAllStatsUI();
            UpdateAllUpgradeButtonTexts();
            pm.UpdateTroopCosts(); // Update troop purchase costs
        }
    }
    
    public void UpgradeSoldierDamage()
    {
        if (pm.money >= soldierDamageUpgradeCost)
        {
            pm.SubtractMoney(soldierDamageUpgradeCost);
            soldierDamageLevel++;
            UpdateAllUpgradeCosts();
            UpdateAllStatsUI();
            UpdateAllUpgradeButtonTexts();
            pm.UpdateTroopCosts();
        }
    }

    // ===== TANK UPGRADES =====
    
    public void UpgradeTankHealth()
    {
        if (pm.money >= tankHealthUpgradeCost)
        {
            pm.SubtractMoney(tankHealthUpgradeCost);
            tankHealthLevel++;
            UpdateAllUpgradeCosts();
            UpdateAllStatsUI();
            UpdateAllUpgradeButtonTexts();
            pm.UpdateTroopCosts();
        }
    }
    
    public void UpgradeTankDamage()
    {
        if (pm.money >= tankDamageUpgradeCost)
        {
            pm.SubtractMoney(tankDamageUpgradeCost);
            tankDamageLevel++;
            UpdateAllUpgradeCosts();
            UpdateAllStatsUI();
            UpdateAllUpgradeButtonTexts();
            pm.UpdateTroopCosts();
        }
    }

    // ===== SUPER TROOP UPGRADES =====
    
    public void UpgradeSuperTroopHealth()
    {
        if (pm.money >= superTroopHealthUpgradeCost)
        {
            pm.SubtractMoney(superTroopHealthUpgradeCost);
            superTroopHealthLevel++;
            UpdateAllUpgradeCosts();
            UpdateAllStatsUI();
            UpdateAllUpgradeButtonTexts();
            pm.UpdateTroopCosts();
        }
    }
    
    public void UpgradeSuperTroopDamage()
    {
        if (pm.money >= superTroopDamageUpgradeCost)
        {
            pm.SubtractMoney(superTroopDamageUpgradeCost);
            superTroopDamageLevel++;
            UpdateAllUpgradeCosts();
            UpdateAllStatsUI();
            UpdateAllUpgradeButtonTexts();
            pm.UpdateTroopCosts();
        }
    }

    // ===== MARKET INCOME UPGRADE =====
    
    public void UpgradeMarketIncome()
    {
        if (pm.money >= marketIncomeUpgradeCost)
        {
            pm.SubtractMoney(marketIncomeUpgradeCost);
            marketIncomeBonusPercent += 10f;
            marketIncomeUpgradeLevel++;
            marketIncomeUpgradeCost = gameConfig != null 
                ? gameConfig.CalculateUpgradeCost(gameConfig.marketBaseCost, marketIncomeUpgradeLevel) 
                : Mathf.RoundToInt(marketIncomeUpgradeCost * 1.35f); // Fallback scaling
            pm.marketIncomeMultiplier = 1f + marketIncomeBonusPercent / 100f;
            UpdateMarketIncomeUpgradeUI();
        }
    }

    // ===== UI UPDATES =====
    
    private void UpdateAllStatsUI()
    {
        if (gameConfig == null) return;
        
        // Get current stats from config
        var soldierStats = gameConfig.GetSoldierStats(soldierHealthLevel, soldierDamageLevel);
        var tankStats = gameConfig.GetTankStats(tankHealthLevel, tankDamageLevel);
        var superTroopStats = gameConfig.GetSuperTroopStats(superTroopHealthLevel, superTroopDamageLevel);
        
        // Soldier stats
        if (soldierHealthText != null)
            soldierHealthText.text = $"Health: {soldierStats.health}";
        if (soldierDamageText != null)
            soldierDamageText.text = $"Damage: {soldierStats.damage}";
        
        // Tank stats
        if (tankHealthText != null)
            tankHealthText.text = $"Health: {tankStats.health}";
        if (tankDamageText != null)
            tankDamageText.text = $"Damage: {tankStats.damage}";
        
        // Super Troop stats
        if (superTroopHealthText != null)
            superTroopHealthText.text = $"Health: {superTroopStats.health}";
        if (superTroopDamageText != null)
            superTroopDamageText.text = $"Damage: {superTroopStats.damage}";
    }

    private void UpdateAllUpgradeButtonTexts()
    {
        // Soldier upgrade buttons
        if (soldierHealthUpgradeButtonText != null)
            soldierHealthUpgradeButtonText.text = $"Upgrade ({soldierHealthUpgradeCost})";
        if (soldierDamageUpgradeButtonText != null)
            soldierDamageUpgradeButtonText.text = $"Upgrade ({soldierDamageUpgradeCost})";
        
        // Tank upgrade buttons
        if (tankHealthUpgradeButtonText != null)
            tankHealthUpgradeButtonText.text = $"Upgrade ({tankHealthUpgradeCost})";
        if (tankDamageUpgradeButtonText != null)
            tankDamageUpgradeButtonText.text = $"Upgrade ({tankDamageUpgradeCost})";
        
        // Super Troop upgrade buttons
        if (superTroopHealthUpgradeButtonText != null)
            superTroopHealthUpgradeButtonText.text = $"Upgrade ({superTroopHealthUpgradeCost})";
        if (superTroopDamageUpgradeButtonText != null)
            superTroopDamageUpgradeButtonText.text = $"Upgrade ({superTroopDamageUpgradeCost})";
    }

    private void UpdateMarketIncomeUpgradeUI()
    {
        if (marketIncomeUpgradeButtonText != null)
            marketIncomeUpgradeButtonText.text = $"Market Income ({marketIncomeUpgradeCost})";
        if (marketIncomeBonusText != null)
            marketIncomeBonusText.text = $"Bonus: {marketIncomeBonusPercent}%";
    }

    /// <summary>
    /// Get current Soldier stats (health, damage).
    /// </summary>
    public (int health, int damage) GetSoldierStats()
    {
        return gameConfig != null 
            ? gameConfig.GetSoldierStats(soldierHealthLevel, soldierDamageLevel)
            : (60, 12);
    }

    /// <summary>
    /// Get current Tank stats (health, damage).
    /// </summary>
    public (int health, int damage) GetTankStats()
    {
        return gameConfig != null 
            ? gameConfig.GetTankStats(tankHealthLevel, tankDamageLevel)
            : (160, 20);
    }

    /// <summary>
    /// Get current Super Troop stats (health, damage).
    /// </summary>
    public (int health, int damage) GetSuperTroopStats()
    {
        return gameConfig != null 
            ? gameConfig.GetSuperTroopStats(superTroopHealthLevel, superTroopDamageLevel)
            : (220, 45);
    }
}
