using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public GameObject player1Button;
    public GameObject player2Button;
    public GameObject combatButton;
    public TextMeshProUGUI roundText;

    private int round = 1;
    public PlayerMoney playerMoney;
    private enum Turn { Player2, Combat, Player1 }
    private Turn currentTurn;

    void Start()
    {
        roundText.text = $"round {round} ";
        currentTurn = Turn.Player1;
        UpdateUI();

    }

    public void OnPlayer2Button()
    {
        currentTurn = Turn.Combat;
        UpdateUI();
    }

    public void OnCombatButton()
    {
        round++;
        roundText.text = $"round {round} ";
        currentTurn = Turn.Player1;
        UpdateUI();
        playerMoney.CollectMarketIncome();
    }

    public void OnPlayer1Button()
    {
        currentTurn = Turn.Player2;
        UpdateUI();
    }

    private void UpdateUI()
    {
        player1Button.gameObject.SetActive(currentTurn == Turn.Player1);
        player2Button.gameObject.SetActive(currentTurn == Turn.Player2);
        combatButton.gameObject.SetActive(currentTurn == Turn.Combat);


    }
}
