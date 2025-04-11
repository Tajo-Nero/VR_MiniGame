using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPinManager : MonoBehaviour
{
    public event Action<int> OnPinsUpdated; // 쓰러진 핀 개수가 변경될 때 실행될 이벤트

    private int fallenPinCount = 0; // 쓰러진 핀 개수
    private HashSet<Transform> fallenPins = new HashSet<Transform>(); // 쓰러진 핀 추적용
    private List<Vector3> initialPositions = new List<Vector3>(); // 핀 초기 위치
    private List<Quaternion> initialRotations = new List<Quaternion>(); // 핀 초기 회전값

    public int FallenPinCount => fallenPinCount;

    private void Start()
    {
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
                fallenPins.Add(pin);
                fallenPinCount++;

                Debug.Log($"쓰러진 핀 개수 업데이트: {fallenPinCount}");

                // 이벤트 호출하여 게임 매니저에 변경된 핀 개수를 알림
                OnPinsUpdated?.Invoke(fallenPinCount);
                StartCoroutine(DeactivatePinAfterDelay(pin.gameObject));
            }
        }
    }

    private bool IsPinFallen(Transform pin)
    {
        float angle = Vector3.Angle(pin.up, Vector3.up);

        return angle > 30f; // 30도 이상 기울어졌을 때 쓰러진 것으로 판단
    }

    private IEnumerator DeactivatePinAfterDelay(GameObject pin)
    {
        yield return new WaitForSeconds(2f);
        pin.SetActive(false);
        Debug.Log($"{pin.name} 비활성화됨!");
    }

    public void ResetPins()
    {
        int index = 0;
        foreach (Transform pin in transform)
        {
            pin.position = initialPositions[index];
            pin.rotation = initialRotations[index];
            pin.gameObject.SetActive(true);
            index++;
        }

        // 쓰러진 핀 초기화 및 이벤트 호출
        fallenPinCount = 0;
        fallenPins.Clear();
        OnPinsUpdated?.Invoke(fallenPinCount);

        Debug.Log("핀 초기화 완료!");
    }
}