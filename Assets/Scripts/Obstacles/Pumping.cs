using UnityEngine;
using System.Collections;

public class Pumping : Opstacle
{
    public float baseBounceForce;
    public float spinForceMultiplier = 1.5f;
    public float additionalSpinForce = 5f;  // 추가 회전력
    private Coroutine currentBounceCoroutine;  // 현재 실행 중인 코루틴 참조
    private Vector3 originalScale;  // 원래 크기 저장

    AudioSource sound;
    void Start()
    {
        originalScale = transform.localScale;  // 시작할 때 원래 크기 저장
        sound = GetComponent<AudioSource>();
    }

    public override void OnCollisionEnter(Collision collision)
    {
        // 럭비공인지 확인
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball == null) return;
        sound.Play();
        Rigidbody rb = ball.rb;
        Vector3 angular = rb.angularVelocity;
        Vector3 normal = collision.transform.position - transform.position;
        Vector3 bounceDir = normal.normalized;

        // 최종 힘 가하기
        rb.AddForce(bounceDir * baseBounceForce, ForceMode.Impulse);

        // 추가 회전력 적용 (반사 방향을 축으로 회전)
        Vector3 spinAxis = Vector3.Cross(bounceDir, Vector3.up).normalized;
        rb.AddTorque(spinAxis * additionalSpinForce, ForceMode.Impulse);

        // 이전 코루틴이 있다면 중지하고 크기 복원
        if (currentBounceCoroutine != null)
        {
            StopCoroutine(currentBounceCoroutine);
            transform.localScale = originalScale;  // 크기 즉시 복원
        }
        // 새로운 코루틴 시작
        currentBounceCoroutine = StartCoroutine(PumpingCoroutine());

        GameManager.Instance.AddScore((int)GameManager.Instance.Scoreweight);
    }

    private IEnumerator PumpingCoroutine()
    {
        Vector3 targetScale = originalScale * 1.3f;  // 원래 크기의 1.3배
        float time = 0f;
        float growDuration = 0.1f;  // 더 빠른 팽창
        float shrinkDuration = 1f;   // 더 천천히 수축
        
        // 커지는 애니메이션 (매우 빠르게)
        while(time < growDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, time / growDuration);
            time += Time.deltaTime;
            yield return null;
        }
        
        // 작아지는 애니메이션 (천천히)
        float shrinkStartTime = time;
        while(time < shrinkStartTime + shrinkDuration)
        {
            float shrinkProgress = (time - shrinkStartTime) / shrinkDuration;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, shrinkProgress);
            time += Time.deltaTime;
            yield return null;
        }
        
        transform.localScale = originalScale;
        currentBounceCoroutine = null;  
    }
}
