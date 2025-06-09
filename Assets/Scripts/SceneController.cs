using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Diagnostics;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }
    public AudioSource ButtonClickSound;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            // 여기서 로딩 진행률을 처리할 수 있습니다.
            yield return null;
        }

        switch(sceneName)
        {
            case "pinball":
                SceneController.Instance.LoadSceneAsync("pinball");
                break;
            case "Animation":
                //SceneController.Instance.LoadSceneAsync("Animation");
                break;
            case "startScene":
                break;
            case "Ingame":
                break;
        }
    }
}
