using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball == null) return;

        Destroy(ball.gameObject);
        Debug.Log("ballCount: " + GameManager.Instance.ballCount);
        if (GameManager.Instance.ballCount <= 0)
        {
            //GameManager.Instance.SwitchGameState(GameManager.GameState.GameOver);
        }
        else
        {
            GameManager.Instance.ResetGame();
        }
    }
}
