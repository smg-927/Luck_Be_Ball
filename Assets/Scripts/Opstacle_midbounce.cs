using UnityEngine;

public class Opstacle_midbounce : Opstacle
{
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            //중간정도 튀기기
        }
    }
}
