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

    [Header("Unit Count UI")]
    public TextMeshProUGUI soldierText;
    public TextMeshProUGUI tankText;
    public TextMeshProUGUI superTroopText;

    [Header("Unit Cost UI")]
    public TextMeshProUGUI soldierCostText;
    public TextMeshProUGUI tankCostText;
    public TextMeshProUGUI superTroopCostText;

    [Header("Market")]
    public int money = 0;
    public int Markets = 0;

    [Header("Upgrades")]
    public float marketIncomeMultiplier = 1f;

    private int currentSoldierCost;
    private int currentTankCost;
    private int currentSuperTroopCost;
    private int currentMarketCost;

    void Start()
    {
        if (gameConfig != null)
        {
            money = gameConfig.startingMoney;
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
        UpdateTroopCosts(); // Ensure cost is current
        if (money >= currentSoldierCost)
        {
            money -= currentSoldierCost;
            soldierCount++;
            UpdateUI();
        }
    }

    public void BuyTank()
    {
        UpdateTroopCosts(); // Ensure cost is current
        if (money >= currentTankCost)
        {
            money -= currentTankCost;
            tankCount++;
            UpdateUI();
        }
    }

    public void BuySuperTroop()
    {
        UpdateTroopCosts(); // Ensure cost is current
        if (money >= currentSuperTroopCost)
        {
            money -= currentSuperTroopCost;
            superTroopCount++;
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
        
        return totalScore;
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
            
        if (combatScoreText != null)
            combatScoreText.text = $"Combat Score: {GetTotalCombatScore()}";
    }
}
