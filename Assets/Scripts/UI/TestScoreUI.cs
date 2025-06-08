using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TestScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private TextMeshProUGUI _remainBallText;

    // 두 개의 Image 컴포넌트
    [SerializeField] private Image neutralFaceImage;  // 무표정 이미지
    [SerializeField] private Image surprisedFaceImage;  // 놀란 표정 이미지

    private float timePassed = 0f;  // 타이머 (2초 동안 점수 상승 체크)
    private bool isSurprised = false;
    private long lastScore = 0; // 이전 점수 (2초 전 점수)
    private float surpriseDuration = 3f; // 놀란 표정 지속 시간
    private float surpriseScore = 600f;

    private void Start()
    {
        neutralFaceImage.gameObject.SetActive(true);
        surprisedFaceImage.gameObject.SetActive(false);
    }
    private void Update()
    {
        // 현재 게임 상태가 Play일 때만 점수 체크와 표정 변경
        if (GameManager.Instance.currentState == GameManager.GameState.Play)
        {
            // 점수와 고득점 표시
            _scoreText.text = "Score: " + GameManager.Instance.score;
            _highScoreText.text = "High Score: " + GameManager.Instance.highScore;
            _remainBallText.text = "Remain Ball: " + GameManager.Instance.ballCount;

            // 점수 차이를 계산
            long scoreDifference = GameManager.Instance.score - lastScore;

            // 점수 차이가 500 이상이면 놀란 표정으로 변경
            if (scoreDifference >= surpriseScore && !isSurprised)
            {
                ChangeToSurprisedFace();
            }
            if(!isSurprised)
            {
                timePassed += Time.deltaTime;
            }
            if(timePassed >=3f)
            {
                lastScore = GameManager.Instance.score;
                timePassed = 0f;
            }
            // 타이머 체크 (2초 동안 점수 증가한 것)
            if (isSurprised)
            {
                timePassed += Time.deltaTime;  // 타이머 증가

                // 2초 동안 놀란 표정을 유지
                if (timePassed >= surpriseDuration && scoreDifference < surpriseScore)
                {
                    ResetToNeutralFace();  // 2초가 지나면 무표정으로 변경
                }
            }
        }
    }

    private void ChangeToSurprisedFace()
    {
        if (neutralFaceImage != null && surprisedFaceImage != null)
        {
            // 놀란 표정 이미지 활성화, 무표정 이미지는 비활성화
            surprisedFaceImage.gameObject.SetActive(true);
            neutralFaceImage.gameObject.SetActive(false);
            isSurprised = true; // 놀란 표정 상태로 변경
            lastScore = GameManager.Instance.score; // 점수 기록
            timePassed = 0f; // 타이머 초기화
        }
    }

    private void ResetToNeutralFace()
    {
        if (neutralFaceImage != null && surprisedFaceImage != null)
        {
            // 무표정 이미지 활성화, 놀란 표정 이미지는 비활성화
            neutralFaceImage.gameObject.SetActive(true);
            surprisedFaceImage.gameObject.SetActive(false);
            isSurprised = false;
            timePassed = 0f;  // 타이머 초기화
        }
    }
}
