using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowlingBall : MonoBehaviour
{
    private Rigidbody ballRigidbody; // 볼링 공의 Rigidbody
    private XRGrabInteractable grabInteractable; // XR Grab Interactable
    private BowlingGameManager gameManager; // GameManager 참조

    public float rollSpeed = 5f; // 공 굴러가는 속도
    public float rollSpin = 10f; // 공 회전 속도

    private void Awake()
    {
        // Rigidbody와 XR Grab Interactable 컴포넌트 가져오기
        ballRigidbody = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // GameManager 참조 가져오기
        gameManager = FindObjectOfType<BowlingGameManager>();

        // 최신 API 사용: selectExited 이벤트 연결
        grabInteractable.selectExited.AddListener(OnBallReleased);
    }

    private void OnBallReleased(SelectExitEventArgs args)
    {       
        // 공이 놓였을 때 속도와 회전 설정
        Vector3 forwardDirection = transform.forward; // 공의 방향
        ballRigidbody.velocity = forwardDirection * rollSpeed; // 굴러가는 속도 설정
        ballRigidbody.angularVelocity = new Vector3(0, rollSpin, 0); // 회전 설정

        // BowlingGameManager에 공 굴린 횟수 업데이트 요청
        if (gameManager != null)
        {
            gameManager.RollBall(); // BowlingGameManager의 RollBall 호출
        }
        
    }
}