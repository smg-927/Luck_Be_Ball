using UnityEngine;
using TMPro;
public class TestScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private TextMeshProUGUI _remainBallText;

    private void Update()
    {
        _scoreText.text = "Score: " + GameManager.Instance.score;
        _highScoreText.text = "High Score: " + GameManager.Instance.highScore;
        _remainBallText.text = "Remain Ball: " + GameManager.Instance.ballCount;
    }
}
