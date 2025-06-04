using System.Collections.Generic;
using UnityEngine;

public class SpringWithCollision : MonoBehaviour
{
    [Header("스프링 기본 설정")]
    public float moveSpeed = 5f;            // 좌우 이동 속도
    public float chargeSpeed = 2f;          // 압축 시 아래로 내려가는 속도
    public float returnSpeed = 10f;         // 복귀 시 올라가는 속도
    public float minScaleY = 0.5f;          // 스프링 최소 압축 비율
    public float maxSpringForce = 20f;      // 최대 힘
    public float bounceRadius = 0.5f;       // 튕김 범위 반지름

    public float minX = -0.3347658f;        // 좌우 이동 최소 X
    public float maxX = 0.3548303f;         // 좌우 이동 최대 X

    private Rigidbody rb;
    private Vector3 originalScale;
    private float currentScaleY;
    private bool isCharging = false;
    private bool wasChargingLastFrame = false;
    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        currentScaleY = originalScale.y;
        startPosition = transform.position;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    void Update()
    {
        // 1) 좌우 이동
        float h = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) h = 1f;
        else if (Input.GetKey(KeyCode.RightArrow)) h = -1f;

        Vector3 newPos = transform.position + new Vector3(h * moveSpeed * Time.deltaTime, 0f, 0f);
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        transform.position = newPos;

        // 2) 압축 중 아래로 내려가는 속도
        if (Input.GetKeyDown(KeyCode.DownArrow))
            isCharging = true;
        if (Input.GetKeyUp(KeyCode.DownArrow))
            isCharging = false;

        // 3) 압축 중인지 복귀 중인지에 따른 스프링 길이 변화
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

        // 4) 압축 중인 마지막 프레임과 복귀 중인 프레임 사이에 튕김 처리
        if (wasChargingLastFrame && !isCharging)
        {
            BounceNearbyBalls();
        }

        wasChargingLastFrame = isCharging;
    }

    void BounceNearbyBalls()
    {
        float topY = transform.position.y + (currentScaleY / 2f);
        Vector3 topCenter = new Vector3(transform.position.x, topY, transform.position.z);

        Collider[] hits = Physics.OverlapSphere(topCenter, bounceRadius);
        foreach (Collider col in hits)
        {
            if (col.CompareTag("RugbyBall"))
            {
                Rigidbody ballRb = col.attachedRigidbody;
                if (ballRb != null)
                {
                    float compressionRatio = 0f;
                    float fullRange = originalScale.y - (originalScale.y * minScaleY);
                    if (fullRange > 0.0001f)
                    {
                        compressionRatio = (originalScale.y - currentScaleY) / fullRange;
                        compressionRatio = Mathf.Clamp01(compressionRatio);
                    }

                    float forceToApply = maxSpringForce * compressionRatio;
                    ballRb.AddForce(Vector3.up * forceToApply, ForceMode.Impulse);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        float topY = transform.position.y + (currentScaleY / 2f);
        Vector3 topCenter = new Vector3(transform.position.x, topY, transform.position.z);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(topCenter, bounceRadius);
    }
}
