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

    void Update()
    {
        // 단순한 사인파 움직임 (0.5f 높이로 1초 주기)
        float newY = Mathf.Sin(Time.time) * 0.5f;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        
        // 회전
        transform.Rotate(new Vector3(0, 0.1f, 0));
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }
}
