using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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

    public enum GameState
    {
        None,
        Start,
        Play,
        Pause,
        GameOver,
        Result,
    }

    public GameState currentState{get; private set;}
    
    #region 게임 변수
    public int score{get; private set;}
    public int highScore{get; private set;}
    public int ballCount{get; private set;}
    public int ballCountMax{get; private set;}
    #endregion

    #region 프리펩
    public GameObject ballPrefab;
    private GameObject ball = null;

    #endregion

    public void SwitchGameState(GameState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
    }

    public void InitGame()
    {
        score = 0;
        SwitchGameState(GameState.Play);
        Debug.Log("currnetState: " + currentState);
        ball = FindAnyObjectByType<Ball>().gameObject;
        if (ball == null)
        {
            SpawnBall();
        }
    }

    private void SpawnBall()
    {
        if (ball != null)
        {
            Destroy(ball);
        }

        ball = Instantiate(ballPrefab, ballPrefab.transform.position, ballPrefab.transform.rotation);
    }

    void Update()
    {
        //if (currentState == GameState.Play)
        //{
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("currentState: " + currentState);
                ResetGame();
            }
        //}
    }

    private void ResetGame()
    {
        SpawnBall();
        SwitchGameState(GameState.Play);
    }
}


