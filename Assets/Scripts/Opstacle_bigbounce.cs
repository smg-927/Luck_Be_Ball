using UnityEngine;
using System.Collections;

public class Opstacle_bigbounce : Opstacle
{
    public float baseBounceForce;
    public float spinForceMultiplier = 1.5f;
    public float additionalSpinForce = 5f;  // 추가 회전력

    public override void OnCollisionEnter(Collision collision)
    {
        // 럭비공인지 확인
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball == null) return;

        Rigidbody rb = ball.rb;
        Vector3 angular = rb.angularVelocity;
        Vector3 normal = collision.transform.position - transform.position;

        // 회전에 따른 반사 방향 계산
        Vector3 spinDirection = Vector3.Cross(angular, normal).normalized;

        // 반사력 = 기본 튐 + 회전 기반 방향 조절
        Vector3 bounceDir = Vector3.Reflect(rb.linearVelocity.normalized, normal);
        bounceDir += spinDirection * spinForceMultiplier;

        // 최종 힘 가하기
        rb.AddForce(bounceDir.normalized * baseBounceForce, ForceMode.Impulse);

        StartCoroutine(BigBounce());
    }

    private IEnumerator BigBounce()
    {
        Vector3 targetScale = new Vector3(1.3f, 1.3f, 1.3f);
        Vector3 currentScale = transform.localScale;
        float time = 0f;
        float duration = 0.1f;  // 0.5초에서 0.2초로 단축
        
        // 커지는 애니메이션
        while(time < duration)
        {
            transform.localScale = Vector3.Lerp(currentScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        
        // 작아지는 애니메이션
        while(time > 0f)
        {
            transform.localScale = Vector3.Lerp(currentScale, targetScale, time / duration);
            time -= Time.deltaTime;
            yield return null;
        }
        
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
