using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // MAIN INSTANCE
    public static UIManager Instance;

    //TEXTS
    [SerializeField] Text healthText;
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    [SerializeField] Text finalScoreText;

    //SCREEN
    [SerializeField] GameObject OverGame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    //MANAGE SCORE TEXT
    public void UpdateUIScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
    //MANAGE HEALTH TEXT
    public void UpdateUIHealth(int newHealth)
    {
        healthText.text = newHealth.ToString();
    }
    //MANAGE TIME TEXT
    public void UpdateUITime(int newTime)
    {
        timeText.text = newTime.ToString();
    }
    //MANAGE GAME OVER SCREEN
    public void ShowGameOver()
    {
        Time.timeScale = 0; //stop game
        OverGame.SetActive(true); //show the screen
        finalScoreText.text = "Score: " + GameManager.Instance.GlobalScore; //set the final score
    }

    //MANAGE PLAY AGAIN BUTTON
    public void PlayAgain()
    {
        Time.timeScale = 1; // play the game
        SceneManager.LoadScene("Game"); //reload the main scene
    }
}
