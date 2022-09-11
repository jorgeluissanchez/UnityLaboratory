using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //ENEMY
    [SerializeField] GameObject[] enemyPrefabs;

    //ENEMY SPAWN
    [Range(1, 10)] [SerializeField] float enemySpawnRate = 1;


    void Start()
    {
        //use a coroutine to select de spawn enemy
        StartCoroutine(enemyToSpawn());
        
    }

    IEnumerator enemyToSpawn()
    {        
        while (true) { 
            yield return new WaitForSeconds(1 / enemySpawnRate); //wait
            //create the random spawn
            float random = Random.Range(0.0f, 1.0f);

            //use the ramdon dificulty
            if (random > GameManager.Instance.globalDifficulty * 0.1f)
            {
                Instantiate(enemyPrefabs[0]);
            } else
            {
                Instantiate(enemyPrefabs[1]);
            }
         }
    }
}
