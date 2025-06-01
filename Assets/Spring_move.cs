using System.Collections.Generic;
using UnityEngine;

public class SpringWithCollision : MonoBehaviour
{
    [Header("������ ������ ����")]
    public float moveSpeed = 5f;    // �¿� �̵� �ӵ�
    public float chargeSpeed = 2f;    // ��¡ �� �Ʒ��� �þ�� �ӵ�
    public float returnSpeed = 10f;   // ���� �� ���� �ӵ�
    public float minScaleY = 0.5f;  // �������� ����� �ּ� ���� (���� ũ���� �� ��)
    public float maxSpringForce = 20f; // ������ ���� �� �ִ� ��

    // �浹 �Ǵ� Overlap üũ�� �ݰ�
    public float bounceRadius = 0.5f;

    private Rigidbody rb;
    private Vector3 originalScale;    // ���� ������
    private float currentScaleY;    // ���� Y�� ������
    private bool isCharging = false;
    private bool wasChargingLastFrame = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        currentScaleY = originalScale.y;

        // �������� �߷°� ���� ������ ���� �ʵ��� ����
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    void Update()
    {
        // 1) �¿� �̵� (Transform ���� ����)
        float h = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) h = -1f;
        else if (Input.GetKey(KeyCode.RightArrow)) h = 1f;
        transform.position += new Vector3(h * moveSpeed * Time.deltaTime, 0f, 0f);

        // 2) ��¡ ���� ����
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isCharging = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isCharging = false;
        }

        // 3) ������ ���� (�Ʒ��� ���� �� ���� ũ�� ����)
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

        // 4) "��¡ �� ���� ��ȯ ����"(KeyUp ����)�� ���� ƨ�ܳ�
        //    �̹� ������(isCharging=false)�̸�, ���� �����ӿ��� isCharging=true ���ٸ�
        if (wasChargingLastFrame && !isCharging)
        {
            BounceNearbyBalls();
        }

        // ���� ������ �񱳸� ���� ���� ����
        wasChargingLastFrame = isCharging;
    }

    // Ű�� �� �� ������ ����� ��ó�� �ִ� ������� ã�� ���� ƨ�ܳ�
    private void BounceNearbyBalls()
    {
        // 1) ������ �����(world ��ǥ) ���
        //    transform.position�� �������� �߽�, 
        //    localScale.y/2 ��ŭ �÷��� ������ �ֻ��(Y��ǥ).
        float topY = transform.position.y + (currentScaleY / 2f);
        Vector3 topCenter = new Vector3(transform.position.x, topY, transform.position.z);

        // 2) OverlapSphere�� �ݰ�(bounceRadius) �� ��� �ݶ��̴� ��������
        Collider[] hits = Physics.OverlapSphere(topCenter, bounceRadius);
        foreach (Collider col in hits)
        {
            // 3) �±װ� "RugbyBall"�� ��쿡�� ó��
            if (col.CompareTag("RugbyBall"))
            {
                Rigidbody ballRb = col.attachedRigidbody;
                if (ballRb != null)
                {
                    // 4) ���� ����(0~1)�� ���: 
                    //    0 = ���� ���� �� ��, 1 = ������ �ּ� �����Ͽ� ����
                    float compressionRatio = 0f;
                    float fullRange = originalScale.y - (originalScale.y * minScaleY);
                    if (fullRange > 0.0001f)
                    {
                        compressionRatio = (originalScale.y - currentScaleY) / fullRange;
                        if (compressionRatio < 0f) compressionRatio = 0f;
                        if (compressionRatio > 1f) compressionRatio = 1f;
                    }

                    // 5) ���� ������ �� ũ��: 
                    //    �ִ� ������ ��(maxSpringForce) �� compressionRatio
                    float forceToApply = maxSpringForce * compressionRatio;

                    // ���� ���� ���� ����
                    ballRb.AddForce(Vector3.up * forceToApply, ForceMode.Impulse);
                }
            }
        }
    }

    // (�׽�Ʈ��) �浹 ������ �ð�ȭ
    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        // ������ ����� ��ǥ
        float topY = transform.position.y + (currentScaleY / 2f);
        Vector3 topCenter = new Vector3(transform.position.x, topY, transform.position.z);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(topCenter, bounceRadius);
    }
}
