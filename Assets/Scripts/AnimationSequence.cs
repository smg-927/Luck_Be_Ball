using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class AnimationSequence : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;
    public float displayTime = 3f;
    public string nextSceneName = "pinball";

    void Start()
    {
        Debug.Log("AnimationSequence Start() 호출됨");

        // 이미지들이 제대로 연결되지 않았을 경우 방어
        if (image1 == null || image2 == null)
        {
            Debug.LogError("image1 또는 image2가 Inspector에서 연결되지 않았습니다.");
            return;
        }

        // 코루틴 시작
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        // 첫 이미지 표시
        Debug.Log("1. 첫 이미지 보여주기");
        image1.SetActive(true);
        image2.SetActive(false);
        yield return new WaitForSeconds(displayTime);

        // 두 번째 이미지 표시
        Debug.Log("2. 두 번째 이미지 보여주기");
        image1.SetActive(false);
        image2.SetActive(true);
        yield return new WaitForSeconds(displayTime);

        // 씬 전환
        Debug.Log("3. 다음 씬 로딩: " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
}
