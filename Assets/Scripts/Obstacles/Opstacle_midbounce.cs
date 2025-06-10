
using UnityEngine;

public class Opstacle_midbounce : Opstacle
{
    public float spinForceMultiplier = 1.5f;
    public float baseBounceForce;

    // public override void OnCollisionEnter(Collision collision)
    // {
    //     // 럭비공인지 확인
    //     Ball ball = collision.gameObject.GetComponent<Ball>();
    //     if (ball == null) return;

    //     Rigidbody rb = ball.rb;
    //     baseBounceForce = rb.linearVelocity.magnitude;
    //     Debug.Log("baseBounceForce: " + baseBounceForce);
    //     Vector3 angular = rb.angularVelocity;
    //     ContactPoint contact = collision.GetContact(0);
    //     Vector3 normal = contact.normal;

    //     // 회전에 따른 반사 방향 계산
    //     Vector3 spinDirection = Vector3.Cross(angular, normal).normalized;

    //     // 반사력 = 기본 튐 + 회전 기반 방향 조절
    //     Vector3 bounceDir = Vector3.Reflect(rb.linearVelocity.normalized, normal);
    //     bounceDir += spinDirection * spinForceMultiplier;

    //     // 최종 힘 가하기
    //     rb.AddForce(bounceDir.normalized * baseBounceForce, ForceMode.Impulse);
    // }
    
}
