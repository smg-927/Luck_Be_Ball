using UnityEngine;
using System.Collections;

public class Itemspawner : MonoBehaviour
{
    [SerializeField] private GameObject PowerUp;
    [SerializeField] private GameObject DoubleScore;
    [SerializeField] private GameObject BallCountUp;
    [SerializeField] private ItemSpot itemSpot;
    private bool isSpawned = false;

    GameObject item;

    public void SpawnItem()
    {
        int randomIndex = Random.Range(0, itemSpot.itemSpot.Length);
        int randomItem = Random.Range(0, 3);

        switch(randomItem)
        {
            case 0:
                item = Instantiate(PowerUp, itemSpot.itemSpot[randomIndex], Quaternion.identity);
                break;
            case 1:
                item = Instantiate(DoubleScore, itemSpot.itemSpot[randomIndex], Quaternion.identity);
                break;
            case 2:
                item = Instantiate(BallCountUp, itemSpot.itemSpot[randomIndex], Quaternion.identity);
                break;
        }
    }
    public IEnumerator SpawnItemCoroutine()
    {
        yield return new WaitForSeconds(5f);
        SpawnItem();   
    }
}
