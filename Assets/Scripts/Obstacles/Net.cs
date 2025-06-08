using UnityEngine;

public class Net : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RugbyBall"))
        {
            //축구 애니메이션 On
        }
    }
}
