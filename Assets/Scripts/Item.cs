using UnityEngine;

public class Item : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other)
    {
        // 기본적인 충돌 처리
        // 자식 클래스에서 이 메서드를 오버라이드하여 구체적인 동작을 구현할 수 있습니다

        if(other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Item 획득");
            Destroy();
        }
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }
}
