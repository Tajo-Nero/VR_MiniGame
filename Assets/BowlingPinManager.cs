using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPinManager : MonoBehaviour
{
    private int fallenPinCount = 0; // ������ �� ����
    private HashSet<Transform> fallenPins = new HashSet<Transform>(); // ������ �� ������
    private List<Vector3> initialPositions = new List<Vector3>(); // �� �ʱ� ��ġ
    private List<Quaternion> initialRotations = new List<Quaternion>(); // �� �ʱ� ȸ����

    public int FallenPinCount
    {
        get { return fallenPinCount; } // ������ �� ���� ��ȯ
    }

    private void Start()
    {
        // ��� ���� �ʱ� ��ġ�� ȸ���� ����
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
                fallenPins.Add(pin); // ���ο� ������ �� ���
                fallenPinCount++;
                StartCoroutine(DeactivatePinAfterDelay(pin.gameObject)); // 5�� �� ��Ȱ��ȭ
                Debug.Log($"{pin.name} ������ ����! ���� ������ �� ����: {fallenPinCount}");
            }
        }
    }

    private bool IsPinFallen(Transform pin)
    {
        // ���� Z�� �������� ������ ������ �Ǵ�
        return pin.rotation.eulerAngles.z > 45f;
    }

    private IEnumerator DeactivatePinAfterDelay(GameObject pin)
    {
        yield return new WaitForSeconds(5f); // 5�� ���
        pin.SetActive(false); // �� ��Ȱ��ȭ
        Debug.Log($"{pin.name} ��Ȱ��ȭ��!");
    }

    public void ResetPins()
    {
        // �� ��ġ�� ȸ���� �ʱ� ���·� �����ϰ� �ٽ� Ȱ��ȭ
        int index = 0;
        foreach (Transform pin in transform)
        {
            pin.position = initialPositions[index];
            pin.rotation = initialRotations[index];
            pin.gameObject.SetActive(true); // ��Ȱ��ȭ�� ���� �ٽ� Ȱ��ȭ
            index++;
        }

        // ���� �ʱ�ȭ
        fallenPinCount = 0;
        fallenPins.Clear();
        Debug.Log("�� �ʱ�ȭ �Ϸ�!");
    }
}