using UnityEngine;

public class BallReset : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody rb;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }
}
