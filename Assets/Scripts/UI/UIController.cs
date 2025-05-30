using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
    [SerializeField] public Canvas canvas;
    public Dictionary<string, Window> WindowSet = new Dictionary<string, Window>();
    public string currentwindow {get; private set;}

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        NewSceneLoaded();
    }
    void RemoveComponent()
    {
        RemoveAllChildren(canvas.gameObject);
    }

    void AddComponent(Transform parent)
    {
        WindowSet.Clear();
        GameObject initialwindow = null;
        for(int i =1; i <= 10; i++)
        {
            initialwindow = Resources.Load<GameObject>("Prefabs/UI/Windows/"+SceneManager.GetActiveScene().name+"/window"+i.ToString());
            if(initialwindow != null && initialwindow.GetComponent<Window>().isInitialwindow)
            {
                break;
            }
            else
            {
                initialwindow = null;
            }
        }

        if(initialwindow == null)
        {
            Debug.LogError($"해당 씬에 initialwindow이 없습니다.");
            return;
        }

        GameObject windowspawn = Instantiate(initialwindow, canvas.transform);
        windowspawn.name = initialwindow.name;
        Window window = windowspawn.GetComponent<Window>();

        WindowSet.Add(window.name, window);
        if(!window.isInitialwindow)
        {
            Debug.LogWarning($"해당 씬에 initialwindow이 없습니다. {currentwindow}로 설정합니다.");
        }

        currentwindow = window.name;

        if(currentwindow == null) // 초기 윈도우가 없다면 첫번째 윈도우로 설정
        {
            currentwindow = WindowSet.Values.First().name;
            Debug.LogWarning($"해당 씬에 initialwindow이 없습니다. {currentwindow}로 설정합니다.");
        }

        Setwindow(currentwindow);
    }

    public void NewSceneLoaded()
    {
        if (canvas == null)
        {
            Debug.LogError("Can't find canvas!");
        }
        RemoveComponent();
        AddComponent(canvas.transform);
    }

    #region 윈도우 설정
    public void Setwindow(string windowName)
    {
        currentwindow = windowName;

        if(!WindowSet.TryGetValue(windowName, out Window newwindow))
        {
            GameObject initialwindow = Resources.Load<GameObject>("Prefabs/UI/Windows/"+SceneManager.GetActiveScene().name+ "/" + windowName);
            if(initialwindow == null)
            {
                Debug.LogError($"해당 씬에 initialwindow이 없습니다.");
                return;
            }

            GameObject windowspawn = Instantiate(initialwindow, canvas.transform);
            windowspawn.name = initialwindow.name;
            Window window = windowspawn.GetComponent<Window>();

            WindowSet.Add(window.name, window);
        }
        
        foreach(Window window in WindowSet.Values)
        {
            if(window.name == windowName)
            {
                window.ShowAllUI();
            }
            else
            {
                window.HideAllUI();
            }
        }
    }

    public void SetwindowWithoutclosing(string windowName)
    {
        if(!WindowSet.TryGetValue(windowName, out Window newwindow))
        {
            GameObject initialwindow = Resources.Load<GameObject>("Prefabs/UI/Windows/"+SceneManager.GetActiveScene().name+ "/" + windowName);
            if(initialwindow == null)
            {
                Debug.LogError($"해당 씬에 "+ windowName+"이 없습니다.");
                return;
            }
            
            GameObject windowspawn = Instantiate(initialwindow, canvas.transform);
            windowspawn.name = initialwindow.name;
            Window window = windowspawn.GetComponent<Window>();

            WindowSet.Add(window.name, window);
        }
        WindowSet[windowName].ShowAllUI();
    }

    public void HideWindow(string windowName)
    {
        if(!WindowSet.TryGetValue(windowName, out Window newwindow))
        {
            return;
        }
        WindowSet[windowName].HideAllUI();
    }

    public void SetWindowBottom(string windowName)
    {
        if(!WindowSet.TryGetValue(windowName, out Window newwindow))
        {
            return;
        }
        WindowSet[windowName].transform.SetAsFirstSibling();
    }
    public IEnumerator Floatwindow(string windowName, float time)
    {
        UIManager.Instance.SetwindowWithoutclosing(windowName);
        yield return new WaitForSeconds(time);
        UIManager.Instance.HideWindow(windowName);
    }

#endregion
    void RemoveAllChildren(GameObject canvas)
    {
        foreach (Transform child in canvas.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}

