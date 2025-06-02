using UnityEngine;

public class Shield : Item
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            //쉴드 효과
        }
    }
}