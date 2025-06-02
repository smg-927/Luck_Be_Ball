using System.Collections.Generic;
using UnityEngine;

public class SpringWithCollision : MonoBehaviour
{
    [Header("������ ������ ����")]
    public float moveSpeed = 5f;            // �¿� �̵� �ӵ�
    public float chargeSpeed = 2f;          // ��¡ �� �Ʒ��� �þ�� �ӵ�
    public float returnSpeed = 10f;         // ���� �� ���� �ӵ�
    public float minScaleY = 0.5f;          // ������ �ּ� ���� ����
    public float maxSpringForce = 20f;      // �ִ� ��
    public float bounceRadius = 0.5f;       // ���� ƨ�ܳ� �ݰ�

    public float minX = -0.3347658f;        // �¿� �̵� �ּ� X
    public float maxX = 0.3548303f;         // �¿� �̵� �ִ� X

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
        // 1) �¿� �̵�
        float h = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) h = 1f;
        else if (Input.GetKey(KeyCode.RightArrow)) h = -1f;

        Vector3 newPos = transform.position + new Vector3(h * moveSpeed * Time.deltaTime, 0f, 0f);
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        transform.position = newPos;

        // 2) ��¡ ����
        if (Input.GetKeyDown(KeyCode.DownArrow))
            isCharging = true;
        if (Input.GetKeyUp(KeyCode.DownArrow))
            isCharging = false;

        // 3) ������ ���� / ����
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

        // 4) ���� ������ �� ƨ���
        if (wasChargingLastFrame && !isCharging)
        {
            BounceNearbyBalls();
        }

        wasChargingLastFrame = isCharging;

        //����ġ
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            transform.position = startPosition;
        }

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
