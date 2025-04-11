using TMPro;
using UnityEngine;


public class BowlingGameUIManager : MonoBehaviour
{
    public TextMeshProUGUI roundText;    // 라운드 표시
    public TextMeshProUGUI turnText;     // 턴 표시
    public TextMeshProUGUI scoreText;    // 점수 표시
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