using System.Collections;
using UnityEngine;

public class BowlingPit : MonoBehaviour
{
    public Transform targetPosition; // �̵��� ��ǥ ��ġ
    public BowlingGameManager gameManager; // ���� �Ŵ��� ����

    void OnTriggerEnter(Collider other)
    {
        // �±װ� "BowlingBall"�� ������Ʈ�� �浹���� ��
        if (other.CompareTag("BowlingBall"))
        {
            StartCoroutine(WaitAndMove(other.gameObject)); // �ڷ�ƾ ����
        }
    }

    // 3�� ��� �� ��ǥ ��ġ�� �̵���Ű�� �� ����
    IEnumerator WaitAndMove(GameObject bowlingBall)
    {       
        yield return new WaitForSeconds(3f); // 3�� ���

        if (targetPosition != null)
        {
            bowlingBall.transform.position = targetPosition.position; // ��ġ �̵�
            
        }

        // �� ���� �� �� �ʱ�ȭ
        if (gameManager != null)
        {
            gameManager.EndTurn();
        }        
    }
}