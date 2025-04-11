using TMPro;
using UnityEngine;


public class BowlingGameUIManager : MonoBehaviour
{
    public TextMeshProUGUI roundText;    // ���� ǥ��
    public TextMeshProUGUI turnText;     // �� ǥ��
    public TextMeshProUGUI scoreText;    // ���� ǥ��
    public BowlingGameManager gameManager;

    
    private void UpdateUI()
    {
        roundText.text = $"Round: {gameManager.currentRound}";
        turnText.text = $"Trun: {gameManager.currentTurn}";
        scoreText.text = $"Score: {gameManager.GetTotalScore()}";
    }

    private void Update()
    {
        UpdateUI();
    }
}