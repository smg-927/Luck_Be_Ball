using UnityEngine;

public class DoubleScore : Item
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.DoubleScore();
        }
    }
}
