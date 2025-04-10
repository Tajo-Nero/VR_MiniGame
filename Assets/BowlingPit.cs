using System.Collections;
using UnityEngine;

public class BowlingPit : MonoBehaviour
{
    public Transform targetPosition; // �̵��� ��ǥ ��ġ

    void OnTriggerEnter(Collider other)
    {
        // �±װ� "BowlingBall"�� ������Ʈ�� �浹���� ��
        if (other.CompareTag("BowlingBall"))
        {
            StartCoroutine(WaitAndMove(other.gameObject)); // �ڷ�ƾ ����
        }
    }

    // 3�� ��� �� ��ǥ ��ġ�� �̵���Ű�� �ڷ�ƾ
    IEnumerator WaitAndMove(GameObject bowlingBall)
    {
        Debug.Log("���� �� �浹 ����! 3�� ��� ��...");
        yield return new WaitForSeconds(3f); // 3�� ���

        if (targetPosition != null)
        {
            bowlingBall.transform.position = targetPosition.position; // ��ġ �̵�
            Debug.Log("���� ���� ��ǥ ��ġ�� �̵��߽��ϴ�!");
        }        
    }
}