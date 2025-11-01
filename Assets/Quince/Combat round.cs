using UnityEngine;
using TMPro;

public class Combatround : MonoBehaviour
{
    public PlayerMoney player1Money;
    public PlayerMoney player2Money;
    public Upgrades player1Upgrades;
    public Upgrades player2Upgrades;

    // UI Texts for player 1
    public TextMeshProUGUI player1Troop1Text;
    public TextMeshProUGUI player1Troop2Text;
    public TextMeshProUGUI player1Troop3Text;
    public TextMeshProUGUI player1Troop4Text;

    // UI Texts for player 2
    public TextMeshProUGUI player2Troop1Text;
    public TextMeshProUGUI player2Troop2Text;
    public TextMeshProUGUI player2Troop3Text;
    public TextMeshProUGUI player2Troop4Text;

    // UI Texts for troop stats (per player)
    public TextMeshProUGUI player1Troop1StatsText;
    public TextMeshProUGUI player1Troop2StatsText;
    public TextMeshProUGUI player1Troop3StatsText;
    public TextMeshProUGUI player1Troop4StatsText;

    public TextMeshProUGUI player2Troop1StatsText;
    public TextMeshProUGUI player2Troop2StatsText;
    public TextMeshProUGUI player2Troop3StatsText;
    public TextMeshProUGUI player2Troop4StatsText;

    public void ShowCombatInfo()
    {
        // Player 1 troop counts
        player1Troop1Text.text = $"Troop 1: {player1Money.troop1Count}";
        player1Troop2Text.text = $"Troop 2: {player1Money.troop2Count}";
        player1Troop3Text.text = $"Troop 3: {player1Money.troop3Count}";
        player1Troop4Text.text = $"Troop 4: {player1Money.troop4Count}";

        // Player 2 troop counts
        player2Troop1Text.text = $"Troop 1: {player2Money.troop1Count}";
        player2Troop2Text.text = $"Troop 2: {player2Money.troop2Count}";
        player2Troop3Text.text = $"Troop 3: {player2Money.troop3Count}";
        player2Troop4Text.text = $"Troop 4: {player2Money.troop4Count}";

        // Player 1 troop stats
        player1Troop1StatsText.text = $"Health: {player1Upgrades.troop1Health} | Damage: {player1Upgrades.troop1Damage} | Range: {player1Upgrades.troop1Range}";
        player1Troop2StatsText.text = $"Health: {player1Upgrades.troop2Health} | Damage: {player1Upgrades.troop2Damage} | Range: {player1Upgrades.troop2Range}";
        player1Troop3StatsText.text = $"Health: {player1Upgrades.troop3Health} | Damage: {player1Upgrades.troop3Damage} | Range: {player1Upgrades.troop3Range}";
        player1Troop4StatsText.text = $"Health: {player1Upgrades.troop4Health} | Damage: {player1Upgrades.troop4Damage} | Range: {player1Upgrades.troop4Range}";

        // Player 2 troop stats
        player2Troop1StatsText.text = $"Health: {player2Upgrades.troop1Health} | Damage: {player2Upgrades.troop1Damage} | Range: {player2Upgrades.troop1Range}";
        player2Troop2StatsText.text = $"Health: {player2Upgrades.troop2Health} | Damage: {player2Upgrades.troop2Damage} | Range: {player2Upgrades.troop2Range}";
        player2Troop3StatsText.text = $"Health: {player2Upgrades.troop3Health} | Damage: {player2Upgrades.troop3Damage} | Range: {player2Upgrades.troop3Range}";
        player2Troop4StatsText.text = $"Health: {player2Upgrades.troop4Health} | Damage: {player2Upgrades.troop4Damage} | Range: {player2Upgrades.troop4Range}";
    }
}
