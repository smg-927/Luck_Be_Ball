using UnityEngine;

public class Addball : Item
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RugbyBall"))
        {
            GameManager.Instance.AddBall();
        }
        base.OnTriggerEnter(other);
    }
}
