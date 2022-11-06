using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player enter in the finish
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.gameWin = true;
            UIManager.Instance.ShowGameWin();
        }
        //if the robot enter in the finish
        if(collision.CompareTag("Robot"))
        {
            UIManager.Instance.ShowGameOver();
        }
    }
}