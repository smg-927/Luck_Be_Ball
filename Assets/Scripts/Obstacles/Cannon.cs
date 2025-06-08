using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour
{
    [SerializeField] private float shootForce = 20f;  // 발사 힘
    [SerializeField] private float growDuration = 0.5f;  // 커지는 시간
    [SerializeField] private float shrinkDuration = 0.2f;  // 작아지는 시간
    [SerializeField] private float maxScale = 1.5f;  // 최대 크기 배수
    
    private Vector3 originalScale;  // 원래 크기
    private Coroutine currentAnimationCoroutine;  // 현재 실행 중인 코루틴

    void Start()
    {
        originalScale = transform.localScale;  // 시작할 때 원래 크기 저장
    }

    public void Fire(GameObject ball)
    {
        // 이전 애니메이션이 있다면 중지
        if (currentAnimationCoroutine != null)
        {
            StopCoroutine(currentAnimationCoroutine);
            transform.localScale = originalScale;
        }

        // 새로운 발사 애니메이션 시작
        currentAnimationCoroutine = StartCoroutine(FireAnimationCoroutine(ball));
    }

    private IEnumerator FireAnimationCoroutine(GameObject ball)
    {
        ball.transform.position = transform.position + new Vector3(0, 0, 0f);
        float time = 0f;
        Vector3 targetScale = originalScale * maxScale;

        // 커지는 애니메이션
        while (time < growDuration)
        {
            float progress = time / growDuration;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, progress);
            time += Time.deltaTime;
            yield return null;
        }

        // 공 발사
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        if (ballRb != null)
        {
            // 대포의 방향으로 발사
            Vector3 shootDirection = new Vector3(-1f,4,0).normalized;
            ball.SetActive(true);
            ballRb.isKinematic = false;
            ball.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            ballRb.AddForce(shootDirection * shootForce, ForceMode.Impulse);
        }

        // 작아지는 애니메이션
        time = 0f;
        while (time < shrinkDuration)
        {
            float progress = time / shrinkDuration;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, progress);
            time += Time.deltaTime;
            yield return null;
        }

        // 원래 크기로 복원
        transform.localScale = originalScale;
        currentAnimationCoroutine = null;
    }
}
