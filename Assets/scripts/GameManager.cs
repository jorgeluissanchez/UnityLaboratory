using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GAME MANAGER
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    //GAME OVER
    public bool gameOver;
    public bool gameWin;
    //GLOBALS
    [Range(1, 10)] public int globalDifficulty = 1;
    public int globalTime = 30;
    public int GlobalTime
    {
        get => globalTime;
        set
        {
            globalTime = value;
            UIManager.Instance.UpdateUITime(globalTime);
        }
    }
    [SerializeField]  int globalScore;
    public int GlobalScore
    {
        get => globalScore;
        set 
        {
            globalScore = value;
            UIManager.Instance.UpdateUIScore(globalScore);
            if (globalScore % 1000 == 0)
            {
                globalDifficulty++;
            }
        }
    }
    //Time Manage
    private void Start()
    {
        StartCoroutine(controlTime());
    }
    //contros of the time's decrement
    IEnumerator controlTime()
    {
        //time's decrement
        while(GlobalTime > 0)
        {
            yield return new WaitForSeconds(1);
            GlobalTime--;
            GlobalScore= GlobalScore + 100;
        }
        gameOver = true;
        UIManager.Instance.ShowGameOver();
    }
}
