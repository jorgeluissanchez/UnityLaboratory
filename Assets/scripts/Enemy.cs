using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //PLAYER
    Transform player;

    //ENEMY
    [SerializeField] int enemyHealth = 1;
    [SerializeField] float enemySpeed = 1;
    [SerializeField] int enemyPoints = 100;
    [SerializeField] SpriteRenderer enemySpriteRenderer;

    private void Start ()
    {
        //find the player tansform
        player = FindObjectOfType<Player>().transform;

        //find the spawn points
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");

        //randomly choose the spawn point
        int spawnPoint = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[spawnPoint].transform.position;
    }

    private void Update ()
    {
        //MOVE AND FLIP
        Vector2 direction = player.position - transform.position;
        transform.position += (Vector3)direction.normalized * Time.deltaTime * enemySpeed;

        if (player.position.x < transform.position.x)
        {
            enemySpriteRenderer.flipX = false;
        }
        else if (player.position.x > transform.position.x)
        {
            enemySpriteRenderer.flipX = true;
        }
    }

    //TAKE DAMAGE
    public void TakeDamage()
    {
        enemyHealth--; //take damage

        if (enemyHealth <= 0)
        {
            //destroy and claim points
            GameManager.Instance.GlobalScore += enemyPoints;
            Destroy(gameObject);
        }
    }
    
    //MAKE DAMAGE
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //make damage and autodestroy
            collision.GetComponent<Player>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
