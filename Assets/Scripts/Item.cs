using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public static event Action<Item> OnItemDestroyed;

    public virtual void OnTriggerEnter(Collider other)
    {
        // 기본적인 충돌 처리
        // 자식 클래스에서 이 메서드를 오버라이드하여 구체적인 동작을 구현할 수 있습니다

        if(other.gameObject.CompareTag("RugbyBall"))
        {
            Destroy();
        }
    }

    public virtual void Destroy()
    {
        GameManager.Instance.transform.GetComponent<Itemspawner>().StartCoroutine(GameManager.Instance.transform.GetComponent<Itemspawner>().SpawnItemCoroutine());
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0);
    }
}
