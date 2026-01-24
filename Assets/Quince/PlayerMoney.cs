using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMoney : MonoBehaviour
{
    [Header("Configuration")]
    public GameConfig gameConfig;
    public Upgrades upgrades;

    [Header("UI Texts")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI marketText;
    public TextMeshProUGUI marketPriceText;
    public TextMeshProUGUI combatScoreText;

    [Header("Unit Counts")]
    public int soldierCount = 0;
    public int tankCount = 0;
    public int superTroopCount = 0;

    [Header("Player Health")]
    public int health = 100;
    public TextMeshProUGUI healthText;

    [Header("Unit Count UI")]
    public TextMeshProUGUI soldierText;
    public TextMeshProUGUI tankText;
    public TextMeshProUGUI superTroopText;

    [Header("Unit Cost UI")]
    public TextMeshProUGUI soldierCostText;
    public TextMeshProUGUI tankCostText;
    public TextMeshProUGUI superTroopCostText;

    [Header("Unit Sell Price UI")]
    public TextMeshProUGUI soldierSellText;
    public TextMeshProUGUI tankSellText;
    public TextMeshProUGUI superTroopSellText;
    public TextMeshProUGUI marketSellText;

    [Header("Market")]
    public int money = 0;
    public int Markets = 0;

    [Header("Upgrades")]
    public float marketIncomeMultiplier = 1f;
    public float doubleTroublePercent = 0f;

    private int currentSoldierCost;
    private int currentTankCost;
    private int currentSuperTroopCost;
    private int currentMarketCost;

    /// <summary>
    /// Check if Double Trouble should trigger and return bonus troop count.
    /// </summary>
    private bool ShouldApplyDoubleTrouble()
    {
        return doubleTroublePercent > 0f && Random.Range(0f, 100f) < doubleTroublePercent;
    }

    void Start()
    {
        if (gameConfig != null)
        {
            money = gameConfig.startingMoney;
            health = gameConfig.playerStartingHealth;
        }
        UpdateTroopCosts();
        UpdateUI();
    }

    /// <summary>
    /// Updates troop costs based on current upgrade levels.
    /// </summary>
    public void UpdateTroopCosts()
    {
        if (gameConfig == null) return;
        
        int highestSoldierLevel = upgrades != null ? Mathf.Max(upgrades.soldierHealthLevel, upgrades.soldierDamageLevel) : 1;
        int highestTankLevel = upgrades != null ? Mathf.Max(upgrades.tankHealthLevel, upgrades.tankDamageLevel) : 1;
        int highestSuperTroopLevel = upgrades != null ? Mathf.Max(upgrades.superTroopHealthLevel, upgrades.superTroopDamageLevel) : 1;
        
        currentSoldierCost = gameConfig.CalculateTroopCost(gameConfig.soldierBaseCost, highestSoldierLevel);
        currentTankCost = gameConfig.CalculateTroopCost(gameConfig.tankBaseCost, highestTankLevel);
        currentSuperTroopCost = gameConfig.CalculateTroopCost(gameConfig.superTroopBaseCost, highestSuperTroopLevel);
        currentMarketCost = gameConfig.CalculateMarketCost(Markets);
        
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
        if (money >= currentMarketCost)
        {
            money -= currentMarketCost;
            Markets++;
            currentMarketCost = gameConfig.CalculateMarketCost(Markets);
            UpdateUI();
        }
    }

    public void BuySoldier()
    {
        if (money >= currentSoldierCost)
        {
            money -= currentSoldierCost;
            soldierCount++;
            
            // Apply Double Trouble chance
            if (ShouldApplyDoubleTrouble())
            {
                soldierCount++; // Bonus troop!
            }
            
            UpdateUI();
        }
    }

    public void BuyTank()
    {
        if (money >= currentTankCost)
        {
            money -= currentTankCost;
            tankCount++;
            
            // Apply Double Trouble chance
            if (ShouldApplyDoubleTrouble())
            {
                tankCount++; // Bonus troop!
            }
            
            UpdateUI();
        }
    }

    public void BuySuperTroop()
    {
        if (money >= currentSuperTroopCost)
        {
            money -= currentSuperTroopCost;
            superTroopCount++;
            
            // Apply Double Trouble chance
            if (ShouldApplyDoubleTrouble())
            {
                superTroopCount++; // Bonus troop!
            }
            
            UpdateUI();
        }
    }

    public void RemoveSoldier()
    {
        if (soldierCount > 0)
        {
            soldierCount--;
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

    public void RemoveSuperTroop()
    {
        if (superTroopCount > 0)
        {
            superTroopCount--;
            UpdateUI();
        }
    }

    /// <summary>
    /// Sell a Soldier and receive gold based on sell price percentage.
    /// </summary>
    public void SellSoldier()
    {
        if (soldierCount <= 0) return;
        int sellPrice = 0;
        if (gameConfig != null && upgrades != null)
        {
            int highestLvl = Mathf.Max(upgrades.soldierHealthLevel, upgrades.soldierDamageLevel);
            sellPrice = gameConfig.GetSoldierSellPrice(highestLvl);
        }
        soldierCount--;
        AddMoney(sellPrice);
        UpdateUI();
    }

    /// <summary>
    /// Sell a Tank and receive gold based on sell price percentage.
    /// </summary>
    public void SellTank()
    {
        if (tankCount <= 0) return;
        int sellPrice = 0;
        if (gameConfig != null && upgrades != null)
        {
            int highestLvl = Mathf.Max(upgrades.tankHealthLevel, upgrades.tankDamageLevel);
            sellPrice = gameConfig.GetTankSellPrice(highestLvl);
        }
        tankCount--;
        AddMoney(sellPrice);
        UpdateUI();
    }

    /// <summary>
    /// Sell a Super Troop and receive gold based on sell price percentage.
    /// </summary>
    public void SellSuperTroop()
    {
        if (superTroopCount <= 0) return;
        int sellPrice = 0;
        if (gameConfig != null && upgrades != null)
        {
            int highestLvl = Mathf.Max(upgrades.superTroopHealthLevel, upgrades.superTroopDamageLevel);
            sellPrice = gameConfig.GetSuperTroopSellPrice(highestLvl);
        }
        superTroopCount--;
        AddMoney(sellPrice);
        UpdateUI();
    }

    /// <summary>
    /// Sell a Market and receive gold based on sell price percentage.
    /// </summary>
    public void SellMarket()
    {
        if (Markets <= 0) return;
        int sellPrice = 0;
        if (gameConfig != null)
        {
            // Pass index of last market (Markets-1) so market cost matches purchase cost scale
            sellPrice = gameConfig.CalculateMarketSellPrice(Markets - 1);
        }
        Markets--;
        AddMoney(sellPrice);
        UpdateUI();
    }

    public void CollectMarketIncome()
    {
        if (gameConfig != null && Markets > 0)
        {
            int totalIncome = Mathf.RoundToInt(Markets * gameConfig.baseMarketIncome * marketIncomeMultiplier);
            AddMoney(totalIncome);
        }
    }

    /// <summary>
    /// Calculate total combat score for all troops.
    /// Combat Score = (Health + Damage) for each troop.
    /// </summary>
    public int GetTotalCombatScore()
    {
        if (gameConfig == null || upgrades == null) return 0;
        
        int totalScore = 0;
        
        // Soldier combat score
        var soldierStats = gameConfig.GetSoldierStats(upgrades.soldierHealthLevel, upgrades.soldierDamageLevel);
        int soldierCombatScore = gameConfig.CalculateTroopCombatScore(soldierStats.health, soldierStats.damage);
        totalScore += soldierCount * soldierCombatScore;
        
        // Tank combat score
        var tankStats = gameConfig.GetTankStats(upgrades.tankHealthLevel, upgrades.tankDamageLevel);
        int tankCombatScore = gameConfig.CalculateTroopCombatScore(tankStats.health, tankStats.damage);
        totalScore += tankCount * tankCombatScore;
        
        // Super Troop combat score
        var superTroopStats = gameConfig.GetSuperTroopStats(upgrades.superTroopHealthLevel, upgrades.superTroopDamageLevel);
        int superTroopCombatScore = gameConfig.CalculateTroopCombatScore(superTroopStats.health, superTroopStats.damage);
        totalScore += superTroopCount * superTroopCombatScore;
        
        // Only apply health penalty when legacy health-based penalties are enabled
        if (gameConfig.playerStartingHealth > 0 && gameConfig.useMinimumCombatScoreAsDamage)
        {
            totalScore = gameConfig.ApplyHealthPenalty(totalScore, health);
        }
        
        return totalScore;
    }

    /// <summary>
    /// Apply damage to the player's health.
    /// </summary>
    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
        UpdateUI();
    }

    /// <summary>
    /// Check if the player is still alive.
    /// </summary>
    public bool IsAlive()
    {
        return health > 0;
    }

    /// <summary>
    /// Get the player's current health.
    /// </summary>
    public int GetHealth()
    {
        return health;
    }

    private void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = $"Money: {money}";
        if (marketText != null)
            marketText.text = $"Markets: {Markets}";
        if (marketPriceText != null)
            marketPriceText.text = $"Buy ({currentMarketCost})";

        if (soldierText != null)
            soldierText.text = $"Soldier: {soldierCount}";
        if (tankText != null)
            tankText.text = $"Tank: {tankCount}";
        if (superTroopText != null)
            superTroopText.text = $"Super Troop: {superTroopCount}";

        if (soldierCostText != null)
            soldierCostText.text = $"Soldier: {currentSoldierCost}";
        if (tankCostText != null)
            tankCostText.text = $"Tank: {currentTankCost}";
        if (superTroopCostText != null)
            superTroopCostText.text = $"Super Troop: {currentSuperTroopCost}";

        // Update sell price texts
        if (gameConfig != null && upgrades != null)
        {
            if (soldierSellText != null)
            {
                int soldierSellPrice = gameConfig.GetSoldierSellPrice(Mathf.Max(upgrades.soldierHealthLevel, upgrades.soldierDamageLevel));
                soldierSellText.text = $"Sell: {soldierSellPrice}";
            }
            if (tankSellText != null)
            {
                int tankSellPrice = gameConfig.GetTankSellPrice(Mathf.Max(upgrades.tankHealthLevel, upgrades.tankDamageLevel));
                tankSellText.text = $"Sell: {tankSellPrice}";
            }
            if (superTroopSellText != null)
            {
                int superTroopSellPrice = gameConfig.GetSuperTroopSellPrice(Mathf.Max(upgrades.superTroopHealthLevel, upgrades.superTroopDamageLevel));
                superTroopSellText.text = $"Sell: {superTroopSellPrice}";
            }
        }
        if (marketSellText != null && gameConfig != null && Markets > 0)
        {
            int marketSellPrice = gameConfig.CalculateMarketSellPrice(Markets - 1);
            marketSellText.text = $"Sell: {marketSellPrice}";
        }
        else if (marketSellText != null)
        {
            marketSellText.text = "Sell: 0";
        }
            
        if (combatScoreText != null)
            combatScoreText.text = $"Combat Score: {GetTotalCombatScore()}";
            
        if (healthText != null)
            healthText.text = $"Health: {health}";
    }
}
