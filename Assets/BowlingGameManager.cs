using UnityEngine;

public class BowlingGameManager : MonoBehaviour
{
    public BowlingPinManager pinManager; // 핀 매니저 참조
    private int currentTurn = 1;         // 현재 턴
    private int rollCount = 0;           // 현재 공 굴린 횟수 (1턴당 최대 2번)
    private int totalScore = 0;          // 게임 총 점수

    private void Start()
    {
        StartTurn(); // 첫 턴 시작
    }

    private void StartTurn()
    {
        rollCount = 0; // 새로운 턴 시작 시 굴린 횟수 초기화
        Debug.Log($"턴 {currentTurn} 시작!");
    }

    public void RollBall()
    {
        rollCount++;

        int pinsKnockedDown = pinManager.FallenPinCount; // 쓰러진 핀 개수 가져오기
        Debug.Log($"턴 {currentTurn}, 공 {rollCount}: {pinsKnockedDown} 핀 쓰러짐!");

        if (rollCount == 1 && pinsKnockedDown == 10) // 스트라이크
        {
            Debug.Log("스트라이크!");
            RecordScore(10);
            EndTurn();
        }
        else if (rollCount == 2)
        {
            if (pinManager.FallenPinCount == 10) // 스페어
            {
                Debug.Log("스페어!");
                RecordScore(10);
            }
            else
            {
                RecordScore(pinManager.FallenPinCount); // 단순 점수 기록
            }

            EndTurn();
        }
    }

    private void RecordScore(int score)
    {
        totalScore += score;
        Debug.Log($"턴 {currentTurn} 점수 기록: {score}. 총 점수: {totalScore}");
    }

    private void EndTurn()
    {
        // 턴 종료 후 핀 초기화
        pinManager.ResetPins();

        currentTurn++; // 다음 턴으로 이동
        if (currentTurn <= 10) // 최대 10턴
        {
            StartTurn(); // 새로운 턴 시작
        }
        else
        {
            Debug.Log($"게임 종료! 최종 점수: {totalScore}");
        }
    }

    public int GetTotalScore()
    {
        return totalScore; // 게임 총 점수 반환
    }
}