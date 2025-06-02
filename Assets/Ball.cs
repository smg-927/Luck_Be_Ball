using UnityEngine;

public class BallReset : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody rb;

    void Start()
    {
        // ���� ��ġ ����
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // ���� �ӵ� �ʱ�ȭ
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // ��ġ ����
            transform.position = startPosition;
        }
    }
}
