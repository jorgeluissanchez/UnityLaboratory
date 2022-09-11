using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //BULLET
    [SerializeField] float bulletSpeed = 5;
    [SerializeField] int bulletHealth = 3;
    
    //POWER
    public bool powerBullet;


    void Start()
    {
        Destroy(gameObject, 3); // 3 live's seconds
    }

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * bulletSpeed; //bullet move
    }

    // BULLET COLLISON
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // enemy collision
        {
            collision.GetComponent<Enemy>().TakeDamage(); // make damage

            //POWER VALIDATOR
            if (!powerBullet)
            {
                Destroy(gameObject); //without power
            } 
            else
            {
                bulletHealth--; //with power
                //health validator
                if (bulletHealth <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}

