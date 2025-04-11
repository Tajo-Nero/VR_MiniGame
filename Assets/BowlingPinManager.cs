using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPinManager : MonoBehaviour
{
    public event Action<int> OnPinsUpdated; // ������ �� ������ ����� �� ����� �̺�Ʈ

    private int fallenPinCount = 0; // ������ �� ����
    private HashSet<Transform> fallenPins = new HashSet<Transform>(); // ������ �� ������
    private List<Vector3> initialPositions = new List<Vector3>(); // �� �ʱ� ��ġ
    private List<Quaternion> initialRotations = new List<Quaternion>(); // �� �ʱ� ȸ����

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

                Debug.Log($"������ �� ���� ������Ʈ: {fallenPinCount}");

                // �̺�Ʈ ȣ���Ͽ� ���� �Ŵ����� ����� �� ������ �˸�
                OnPinsUpdated?.Invoke(fallenPinCount);
                StartCoroutine(DeactivatePinAfterDelay(pin.gameObject));
            }
        }
    }

    private bool IsPinFallen(Transform pin)
    {
        float angle = Vector3.Angle(pin.up, Vector3.up);

        return angle > 30f; // 30�� �̻� �������� �� ������ ������ �Ǵ�
    }

    private IEnumerator DeactivatePinAfterDelay(GameObject pin)
    {
        yield return new WaitForSeconds(2f);
        pin.SetActive(false);
        Debug.Log($"{pin.name} ��Ȱ��ȭ��!");
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

        // ������ �� �ʱ�ȭ �� �̺�Ʈ ȣ��
        fallenPinCount = 0;
        fallenPins.Clear();
        OnPinsUpdated?.Invoke(fallenPinCount);

        Debug.Log("�� �ʱ�ȭ �Ϸ�!");
    }
}