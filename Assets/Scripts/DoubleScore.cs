using UnityEngine;

public class DoubleScore : Item
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RugbyBall"))
        {
            GameManager.Instance.DoubleScore();
        }
        base.OnTriggerEnter(other);
    }
}
