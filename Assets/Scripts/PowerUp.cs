using UnityEngine;

public class PowerUp : Item
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RugbyBall"))
        {
            GameManager.Instance.PowerUp();
        }
        base.OnTriggerEnter(other);
    }

}
