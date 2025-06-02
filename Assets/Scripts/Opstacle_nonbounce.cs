using UnityEngine;

public class Opstacle_nonbounce : Opstacle
{
    public override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            //안튀기기
        }
    }
}
