using UnityEngine;

public class Opstacle_bigbounce : Opstacle
{
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            //많이 튀기기
        }
    }
}
