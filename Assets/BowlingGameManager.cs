using System.Collections.Generic;
using UnityEngine;

public class BowlingGameManager : MonoBehaviour
{
    public BowlingPinManager pinManager;
    public GameObject bowlingBall;
    public Transform ballStartPosition;

    public int currentTurn = 1;
    public int currentRound = 1;
    private int rollCount = 0;
    private int totalScore = 0;
    private List<int> turnScores = new List<int>();

    private void Awake()
    {
        pinManager.OnPinsUpdated += UpdatePinCount;
    }

    private void StartTurn()
    {
        rollCount = 0;
        Debug.Log($"라운드 {currentRound}, 턴 {currentTurn} 시작!");
    }

    public void RollBall()
    {
        rollCount++;
        int fallenPins = pinManager.FallenPinCount;

        Debug.Log($"턴 {currentTurn}, 공 {rollCount}: {fallenPins} 핀 쓰러짐!");

        // 첫 번째 굴림에서도 점수 기록하도록 수정
        RecordScore(fallenPins);

        if (rollCount == 1 && fallenPins == 10)
        {
            Debug.Log("스트라이크!");
            RecordScore(GetBonus(2));
            EndTurn();
        }
        else if (rollCount == 2)
        {
            int previousScore = (currentTurn - 1 < turnScores.Count) ? turnScores[currentTurn - 1] : 0;

            if (fallenPins + previousScore == 10)
            {
                Debug.Log("스페어!");
                RecordScore(GetBonus(1));
            }

            EndTurn();
        }
    }

    private void RecordScore(int score)
    {
        totalScore += score;
        turnScores.Add(score);
        Debug.Log($"턴 {currentTurn} 점수 기록: {score}, 총 점수: {totalScore}");
    }

    public void EndTurn()
    {
        Debug.Log($"턴 {currentTurn} 종료!");
        pinManager.ResetPins();

        currentTurn++;

        if (currentTurn > 2)
        {
            currentRound++;
            currentTurn = 1;
            ResetBallPosition();
            Debug.Log($"새로운 라운드 시작! 현재 라운드: {currentRound}");
        }

        if (currentRound <= 5)
        {
            StartTurn();
        }
        else
        {
            Debug.Log($"게임 종료! 최종 점수: {totalScore}");
        }
    }

    private void ResetBallPosition()
    {
        if (bowlingBall != null && ballStartPosition != null)
        {
            bowlingBall.transform.position = ballStartPosition.position;
            bowlingBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bowlingBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Debug.Log("볼링 공 위치 초기화 완료!");
        }
    }

    private int GetBonus(int rollsToConsider)
    {
        int bonusScore = 0;
        int currentIndex = turnScores.Count - rollsToConsider;

        for (int i = 0; i < rollsToConsider; i++)
        {
            if (currentIndex >= 0 && currentIndex < turnScores.Count)
            {
                bonusScore += turnScores[currentIndex];
            }
            currentIndex++;
        }

        return bonusScore;
    }

    private void UpdatePinCount(int newPinCount)
    {
        totalScore += newPinCount;
        Debug.Log($"총 점수 업데이트됨: {totalScore}");
    }

    public int GetTotalScore()
    {
        return totalScore;
    }
}