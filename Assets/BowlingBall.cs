using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowlingBall : MonoBehaviour
{
    private Rigidbody ballRigidbody; // ���� ���� Rigidbody
    private XRGrabInteractable grabInteractable; // XR Grab Interactable
    private BowlingGameManager gameManager; // GameManager ����

    public float rollSpeed = 5f; // �� �������� �ӵ�
    public float rollSpin = 10f; // �� ȸ�� �ӵ�

    private void Awake()
    {
        // Rigidbody�� XR Grab Interactable ������Ʈ ��������
        ballRigidbody = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // GameManager ���� ��������
        gameManager = FindObjectOfType<BowlingGameManager>();

        // �ֽ� API ���: selectExited �̺�Ʈ ����
        grabInteractable.selectExited.AddListener(OnBallReleased);
    }

    private void OnBallReleased(SelectExitEventArgs args)
    {       
        // ���� ������ �� �ӵ��� ȸ�� ����
        Vector3 forwardDirection = transform.forward; // ���� ����
        ballRigidbody.velocity = forwardDirection * rollSpeed; // �������� �ӵ� ����
        ballRigidbody.angularVelocity = new Vector3(0, rollSpin, 0); // ȸ�� ����

        // BowlingGameManager�� �� ���� Ƚ�� ������Ʈ ��û
        if (gameManager != null)
        {
            gameManager.RollBall(); // BowlingGameManager�� RollBall ȣ��
        }
        
    }
}