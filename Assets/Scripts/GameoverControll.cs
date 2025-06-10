using UnityEngine;
using TMPro;

public class GameoverControll : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI scoreText;
    public void Gameover()
    {
        this.gameObject.SetActive(true);
        scoreText.text = "Your Score: " + GameManager.Instance.score;
    }

    public void Restart()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.InitGame();
    }

    public void Quit()
    {
        SceneController.Instance.LoadSceneAsync("startScene");
    }
}
