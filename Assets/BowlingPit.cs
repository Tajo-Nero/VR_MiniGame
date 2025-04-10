using System.Collections;
using UnityEngine;

public class BowlingPit : MonoBehaviour
{
    public Transform targetPosition; // 이동할 목표 위치

    void OnTriggerEnter(Collider other)
    {
        // 태그가 "BowlingBall"인 오브젝트와 충돌했을 때
        if (other.CompareTag("BowlingBall"))
        {
            StartCoroutine(WaitAndMove(other.gameObject)); // 코루틴 시작
        }
    }

    // 3초 대기 후 목표 위치로 이동시키는 코루틴
    IEnumerator WaitAndMove(GameObject bowlingBall)
    {
        Debug.Log("볼링 공 충돌 감지! 3초 대기 중...");
        yield return new WaitForSeconds(3f); // 3초 대기

        if (targetPosition != null)
        {
            bowlingBall.transform.position = targetPosition.position; // 위치 이동
            Debug.Log("볼링 공이 목표 위치로 이동했습니다!");
        }        
    }
}