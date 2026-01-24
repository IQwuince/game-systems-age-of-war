using UnityEngine;

/// <summary>
/// Centralized configuration for all game balancing values.
/// All costs, stats, and scaling factors are easily tweakable in one place.
/// </summary>
[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/Config", order = 1)]
public class GameConfig : ScriptableObject
{
    [Header("Game Settings")]
    [Tooltip("Total number of build rounds before determining winner")]
    public int totalRounds = 10;
    
    [Tooltip("Starting money for each player")]
    public int startingMoney = 100;
    
    [Tooltip("Base income per market per round")]
    public int baseMarketIncome = 20;

    [Header("Scaling Formulas")]
    [Tooltip("Multiplier for upgrade cost scaling: BaseCost * (upgradeScalingBase ^ (Level - 1))")]
    public float upgradeScalingBase = 1.35f;
    
    [Tooltip("Multiplier for troop cost scaling when buying higher level troops: BaseCost * (troopScalingBase ^ (HighestUpgradeLevel - 1))")]
    public float troopScalingBase = 1.4f;

    [Header("Market Settings")]
    [Tooltip("Base cost to buy a market")]
    public int marketBaseCost = 50;
    
    [Tooltip("Market cost scaling base (same formula as upgrade scaling)")]
    public float marketScalingBase = 1.35f;

    [Header("=== SOLDIER ===")]
    [Tooltip("Base cost to buy a Soldier at level 1")]
    public int soldierBaseCost = 30;
    [Tooltip("Base health of Soldier at level 1")]
    public int soldierBaseHealth = 60;
    [Tooltip("Base damage of Soldier at level 1")]
    public int soldierBaseDamage = 12;
    
    [Header("Soldier Upgrade Costs")]
    [Tooltip("Base cost to upgrade Soldier health")]
    public int soldierHealthUpgradeBaseCost = 18;
    [Tooltip("Base cost to upgrade Soldier damage")]
    public int soldierDamageUpgradeBaseCost = 22;
    
    [Header("Soldier Stat Gains Per Level")]
    [Tooltip("Health gained per upgrade level for Soldier")]
    public int soldierHealthPerLevel = 12;
    [Tooltip("Damage gained per upgrade level for Soldier")]
    public int soldierDamagePerLevel = 4;

    [Header("=== TANK ===")]
    [Tooltip("Base cost to buy a Tank at level 1")]
    public int tankBaseCost = 80;
    [Tooltip("Base health of Tank at level 1")]
    public int tankBaseHealth = 160;
    [Tooltip("Base damage of Tank at level 1")]
    public int tankBaseDamage = 20;
    
    [Header("Tank Upgrade Costs")]
    [Tooltip("Base cost to upgrade Tank health")]
    public int tankHealthUpgradeBaseCost = 45;
    [Tooltip("Base cost to upgrade Tank damage")]
    public int tankDamageUpgradeBaseCost = 55;
    
    [Header("Tank Stat Gains Per Level")]
    [Tooltip("Health gained per upgrade level for Tank")]
    public int tankHealthPerLevel = 28;
    [Tooltip("Damage gained per upgrade level for Tank")]
    public int tankDamagePerLevel = 6;

    [Header("=== SUPER TROOP ===")]
    [Tooltip("Base cost to buy a Super Troop at level 1")]
    public int superTroopBaseCost = 160;
    [Tooltip("Base health of Super Troop at level 1")]
    public int superTroopBaseHealth = 220;
    [Tooltip("Base damage of Super Troop at level 1")]
    public int superTroopBaseDamage = 45;
    
    [Header("Super Troop Upgrade Costs")]
    [Tooltip("Base cost to upgrade Super Troop health")]
    public int superTroopHealthUpgradeBaseCost = 85;
    [Tooltip("Base cost to upgrade Super Troop damage")]
    public int superTroopDamageUpgradeBaseCost = 95;
    
    [Header("Super Troop Stat Gains Per Level")]
    [Tooltip("Health gained per upgrade level for Super Troop")]
    public int superTroopHealthPerLevel = 35;
    [Tooltip("Damage gained per upgrade level for Super Troop")]
    public int superTroopDamagePerLevel = 10;

    [Header("=== DOUBLE TROUBLE ===")]
    [Tooltip("Base cost to upgrade Double Trouble")]
    public int doubleTroubleBaseCost = 40;
    [Tooltip("Percentage gained per upgrade level for Double Trouble")]
    public float doubleTroublePercentPerLevel = 10f;
    [Tooltip("Maximum percentage for Double Trouble (default 70%)")]
    public float doubleTroubleMaxPercent = 70f;

    [Header("=== PLAYER HEALTH ===")]
    [Tooltip("Starting health for each player")]
    public int playerStartingHealth = 100;
    
    [Tooltip("Multiplier for damage taken when below minimum combat score")]
    public float damageMultiplier = 0.5f;
    
    [Tooltip("Combat score penalty per missing health point (e.g., 0.001 = 0.1% penalty per missing health)")]
    public float penaltyPerMissingHealth = 0.001f;

    [Header("=== MINIMUM COMBAT SCORE ===")]
    [Tooltip("Base minimum combat score for Round 1")]
    public int minimumCombatScoreBase = 100;
    
    [Tooltip("Additional minimum combat score added per round after Round 1")]
    public int minimumCombatScorePerRound = 200;
    
    [Tooltip("Enable the MSC system (either as reward or damage based on useMinimumCombatScoreAsDamage)")]
    public bool useMinimumCombatScore = true;
    
    [Tooltip("If true, players below MSC take damage (legacy). If false, players above MSC get gold (new reward flow).")]
    public bool useMinimumCombatScoreAsDamage = false;
    
    [Header("MSC Reward Settings")]
    [Tooltip("Gold rewarded per 100 combat score above the minimum MSC")]
    public float bonusGoldPer100CombatScore = 2f;
    
    [Tooltip("Maximum bonus gold a player can get in a single round")]
    public int maxBonusGoldPerRound = 100;

    [Header("=== SELL PRICES ===")]
    [Tooltip("Sell price as percentage of buy cost for Soldiers")]
    [Range(0f, 1f)] public float soldierSellPercent = 0.5f;
    
    [Tooltip("Sell price as percentage of buy cost for Tanks")]
    [Range(0f, 1f)] public float tankSellPercent = 0.5f;
    
    [Tooltip("Sell price as percentage of buy cost for Super Troops")]
    [Range(0f, 1f)] public float superTroopSellPercent = 0.5f;
    
    [Tooltip("Sell price as percentage of buy cost for Markets")]
    [Range(0f, 1f)] public float marketSellPercent = 0.8f;

    /// <summary>
    /// Calculate upgrade cost using the formula: BaseCost * (upgradeScalingBase ^ (Level - 1))
    /// </summary>
    public int CalculateUpgradeCost(int baseCost, int level)
    {
        if (level <= 1) return baseCost;
        return Mathf.RoundToInt(baseCost * Mathf.Pow(upgradeScalingBase, level - 1));
    }

    /// <summary>
    /// Calculate troop purchase cost when buying at higher upgrade levels.
    /// Formula: BaseCost * (troopScalingBase ^ (HighestUpgradeLevel - 1))
    /// </summary>
    public int CalculateTroopCost(int baseCost, int highestUpgradeLevel)
    {
        if (highestUpgradeLevel <= 1) return baseCost;
        return Mathf.RoundToInt(baseCost * Mathf.Pow(troopScalingBase, highestUpgradeLevel - 1));
    }

    /// <summary>
    /// Calculate market purchase cost using scaling formula.
    /// </summary>
    public int CalculateMarketCost(int marketCount)
    {
        if (marketCount <= 0) return marketBaseCost;
        return Mathf.RoundToInt(marketBaseCost * Mathf.Pow(marketScalingBase, marketCount));
    }

    /// <summary>
    /// Get current stats for Soldier based on upgrade level.
    /// </summary>
    public (int health, int damage) GetSoldierStats(int healthLevel, int damageLevel)
    {
        int health = soldierBaseHealth + (healthLevel - 1) * soldierHealthPerLevel;
        int damage = soldierBaseDamage + (damageLevel - 1) * soldierDamagePerLevel;
        return (health, damage);
    }

    /// <summary>
    /// Get current stats for Tank based on upgrade level.
    /// </summary>
    public (int health, int damage) GetTankStats(int healthLevel, int damageLevel)
    {
        int health = tankBaseHealth + (healthLevel - 1) * tankHealthPerLevel;
        int damage = tankBaseDamage + (damageLevel - 1) * tankDamagePerLevel;
        return (health, damage);
    }

    /// <summary>
    /// Get current stats for Super Troop based on upgrade level.
    /// </summary>
    public (int health, int damage) GetSuperTroopStats(int healthLevel, int damageLevel)
    {
        int health = superTroopBaseHealth + (healthLevel - 1) * superTroopHealthPerLevel;
        int damage = superTroopBaseDamage + (damageLevel - 1) * superTroopDamagePerLevel;
        return (health, damage);
    }

    /// <summary>
    /// Calculate Combat Score for a single troop (Health + Damage).
    /// </summary>
    public int CalculateTroopCombatScore(int health, int damage)
    {
        return health + damage;
    }

    /// <summary>
    /// Calculate the current Double Trouble percentage based on upgrade level.
    /// Returns a value between 0 and doubleTroubleMaxPercent.
    /// </summary>
    public float CalculateDoubleTroublePercent(int level)
    {
        if (level <= 1) return 0f;
        float percent = (level - 1) * doubleTroublePercentPerLevel;
        return Mathf.Min(percent, doubleTroubleMaxPercent);
    }

    /// <summary>
    /// Get the minimum combat score required for a specific round.
    /// Formula: minimumCombatScoreBase + (round - 1) * minimumCombatScorePerRound
    /// Example: Round 1 = 100, Round 2 = 300, Round 3 = 500 (with defaults)
    /// </summary>
    public int GetMinimumCombatScoreForRound(int round)
    {
        if (round <= 1) return minimumCombatScoreBase;
        return minimumCombatScoreBase + (round - 1) * minimumCombatScorePerRound;
    }

    /// <summary>
    /// Calculate damage taken when player's combat score is below the minimum.
    /// Formula: damage = (minimumScore - playerScore) * damageMultiplier
    /// </summary>
    public int CalculateDamageFromLowScore(int minimumScore, int playerScore)
    {
        if (playerScore >= minimumScore) return 0;
        return Mathf.RoundToInt((minimumScore - playerScore) * damageMultiplier);
    }

    /// <summary>
    /// Apply health-based combat score penalty.
    /// Formula: adjustedScore = combatScore * (1 - ((maxHealth - currentHealth) * penaltyPerMissingHealth))
    /// Returns the adjusted combat score (reduced if health is below max).
    /// </summary>
    public int ApplyHealthPenalty(int combatScore, int currentHealth)
    {
        if (currentHealth >= playerStartingHealth) return combatScore;
        
        int missingHealth = playerStartingHealth - currentHealth;
        float penaltyMultiplier = 1f - (missingHealth * penaltyPerMissingHealth);
        
        // Ensure penalty doesn't go below 0
        penaltyMultiplier = Mathf.Clamp01(penaltyMultiplier);
        
        return Mathf.RoundToInt(combatScore * penaltyMultiplier);
    }

    /// <summary>
    /// Calculate bonus gold for exceeding the minimum combat score.
    /// Formula: bonus = ((playerScore - minimumScore) / 100) * bonusGoldPer100CombatScore
    /// Capped at maxBonusGoldPerRound.
    /// </summary>
    public int CalculateBonusGoldFromScore(int minimumScore, int playerScore)
    {
        if (playerScore <= minimumScore) return 0;
        int diff = playerScore - minimumScore;
        float rawBonus = (diff / 100f) * bonusGoldPer100CombatScore;
        int bonus = Mathf.FloorToInt(rawBonus);
        return Mathf.Min(bonus, maxBonusGoldPerRound);
    }

    /// <summary>
    /// Calculate sell price for a Soldier based on current upgrade level.
    /// </summary>
    public int GetSoldierSellPrice(int highestUpgradeLevel)
    {
        int buyCost = CalculateTroopCost(soldierBaseCost, highestUpgradeLevel);
        return Mathf.RoundToInt(buyCost * soldierSellPercent);
    }

    /// <summary>
    /// Calculate sell price for a Tank based on current upgrade level.
    /// </summary>
    public int GetTankSellPrice(int highestUpgradeLevel)
    {
        int buyCost = CalculateTroopCost(tankBaseCost, highestUpgradeLevel);
        return Mathf.RoundToInt(buyCost * tankSellPercent);
    }

    /// <summary>
    /// Calculate sell price for a Super Troop based on current upgrade level.
    /// </summary>
    public int GetSuperTroopSellPrice(int highestUpgradeLevel)
    {
        int buyCost = CalculateTroopCost(superTroopBaseCost, highestUpgradeLevel);
        return Mathf.RoundToInt(buyCost * superTroopSellPercent);
    }

    /// <summary>
    /// Calculate sell price for a Market based on market index.
    /// </summary>
    public int CalculateMarketSellPrice(int marketIndex)
    {
        int buyCost = CalculateMarketCost(marketIndex);
        return Mathf.RoundToInt(buyCost * marketSellPercent);
    }
}
