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
    public long Scoreweight{get; private set;} = 100;
    public long score{get; private set;}
    public long highScore{get; private set;}
    public int ballCount{get; private set;}
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
        switch(newState)
        {
            case GameState.Play:
                break;
            case GameState.GameOver:
                GameOver();
                break;
        }
    }

    public void InitGame()
    {
        this.score = 0;
        ballCount = 10;
        Debug.Log("ballCount: " + ballCount);
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

        Itemspawner itemspawner = this.transform.GetComponent<Itemspawner>();
        if(itemspawner == null)
        {
            Debug.LogError("Itemspawner 컴포넌트를 찾을 수 없습니다.");
        }
        itemspawner.SpawnItem();
    }

    private void SpawnBall()
    {
        if (ball != null)
        {
            Destroy(ball);
        }

        ball = Instantiate(ballPrefab, spring.transform.position+new Vector3(0,0.2f,0), ballPrefab.transform.rotation);
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
        if(currentState == GameState.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                InitGame();
            }
        }
    }

    public void ResetGame()
    {
        if(ballCount <= 0)
        {
            SwitchGameState(GameState.GameOver);
            return;
        }
        ballCount--;
        SpawnBall();
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

    private void GameOver()
    {
        Debug.Log("GameOver");
        if(score > highScore)
        {
            highScore = score;
        }
    }

    #region 점수관련
    public void AddScore(int score)
    {
        this.score += score;
    }

    #endregion

    #region 아이템
    public void DoubleScore()
    {
        Debug.Log("Scoreweight: " + Scoreweight);
        StartCoroutine(DoubleScoreCoroutine());
    }

    public void AddBall()
    {
        ballCount++;
    }

    public void PowerUp()
    {
        //파워 업 효과
    }

    public IEnumerator DoubleScoreCoroutine()
    {
        Scoreweight *= 2;
        yield return new WaitForSeconds(10f);
        Scoreweight /= 2;
    }

    #endregion

}


