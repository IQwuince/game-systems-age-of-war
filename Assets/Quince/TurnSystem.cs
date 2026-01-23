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
    public TextMeshProUGUI minimumCombatScoreText;
    public TextMeshProUGUI startingPlayerText;
    
    [Header("Player References")]
    public PlayerMoney playerMoney1;
    public PlayerMoney playerMoney2;
    
    [Header("Events")]
    public UnityEvent onRoundEnd;
    public UnityEvent onGameEnd;

    private int currentRound = 1;
    private int totalRounds;
    private bool gameEnded = false;
    
    /// <summary>
    /// The player who starts the current round (1 or 2).
    /// Randomly chosen at match start, then alternates each round.
    /// </summary>
    public int startingPlayer = 1;
    
    private enum Turn { Player1, Player2 }
    private Turn currentTurn;

    void Start()
    {
        totalRounds = gameConfig != null ? gameConfig.totalRounds : 10;
        
        // Randomly choose starting player at match start (only once)
        startingPlayer = Random.value < 0.5f ? 1 : 2;
        currentTurn = (startingPlayer == 1) ? Turn.Player1 : Turn.Player2;
        
        UpdateRoundUI();
        UpdateTurnUI();
        UpdateMinimumCombatScoreUI();
        UpdateStartingPlayerUI();
        
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
        
        // Combat logic: use new combat round or legacy minimum score system
        if (gameConfig != null && gameConfig.useMinimumCombatScore)
        {
            // Legacy minimum combat score mechanic (disabled by default)
            EvaluateCombatScores();
        }
        else
        {
            // New combat round system
            CombatRound();
        }
        
        // Check if any player has died
        if (CheckForPlayerDeath())
        {
            return;
        }
        
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
        
        // Alternate starting player for next round
        startingPlayer = (startingPlayer == 1) ? 2 : 1;
        currentTurn = (startingPlayer == 1) ? Turn.Player1 : Turn.Player2;
        
        UpdateRoundUI();
        UpdateTurnUI();
        UpdateMinimumCombatScoreUI();
        UpdateStartingPlayerUI();
    }

    /// <summary>
    /// Simultaneous combat round: compare players' combat scores and deal damage to loser.
    /// Winner is the player with higher score; damage is based on score difference.
    /// </summary>
    private void CombatRound()
    {
        if (gameConfig == null) return;
        
        int p1Score = playerMoney1.GetTotalCombatScore();
        int p2Score = playerMoney2.GetTotalCombatScore();
        
        // Tie: no damage
        if (p1Score == p2Score)
        {
            Debug.Log($"Combat round: Tie! Both players have score {p1Score}");
            return;
        }
        
        if (p1Score > p2Score)
        {
            int diff = p1Score - p2Score;
            int damage = gameConfig.CalculateDamageFromCombatDifference(diff);
            playerMoney2.TakeDamage(damage);
            Debug.Log($"Combat round: P1 wins by {diff} -> dealt {damage} damage to P2");
        }
        else
        {
            int diff = p2Score - p1Score;
            int damage = gameConfig.CalculateDamageFromCombatDifference(diff);
            playerMoney1.TakeDamage(damage);
            Debug.Log($"Combat round: P2 wins by {diff} -> dealt {damage} damage to P1");
        }
    }

    /// <summary>
    /// Evaluate both players' combat scores against the minimum requirement.
    /// Apply damage if below the minimum.
    /// </summary>
    private void EvaluateCombatScores()
    {
        if (gameConfig == null) return;
        
        int minimumScore = gameConfig.GetMinimumCombatScoreForRound(currentRound);
        
        // Evaluate Player 1
        int player1Score = playerMoney1.GetTotalCombatScore();
        int player1Damage = gameConfig.CalculateDamageFromLowScore(minimumScore, player1Score);
        if (player1Damage > 0)
        {
            playerMoney1.TakeDamage(player1Damage);
            Debug.Log($"Player 1 took {player1Damage} damage (Score: {player1Score} < Min: {minimumScore})");
        }
        
        // Evaluate Player 2
        int player2Score = playerMoney2.GetTotalCombatScore();
        int player2Damage = gameConfig.CalculateDamageFromLowScore(minimumScore, player2Score);
        if (player2Damage > 0)
        {
            playerMoney2.TakeDamage(player2Damage);
            Debug.Log($"Player 2 took {player2Damage} damage (Score: {player2Score} < Min: {minimumScore})");
        }
    }

    /// <summary>
    /// Check if any player's health has reached 0.
    /// Returns true if the game ends due to player death.
    /// </summary>
    private bool CheckForPlayerDeath()
    {
        bool player1Dead = !playerMoney1.IsAlive();
        bool player2Dead = !playerMoney2.IsAlive();
        
        if (player1Dead || player2Dead)
        {
            gameEnded = true;
            
            string winnerText;
            if (player1Dead && player2Dead)
            {
                winnerText = "Both players eliminated!\nIt's a Draw!";
            }
            else if (player1Dead)
            {
                winnerText = $"Player 1 eliminated!\nPlayer 2 Wins!";
            }
            else
            {
                winnerText = $"Player 2 eliminated!\nPlayer 1 Wins!";
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
            return true;
        }
        
        return false;
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

    private void UpdateMinimumCombatScoreUI()
    {
        if (minimumCombatScoreText != null && gameConfig != null)
        {
            int minimumScore = gameConfig.GetMinimumCombatScoreForRound(currentRound);
            minimumCombatScoreText.text = $"Min Combat Score: {minimumScore}";
        }
    }

    private void UpdateTurnUI()
    {
        if (player1Button != null)
            player1Button.SetActive(currentTurn == Turn.Player1);
        if (player2Button != null)
            player2Button.SetActive(currentTurn == Turn.Player2);
    }

    private void UpdateStartingPlayerUI()
    {
        if (startingPlayerText != null)
            startingPlayerText.text = $"Starting Player: P{startingPlayer}";
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
    
    /// <summary>
    /// Get the minimum combat score requirement for the current round.
    /// </summary>
    public int GetCurrentMinimumCombatScore()
    {
        return gameConfig != null ? gameConfig.GetMinimumCombatScoreForRound(currentRound) : 0;
    }
    
    /// <summary>
    /// Get the player who starts the current round (1 or 2).
    /// </summary>
    public int GetStartingPlayer() => startingPlayer;
}
