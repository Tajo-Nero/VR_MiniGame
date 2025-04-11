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
        Debug.Log($"���� {currentRound}, �� {currentTurn} ����!");
    }

    public void RollBall()
    {
        rollCount++;
        int fallenPins = pinManager.FallenPinCount;

        Debug.Log($"�� {currentTurn}, �� {rollCount}: {fallenPins} �� ������!");

        // ù ��° ���������� ���� ����ϵ��� ����
        RecordScore(fallenPins);

        if (rollCount == 1 && fallenPins == 10)
        {
            Debug.Log("��Ʈ����ũ!");
            RecordScore(GetBonus(2));
            EndTurn();
        }
        else if (rollCount == 2)
        {
            int previousScore = (currentTurn - 1 < turnScores.Count) ? turnScores[currentTurn - 1] : 0;

            if (fallenPins + previousScore == 10)
            {
                Debug.Log("�����!");
                RecordScore(GetBonus(1));
            }

            EndTurn();
        }
    }

    private void RecordScore(int score)
    {
        totalScore += score;
        turnScores.Add(score);
        Debug.Log($"�� {currentTurn} ���� ���: {score}, �� ����: {totalScore}");
    }

    public void EndTurn()
    {
        Debug.Log($"�� {currentTurn} ����!");
        pinManager.ResetPins();

        currentTurn++;

        if (currentTurn > 2)
        {
            currentRound++;
            currentTurn = 1;
            ResetBallPosition();
            Debug.Log($"���ο� ���� ����! ���� ����: {currentRound}");
        }

        if (currentRound <= 5)
        {
            StartTurn();
        }
        else
        {
            Debug.Log($"���� ����! ���� ����: {totalScore}");
        }
    }

    private void ResetBallPosition()
    {
        if (bowlingBall != null && ballStartPosition != null)
        {
            bowlingBall.transform.position = ballStartPosition.position;
            bowlingBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bowlingBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Debug.Log("���� �� ��ġ �ʱ�ȭ �Ϸ�!");
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
        Debug.Log($"�� ���� ������Ʈ��: {totalScore}");
    }

    public int GetTotalScore()
    {
        return totalScore;
    }
}