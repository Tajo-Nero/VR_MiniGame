using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPinManager : MonoBehaviour
{
    private int fallenPinCount = 0; // 쓰러진 핀 개수
    private HashSet<Transform> fallenPins = new HashSet<Transform>(); // 쓰러진 핀 추적용
    private List<Vector3> initialPositions = new List<Vector3>(); // 핀 초기 위치
    private List<Quaternion> initialRotations = new List<Quaternion>(); // 핀 초기 회전값

    public int FallenPinCount
    {
        get { return fallenPinCount; } // 쓰러진 핀 개수 반환
    }

    private void Start()
    {
        // 모든 핀의 초기 위치와 회전을 저장
        foreach (Transform pin in transform)
        {
            initialPositions.Add(pin.position);
            initialRotations.Add(pin.rotation);
        }
    }

    private void Update()
    {
        foreach (Transform pin in transform)
        {
            if (!fallenPins.Contains(pin) && IsPinFallen(pin))
            {
                fallenPins.Add(pin); // 새로운 쓰러진 핀 기록
                fallenPinCount++;
                StartCoroutine(DeactivatePinAfterDelay(pin.gameObject)); // 5초 후 비활성화
                Debug.Log($"{pin.name} 쓰러짐 감지! 현재 쓰러진 핀 개수: {fallenPinCount}");
            }
        }
    }

    private bool IsPinFallen(Transform pin)
    {
        // 핀이 Z축 기준으로 기울어진 각도를 판단
        return pin.rotation.eulerAngles.z > 45f;
    }

    private IEnumerator DeactivatePinAfterDelay(GameObject pin)
    {
        yield return new WaitForSeconds(5f); // 5초 대기
        pin.SetActive(false); // 핀 비활성화
        Debug.Log($"{pin.name} 비활성화됨!");
    }

    public void ResetPins()
    {
        // 핀 위치와 회전을 초기 상태로 복원하고 다시 활성화
        int index = 0;
        foreach (Transform pin in transform)
        {
            pin.position = initialPositions[index];
            pin.rotation = initialRotations[index];
            pin.gameObject.SetActive(true); // 비활성화된 핀을 다시 활성화
            index++;
        }

        // 상태 초기화
        fallenPinCount = 0;
        fallenPins.Clear();
        Debug.Log("핀 초기화 완료!");
    }
}