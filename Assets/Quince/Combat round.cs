using UnityEngine;
using TMPro;

public class Combatround : MonoBehaviour
{
    [Header("Player References")]
    public PlayerMoney player1Money;
    public PlayerMoney player2Money;
    public Upgrades player1Upgrades;
    public Upgrades player2Upgrades;
    public TurnSystem turnSystem;

    [Header("Combat Score UI")]
    public TextMeshProUGUI player1CombatScoreText;
    public TextMeshProUGUI player2CombatScoreText;
    public TextMeshProUGUI player1MinimumCombatScoreText;
    public TextMeshProUGUI player2MinimumCombatScoreText;

    [Header("Player Health UI")]
    public TextMeshProUGUI player1HealthText;
    public TextMeshProUGUI player2HealthText;

    [Header("MSC Bonus UI")]
    public TextMeshProUGUI player1MSCBonusText;
    public TextMeshProUGUI player2MSCBonusText;

    // UI Texts for player 1 troop counts
    [Header("Player 1 Troop Count UI")]
    public TextMeshProUGUI player1SoldierText;
    public TextMeshProUGUI player1TankText;
    public TextMeshProUGUI player1SuperTroopText;

    // UI Texts for player 2 troop counts
    [Header("Player 2 Troop Count UI")]
    public TextMeshProUGUI player2SoldierText;
    public TextMeshProUGUI player2TankText;
    public TextMeshProUGUI player2SuperTroopText;

    // UI Texts for troop stats (per player)
    [Header("Player 1 Troop Stats UI")]
    public TextMeshProUGUI player1SoldierStatsText;
    public TextMeshProUGUI player1TankStatsText;
    public TextMeshProUGUI player1SuperTroopStatsText;

    [Header("Player 2 Troop Stats UI")]
    public TextMeshProUGUI player2SoldierStatsText;
    public TextMeshProUGUI player2TankStatsText;
    public TextMeshProUGUI player2SuperTroopStatsText;

    private void Update()
    {
        UpdateCombatScores();
    }

    /// <summary>
    /// Update combat score display for both players.
    /// </summary>
    public void UpdateCombatScores()
    {
        if (player1CombatScoreText != null && player1Money != null)
            player1CombatScoreText.text = $"Combat Score: {player1Money.GetTotalCombatScore()}";
        
        if (player2CombatScoreText != null && player2Money != null)
            player2CombatScoreText.text = $"Combat Score: {player2Money.GetTotalCombatScore()}";

        int minimumScore = GetMinimumCombatScore();
        
        if (player1MinimumCombatScoreText != null || player2MinimumCombatScoreText != null)
        {
            if (player1MinimumCombatScoreText != null)
                player1MinimumCombatScoreText.text = $"Min Combat Score: {minimumScore}";
            if (player2MinimumCombatScoreText != null)
                player2MinimumCombatScoreText.text = $"Min Combat Score: {minimumScore}";
        }
        
        // Update MSC bonus prediction (only when using reward flow, not damage)
        if (IsUsingMSCRewardMode())
        {
            GameConfig gameConfig = GetGameConfig();
            if (player1MSCBonusText != null && player1Money != null)
            {
                int predictedBonus1 = gameConfig.CalculateBonusGoldFromScore(minimumScore, player1Money.GetTotalCombatScore());
                player1MSCBonusText.text = $"MSC Bonus: {predictedBonus1}g";
            }
            if (player2MSCBonusText != null && player2Money != null)
            {
                int predictedBonus2 = gameConfig.CalculateBonusGoldFromScore(minimumScore, player2Money.GetTotalCombatScore());
                player2MSCBonusText.text = $"MSC Bonus: {predictedBonus2}g";
            }
        }
        else
        {
            // Hide or clear bonus text when not using reward flow
            if (player1MSCBonusText != null)
                player1MSCBonusText.text = "";
            if (player2MSCBonusText != null)
                player2MSCBonusText.text = "";
        }
        
        // Update health displays
        if (player1HealthText != null && player1Money != null)
            player1HealthText.text = $"Health: {player1Money.GetHealth()}";
        
        if (player2HealthText != null && player2Money != null)
            player2HealthText.text = $"Health: {player2Money.GetHealth()}";
    }

    /// <summary>
    /// Shows detailed combat information for both players.
    /// </summary>
    public void ShowCombatInfo()
    {
        // Player 1 unit counts
        if (player1SoldierText != null)
            player1SoldierText.text = $"Soldier: {player1Money.soldierCount}";
        if (player1TankText != null)
            player1TankText.text = $"Tank: {player1Money.tankCount}";
        if (player1SuperTroopText != null)
            player1SuperTroopText.text = $"Super Troop: {player1Money.superTroopCount}";

        // Player 2 unit counts
        if (player2SoldierText != null)
            player2SoldierText.text = $"Soldier: {player2Money.soldierCount}";
        if (player2TankText != null)
            player2TankText.text = $"Tank: {player2Money.tankCount}";
        if (player2SuperTroopText != null)
            player2SuperTroopText.text = $"Super Troop: {player2Money.superTroopCount}";

        // Player 1 unit stats
        if (player1Upgrades != null)
        {
            var soldierStats = player1Upgrades.GetSoldierStats();
            var tankStats = player1Upgrades.GetTankStats();
            var superTroopStats = player1Upgrades.GetSuperTroopStats();
            
            if (player1SoldierStatsText != null)
                player1SoldierStatsText.text = $"Health: {soldierStats.health} | Damage: {soldierStats.damage}";
            if (player1TankStatsText != null)
                player1TankStatsText.text = $"Health: {tankStats.health} | Damage: {tankStats.damage}";
            if (player1SuperTroopStatsText != null)
                player1SuperTroopStatsText.text = $"Health: {superTroopStats.health} | Damage: {superTroopStats.damage}";
        }

        // Player 2 unit stats
        if (player2Upgrades != null)
        {
            var soldierStats = player2Upgrades.GetSoldierStats();
            var tankStats = player2Upgrades.GetTankStats();
            var superTroopStats = player2Upgrades.GetSuperTroopStats();
            
            if (player2SoldierStatsText != null)
                player2SoldierStatsText.text = $"Health: {soldierStats.health} | Damage: {soldierStats.damage}";
            if (player2TankStatsText != null)
                player2TankStatsText.text = $"Health: {tankStats.health} | Damage: {tankStats.damage}";
            if (player2SuperTroopStatsText != null)
                player2SuperTroopStatsText.text = $"Health: {superTroopStats.health} | Damage: {superTroopStats.damage}";
        }
        
        // Update combat scores
        UpdateCombatScores();
    }

    private int GetMinimumCombatScore()
    {
        if (turnSystem != null)
            return turnSystem.GetCurrentMinimumCombatScore();

        GameConfig config = GetGameConfig();
        return config != null ? config.minimumCombatScoreBase : 0;
    }

    private GameConfig GetGameConfig()
    {
        if (player1Money != null && player1Money.gameConfig != null)
            return player1Money.gameConfig;
        if (player2Money != null && player2Money.gameConfig != null)
            return player2Money.gameConfig;
        return null;
    }

    /// <summary>
    /// Check if the game is using MSC reward mode (not damage mode).
    /// </summary>
    private bool IsUsingMSCRewardMode()
    {
        GameConfig config = GetGameConfig();
        return config != null && config.useMinimumCombatScore && !config.useMinimumCombatScoreAsDamage;
    }
}
