using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    private float lastLogTime = 0f;
    private const float LOG_INTERVAL = 0.5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
