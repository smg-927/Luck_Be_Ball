using UnityEngine;
<<<<<<< HEAD

public class GameoverControll : MonoBehaviour
{
    public void Gameover()
    {
        this.gameObject.SetActive(true);
=======
using TMPro;

public class GameoverControll : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreText;
    public void Gameover()
    {
        this.gameObject.SetActive(true);
        scoreText.text = "Final Score: " + GameManager.Instance.score.ToString();
>>>>>>> faa421388f34bdf976303e7b05f28d2bef8d2044
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
<<<<<<< HEAD
=======


>>>>>>> faa421388f34bdf976303e7b05f28d2bef8d2044
}
