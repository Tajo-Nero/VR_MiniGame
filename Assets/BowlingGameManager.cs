using UnityEngine;

public class BowlingGameManager : MonoBehaviour
{
    public BowlingPinManager pinManager; // �� �Ŵ��� ����
    private int currentTurn = 1;         // ���� ��
    private int rollCount = 0;           // ���� �� ���� Ƚ�� (1�ϴ� �ִ� 2��)
    private int totalScore = 0;          // ���� �� ����

    private void Start()
    {
        StartTurn(); // ù �� ����
    }

    private void StartTurn()
    {
        rollCount = 0; // ���ο� �� ���� �� ���� Ƚ�� �ʱ�ȭ
        Debug.Log($"�� {currentTurn} ����!");
    }

    public void RollBall()
    {
        rollCount++;

        int pinsKnockedDown = pinManager.FallenPinCount; // ������ �� ���� ��������
        Debug.Log($"�� {currentTurn}, �� {rollCount}: {pinsKnockedDown} �� ������!");

        if (rollCount == 1 && pinsKnockedDown == 10) // ��Ʈ����ũ
        {
            Debug.Log("��Ʈ����ũ!");
            RecordScore(10);
            EndTurn();
        }
        else if (rollCount == 2)
        {
            if (pinManager.FallenPinCount == 10) // �����
            {
                Debug.Log("�����!");
                RecordScore(10);
            }
            else
            {
                RecordScore(pinManager.FallenPinCount); // �ܼ� ���� ���
            }

            EndTurn();
        }
    }

    private void RecordScore(int score)
    {
        totalScore += score;
        Debug.Log($"�� {currentTurn} ���� ���: {score}. �� ����: {totalScore}");
    }

    private void EndTurn()
    {
        // �� ���� �� �� �ʱ�ȭ
        pinManager.ResetPins();

        currentTurn++; // ���� ������ �̵�
        if (currentTurn <= 10) // �ִ� 10��
        {
            StartTurn(); // ���ο� �� ����
        }
        else
        {
            Debug.Log($"���� ����! ���� ����: {totalScore}");
        }
    }

    public int GetTotalScore()
    {
        return totalScore; // ���� �� ���� ��ȯ
    }
}