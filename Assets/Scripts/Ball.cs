using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    private const float DRAG_COEFFICIENT = 0.3f;  // 이동 공기 저항 계수
    private const float ANGULAR_DRAG_COEFFICIENT = 0.01f;  // 회전 공기 저항 계수 
    private const float MIN_VELOCITY = 0.1f;  

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 0f;  
        rb.angularDamping = 0f;  
    }

    void FixedUpdate()  
    {
        float speed = rb.linearVelocity.magnitude;
        if (speed > MIN_VELOCITY)
        {
            Vector3 dragForce = -rb.linearVelocity.normalized * (speed * DRAG_COEFFICIENT);
            rb.AddForce(dragForce, ForceMode.Force);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }

        // 회전 감속
        float angularSpeed = rb.angularVelocity.magnitude;
        Vector3 angularDrag = -rb.angularVelocity.normalized * (angularSpeed * ANGULAR_DRAG_COEFFICIENT);
        rb.AddTorque(angularDrag, ForceMode.Force);

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rb.AddForce(new Vector3(-5, 5, 0), ForceMode.Impulse);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            rb.AddForce(new Vector3(5, 5, 0), ForceMode.Impulse);
        }
    }
}
