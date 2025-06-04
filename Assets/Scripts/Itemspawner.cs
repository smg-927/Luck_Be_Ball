using UnityEngine;

public class Itemspawner : MonoBehaviour
{
    [SerializeField] private GameObject PowerUp;
    [SerializeField] private GameObject DoubleScore;
    [SerializeField] private GameObject BallCountUp;

    public void SpawnItem()
    {
        Instantiate(PowerUp, transform.position, Quaternion.identity);
        Instantiate(DoubleScore, transform.position, Quaternion.identity);
        Instantiate(BallCountUp, transform.position, Quaternion.identity);
    }
}
