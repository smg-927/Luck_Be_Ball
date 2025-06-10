using UnityEngine;

public class GameoverControll : MonoBehaviour
{
    public void Gameover()
    {
        this.gameObject.SetActive(true);
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
