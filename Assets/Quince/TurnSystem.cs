using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class TurnSystem : MonoBehaviour
{
    [Header("Configuration")]
    public GameConfig gameConfig;
    
    [Header("UI References")]
    public GameObject player1Button;
    public GameObject player2Button;
    public GameObject endRoundButton;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI gameOverText;
    
    [Header("Player References")]
    public PlayerMoney playerMoney1;
    public PlayerMoney playerMoney2;
    
    [Header("Events")]
    public UnityEvent onRoundEnd;
    public UnityEvent onGameEnd;

    private int currentRound = 1;
    private int totalRounds;
    private bool gameEnded = false;
    
    private enum Turn { Player1, Player2 }
    private Turn currentTurn;

    void Start()
    {
        totalRounds = gameConfig != null ? gameConfig.totalRounds : 10;
        currentTurn = Turn.Player1;
        UpdateRoundUI();
        UpdateTurnUI();
        
        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Called when Player 1 ends their turn.
    /// </summary>
    public void OnPlayer1Button()
    {
        if (gameEnded) return;
        currentTurn = Turn.Player2;
        UpdateTurnUI();
    }

    /// <summary>
    /// Called when Player 2 ends their turn, ending the round.
    /// </summary>
    public void OnPlayer2Button()
    {
        if (gameEnded) return;
        EndRound();
    }

    /// <summary>
    /// Alternative button to end the round (can be used by either player).
    /// </summary>
    public void OnEndRoundButton()
    {
        if (gameEnded) return;
        EndRound();
    }

    private void EndRound()
    {
        // Collect market income for both players
        playerMoney1.CollectMarketIncome();
        playerMoney2.CollectMarketIncome();
        
        // Fire round end event
        onRoundEnd?.Invoke();
        
        // Check if this was the final round
        if (currentRound >= totalRounds)
        {
            EndGame();
            return;
        }
        
        // Advance to next round
        currentRound++;
        currentTurn = Turn.Player1;
        UpdateRoundUI();
        UpdateTurnUI();
    }

    private void EndGame()
    {
        gameEnded = true;
        
        // Calculate final combat scores
        int player1Score = playerMoney1.GetTotalCombatScore();
        int player2Score = playerMoney2.GetTotalCombatScore();
        
        // Determine winner
        string winnerText;
        if (player1Score > player2Score)
        {
            winnerText = $"Player 1 Wins!\nScore: {player1Score} vs {player2Score}";
        }
        else if (player2Score > player1Score)
        {
            winnerText = $"Player 2 Wins!\nScore: {player2Score} vs {player1Score}";
        }
        else
        {
            winnerText = $"It's a Tie!\nBoth players scored: {player1Score}";
        }
        
        // Display game over
        if (gameOverText != null)
        {
            gameOverText.text = winnerText;
            gameOverText.gameObject.SetActive(true);
        }
        
        // Hide turn buttons
        if (player1Button != null) player1Button.SetActive(false);
        if (player2Button != null) player2Button.SetActive(false);
        if (endRoundButton != null) endRoundButton.SetActive(false);
        
        // Fire game end event
        onGameEnd?.Invoke();
        
        Debug.Log($"Game Over! {winnerText}");
    }

    private void UpdateRoundUI()
    {
        if (roundText != null)
            roundText.text = $"Round {currentRound} / {totalRounds}";
    }

    private void UpdateTurnUI()
    {
        if (player1Button != null)
            player1Button.SetActive(currentTurn == Turn.Player1);
        if (player2Button != null)
            player2Button.SetActive(currentTurn == Turn.Player2);
        if (endRoundButton != null)
            endRoundButton.SetActive(false); // Can enable if you want a shared end round button
    }

    /// <summary>
    /// Get the current round number.
    /// </summary>
    public int GetCurrentRound() => currentRound;
    
    /// <summary>
    /// Get total number of rounds.
    /// </summary>
    public int GetTotalRounds() => totalRounds;
    
    /// <summary>
    /// Check if the game has ended.
    /// </summary>
    public bool IsGameEnded() => gameEnded;
}
