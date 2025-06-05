using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball == null) return;

        Destroy(ball.gameObject);
    }
}
