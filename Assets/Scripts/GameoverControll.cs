using UnityEngine;
using TMPro;

public class GameoverControll : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreText;
    public void Gameover()
    {
        this.gameObject.SetActive(true);
        scoreText.text = "Final Score: " + GameManager.Instance.score.ToString();
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
