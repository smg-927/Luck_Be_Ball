using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TestScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private TextMeshProUGUI _remainBallText;

    // �� ���� Image ������Ʈ
    [SerializeField] private Image neutralFaceImage;  // ��ǥ�� �̹���
    [SerializeField] private Image surprisedFaceImage;  // ��� ǥ�� �̹���

    private float timePassed = 0f;  // Ÿ�̸� (2�� ���� ���� ��� üũ)
    private bool isSurprised = false;
    private long lastScore = 0; // ���� ���� (2�� �� ����)
    private float surpriseDuration = 3f; // ��� ǥ�� ���� �ð�
    private float surpriseScore = 600f;

    private void Start()
    {
        neutralFaceImage.gameObject.SetActive(true);
        surprisedFaceImage.gameObject.SetActive(false);
    }
    private void Update()
    {
        // ���� ���� ���°� Play�� ���� ���� üũ�� ǥ�� ����
        if (GameManager.Instance.currentState == GameManager.GameState.Play)
        {
            // ������ ����� ǥ��
            _scoreText.text = "Score: " + GameManager.Instance.score;
            _highScoreText.text = "High Score: " + GameManager.Instance.highScore;
            _remainBallText.text = "Remain Ball: " + GameManager.Instance.ballCount;

            // ���� ���̸� ���
            long scoreDifference = GameManager.Instance.score - lastScore;

            // ���� ���̰� 500 �̻��̸� ��� ǥ������ ����
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
            // Ÿ�̸� üũ (2�� ���� ���� ������ ��)
            if (isSurprised)
            {
                timePassed += Time.deltaTime;  // Ÿ�̸� ����

                // 2�� ���� ��� ǥ���� ����
                if (timePassed >= surpriseDuration && scoreDifference < surpriseScore)
                {
                    ResetToNeutralFace();  // 2�ʰ� ������ ��ǥ������ ����
                }
            }
        }
    }

    private void ChangeToSurprisedFace()
    {
        if (neutralFaceImage != null && surprisedFaceImage != null)
        {
            // ��� ǥ�� �̹��� Ȱ��ȭ, ��ǥ�� �̹����� ��Ȱ��ȭ
            surprisedFaceImage.gameObject.SetActive(true);
            neutralFaceImage.gameObject.SetActive(false);
            isSurprised = true; // ��� ǥ�� ���·� ����
            lastScore = GameManager.Instance.score; // ���� ���
            timePassed = 0f; // Ÿ�̸� �ʱ�ȭ
        }
    }

    private void ResetToNeutralFace()
    {
        if (neutralFaceImage != null && surprisedFaceImage != null)
        {
            // ��ǥ�� �̹��� Ȱ��ȭ, ��� ǥ�� �̹����� ��Ȱ��ȭ
            neutralFaceImage.gameObject.SetActive(true);
            surprisedFaceImage.gameObject.SetActive(false);
            isSurprised = false;
            timePassed = 0f;  // Ÿ�̸� �ʱ�ȭ
        }
    }
}
