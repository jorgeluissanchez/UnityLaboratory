using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    //PLAYER
    Player player;

    //ACCELERATION
    float accelerationOriginal;

    //SLOW
    [SerializeField] float slowAcceleration = 0.5f;

    void Start()
    {
        player = FindObjectOfType<Player>(); //find the player
        accelerationOriginal = player.moveAcceleration; //save the acceleration
    }

    //when the player enter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.moveAcceleration *= slowAcceleration; //decrement the acceleration
        }
    }

    //when the player exit
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.moveAcceleration = accelerationOriginal; //return the acceleration
        }
    }
}
