using UnityEngine; 
using System.Collections;

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
    public int Scoreweight{get; private set;} = 100;
    public int score{get; private set;}
    public int highScore{get; private set;}
    public int ballCount{get; private set;}
    public int ballCountMax;
    #endregion

    #region 프리펩
    public GameObject ballPrefab;
    private GameObject ball = null;
    private GameObject spring;
    private Vector3 springPosition;
    #endregion

    public void SwitchGameState(GameState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
    }

    public void InitGame()
    {
        this.score = 0;
        ballCount = ballCountMax;
        SwitchGameState(GameState.Play);
        Debug.Log("currentState: " + currentState);

        spring = FindAnyObjectByType<SpringWithCollision>()?.gameObject;
        springPosition = spring.transform.position;
        Debug.Log("springPosition: " + springPosition);

        // Ball 초기화
        Ball existingBall = FindAnyObjectByType<Ball>();
        if (existingBall != null)
        {
            ball = existingBall.gameObject;
        }
        else
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
        if (currentState == GameState.Play)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("currentState: " + currentState);
                ResetGame();
            }
        }
    }

    public void ResetGame()
    {
        ballCount--;
        SpawnBall();
        ResetSpring();
        SwitchGameState(GameState.Play);
    }

    private void ResetSpring()
    {
        if (spring == null)
        {
            Debug.Log("GameManager: Spring 오브젝트를 찾을 수 없습니다.");
            spring = FindAnyObjectByType<SpringWithCollision>()?.gameObject;
        }
        spring.transform.position = springPosition;
    }

    #region 점수관련
    public void AddScore(int score)
    {
        this.score += score;
        Debug.Log("score: " + this.score);
    }

    #endregion

    #region 아이템
    public void DoubleScore()
    {
        Debug.Log("Scoreweight: " + Scoreweight);
        StartCoroutine(DoubleScoreCoroutine());
    }

    public IEnumerator DoubleScoreCoroutine()
    {
        Scoreweight *= 2;
        yield return new WaitForSeconds(10f);
        Scoreweight /= 2;
    }

    #endregion

}


