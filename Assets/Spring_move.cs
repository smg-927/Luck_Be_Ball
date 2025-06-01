using System.Collections.Generic;
using UnityEngine;

public class SpringWithCollision : MonoBehaviour
{
    [Header("스프링 움직임 설정")]
    public float moveSpeed = 5f;    // 좌우 이동 속도
    public float chargeSpeed = 2f;    // 차징 시 아래로 늘어나는 속도
    public float returnSpeed = 10f;   // 복귀 시 빠른 속도
    public float minScaleY = 0.5f;  // 스프링이 압축될 최소 비율 (원래 크기의 몇 배)
    public float maxSpringForce = 20f; // 스프링 복귀 시 최대 힘

    // 충돌 또는 Overlap 체크용 반경
    public float bounceRadius = 0.5f;

    private Rigidbody rb;
    private Vector3 originalScale;    // 원래 스케일
    private float currentScaleY;    // 현재 Y축 스케일
    private bool isCharging = false;
    private bool wasChargingLastFrame = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        currentScaleY = originalScale.y;

        // 스프링은 중력과 물리 반응을 받지 않도록 고정
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    void Update()
    {
        // 1) 좌우 이동 (Transform 직접 변경)
        float h = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) h = -1f;
        else if (Input.GetKey(KeyCode.RightArrow)) h = 1f;
        transform.position += new Vector3(h * moveSpeed * Time.deltaTime, 0f, 0f);

        // 2) 차징 상태 감지
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isCharging = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isCharging = false;
        }

        // 3) 스케일 조절 (아래로 압축 ⇔ 원래 크기 복귀)
        if (isCharging)
        {
            currentScaleY -= chargeSpeed * Time.deltaTime;
            float minY = originalScale.y * minScaleY;
            if (currentScaleY < minY) currentScaleY = minY;
        }
        else
        {
            currentScaleY += returnSpeed * Time.deltaTime;
            if (currentScaleY > originalScale.y) currentScaleY = originalScale.y;
        }
        transform.localScale = new Vector3(originalScale.x, currentScaleY, originalScale.z);

        // 4) "차징 → 복귀 전환 순간"(KeyUp 순간)에 공을 튕겨냄
        //    이번 프레임(isCharging=false)이며, 이전 프레임에는 isCharging=true 였다면
        if (wasChargingLastFrame && !isCharging)
        {
            BounceNearbyBalls();
        }

        // 다음 프레임 비교를 위해 상태 저장
        wasChargingLastFrame = isCharging;
    }

    // 키를 뗄 때 스프링 꼭대기 근처에 있는 럭비공을 찾아 위로 튕겨냄
    private void BounceNearbyBalls()
    {
        // 1) 스프링 꼭대기(world 좌표) 계산
        //    transform.position은 스프링의 중심, 
        //    localScale.y/2 만큼 올려야 스프링 최상단(Y좌표).
        float topY = transform.position.y + (currentScaleY / 2f);
        Vector3 topCenter = new Vector3(transform.position.x, topY, transform.position.z);

        // 2) OverlapSphere로 반경(bounceRadius) 내 모든 콜라이더 가져오기
        Collider[] hits = Physics.OverlapSphere(topCenter, bounceRadius);
        foreach (Collider col in hits)
        {
            // 3) 태그가 "RugbyBall"인 경우에만 처리
            if (col.CompareTag("RugbyBall"))
            {
                Rigidbody ballRb = col.attachedRigidbody;
                if (ballRb != null)
                {
                    // 4) 압축 정도(0~1)를 계산: 
                    //    0 = 전혀 압축 안 됨, 1 = 완전히 최소 스케일에 도달
                    float compressionRatio = 0f;
                    float fullRange = originalScale.y - (originalScale.y * minScaleY);
                    if (fullRange > 0.0001f)
                    {
                        compressionRatio = (originalScale.y - currentScaleY) / fullRange;
                        if (compressionRatio < 0f) compressionRatio = 0f;
                        if (compressionRatio > 1f) compressionRatio = 1f;
                    }

                    // 5) 실제 적용할 힘 크기: 
                    //    최대 스프링 힘(maxSpringForce) × compressionRatio
                    float forceToApply = maxSpringForce * compressionRatio;

                    // 공에 위로 힘을 가함
                    ballRb.AddForce(Vector3.up * forceToApply, ForceMode.Impulse);
                }
            }
        }
    }

    // (테스트용) 충돌 영역을 시각화
    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        // 스프링 꼭대기 좌표
        float topY = transform.position.y + (currentScaleY / 2f);
        Vector3 topCenter = new Vector3(transform.position.x, topY, transform.position.z);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(topCenter, bounceRadius);
    }
}
