using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    //PREFABS 
    [SerializeField] GameObject prefabItem;
    [SerializeField] GameObject[] prefabsPowerUp;

    //ITEMS
    [SerializeField] int itemDelay = 10;
    [SerializeField] float itemSpawn = 10;

    //POWERUPS
    [SerializeField] int powerUpDelay = 10;
    [SerializeField] float powerUpSpawn = 1;

    void Start()
    {
        // start courtines
        StartCoroutine(spawnItem());
        StartCoroutine(spawnPowerUp());
    }
    //start items
    IEnumerator spawnItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(itemDelay);
            Vector2 itemPosition = Random.insideUnitCircle * itemSpawn;
            Instantiate(prefabItem, itemPosition, Quaternion.identity);
        }
    }
    //start powerUps
    IEnumerator spawnPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpDelay);
            Vector2 powerUpPosition = Random.insideUnitCircle * powerUpSpawn;
            int powerUp = Random.Range(0, prefabsPowerUp.Length);
            Instantiate(prefabsPowerUp[powerUp], powerUpPosition, Quaternion.identity);
        }
    }

}
