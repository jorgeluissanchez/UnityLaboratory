using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    //PLAYER
    Player player;
    Robot robot;
    [SerializeField] GameObject QuizScreen;
    public QuizManager quizManager;
    public int state;
    // PLAYER HEALTH
    [SerializeField] int playerCrash = 0;
    public int PlayerCrash
    {
        get => playerCrash;
        set
        {
            playerCrash = value;
            UIManager.Instance.UpdateUICrash(playerCrash);
        }
    }
    //when the player enter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  
        {
            Time.timeScale = 0;
            QuizScreen.SetActive(true);
            PlayerCrash++;
            GameManager.Instance.GlobalScore -= 500;
        }
    }
}
