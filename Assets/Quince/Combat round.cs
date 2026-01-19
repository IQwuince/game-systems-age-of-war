using UnityEngine;
using TMPro;

public class Combatround : MonoBehaviour
{
    public PlayerMoney player1Money;
    public PlayerMoney player2Money;
    public Upgrades player1Upgrades;
    public Upgrades player2Upgrades;
    public TextMeshProUGUI player1BaseHealthText;
    public TextMeshProUGUI player2BaseHealthText;


    // UI Texts for player 1
    public TextMeshProUGUI player1TroopText;
    public TextMeshProUGUI player1TankText;
    public TextMeshProUGUI player1RangeText;
    public TextMeshProUGUI player1Troop4Text;

    // UI Texts for player 2
    public TextMeshProUGUI player2TroopText;
    public TextMeshProUGUI player2TankText;
    public TextMeshProUGUI player2RangeText;
    public TextMeshProUGUI player2Troop4Text;

    // UI Texts for troop stats (per player)
    public TextMeshProUGUI player1TroopStatsText;
    public TextMeshProUGUI player1TankStatsText;
    public TextMeshProUGUI player1RangeStatsText;
    public TextMeshProUGUI player1Troop4StatsText;

    public TextMeshProUGUI player2TroopStatsText;
    public TextMeshProUGUI player2TankStatsText;
    public TextMeshProUGUI player2RangeStatsText;
    public TextMeshProUGUI player2Troop4StatsText;

    private void Update()
    {
        ShowBaseHealth();
    }
    public void ShowCombatInfo()
    {
        // Player 1 unit counts
        player1TroopText.text = $"Troop: {player1Money.troopCount}";
        player1TankText.text = $"Tank: {player1Money.tankCount}";
        player1RangeText.text = $"Range: {player1Money.rangeCount}";

        // Player 2 unit counts
        player2TroopText.text = $"Troop: {player2Money.troopCount}";
        player2TankText.text = $"Tank: {player2Money.tankCount}";
        player2RangeText.text = $"Range: {player2Money.rangeCount}";

        // Player 1 unit stats
        player1TroopStatsText.text = $"Health: {player1Upgrades.troopHealth} | Damage: {player1Upgrades.troopDamage} | Range: {player1Upgrades.troopRange}";
        player1TankStatsText.text = $"Health: {player1Upgrades.tankHealth} | Damage: {player1Upgrades.tankDamage} | Range: {player1Upgrades.tankRange}";
        player1RangeStatsText.text = $"Health: {player1Upgrades.rangeHealth} | Damage: {player1Upgrades.rangeDamage} | Range: {player1Upgrades.rangeRange}";

        // Player 2 unit stats
        player2TroopStatsText.text = $"Health: {player2Upgrades.troopHealth} | Damage: {player2Upgrades.troopDamage} | Range: {player2Upgrades.troopRange}";
        player2TankStatsText.text = $"Health: {player2Upgrades.tankHealth} | Damage: {player2Upgrades.tankDamage} | Range: {player2Upgrades.tankRange}";
        player2RangeStatsText.text = $"Health: {player2Upgrades.rangeHealth} | Damage: {player2Upgrades.rangeDamage} | Range: {player2Upgrades.rangeRange}";
    }

    public void ShowBaseHealth()
    {
        player1BaseHealthText.text = $"Base Health: {player1Money.baseHealth}";
        player2BaseHealthText.text = $"Base Health: {player2Money.baseHealth}";
    }

}
