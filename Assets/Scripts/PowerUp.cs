using UnityEngine;

public class PowerUp : Item
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            //파워업 효과
        }
    }
}
