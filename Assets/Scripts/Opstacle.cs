using UnityEngine;

public class Opstacle : MonoBehaviour
{
    public virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            // 충돌 처리
        }
    }
}
