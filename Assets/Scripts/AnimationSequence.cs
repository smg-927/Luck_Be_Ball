// AnimationSequence.cs
using UnityEngine;
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
        if (image1 == null || image2 == null)
        {
            Debug.LogError("AnimationSequence: image1 또는 image2가 연결되지 않음");
            return;
        }
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        image1.SetActive(true);
        image2.SetActive(false);
        yield return new WaitForSeconds(displayTime);

        image1.SetActive(false);
        image2.SetActive(true);
        yield return new WaitForSeconds(displayTime);

        Debug.Log("AnimationSequence: 다음 씬 -> " + nextSceneName);
        // 여기서 바로 로딩하기보다 SceneController 호출
        SceneController.Instance.LoadSceneAsync(nextSceneName);
    }
}
