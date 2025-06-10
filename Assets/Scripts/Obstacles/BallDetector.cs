using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class BallDetector : MonoBehaviour
{
    [SerializeField] public Cannon cannon;
    AudioSource sound;
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RugbyBall"))
        {
            StartCoroutine(BallCoroutine(other.gameObject));
        }
    }

    IEnumerator BallCoroutine(GameObject ball)
    {
        if(ball == null)
        {
            yield break;
        }
        ball.GetComponent<Rigidbody>().isKinematic = true;
        sound.Play();
        ball.transform.rotation = Quaternion.Euler(0, 0, -45);
        ball.transform.position = transform.position + new Vector3(0.02f, 0.02f, -0.04f);

        yield return new WaitForSeconds(0.5f);
        float time = Time.time;
        Vector3 InitialPosition = ball.transform.position;
        while (Time.time - time < 1f)
        {
            ball.transform.position = Vector3.Lerp(ball.transform.position, InitialPosition + new Vector3(0.1f, -0.1f, 0), 0.01f);
            yield return null;
        }
        ball.SetActive(false);
        cannon.Fire(ball);
        yield return null;
    }
}
