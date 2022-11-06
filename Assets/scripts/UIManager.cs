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
    [SerializeField] Text crashText;
    [SerializeField] Text scoreText;
    [SerializeField] Text pauseText;
    [SerializeField] Text timeText;
    [SerializeField] Text speedXText;
    [SerializeField] Text speedYText;
    [SerializeField] Text finalScoreText2;
    //SCREEN
    [SerializeField] GameObject OverGame;
    [SerializeField] GameObject WinGame;
    [SerializeField] GameObject Game;
    [SerializeField] GameObject Records;
    [SerializeField] GameObject Credits;
    //MUSIC
    [SerializeField] GameObject CamaraController;
    //Instantiate
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
    public void UpdateUICrash(int newCrash)
    {
        crashText.text = newCrash.ToString();
    }
    //MANAGE TIME TEXT
    public void UpdateUITime(int newTime)
    {
        timeText.text = newTime.ToString();
    }
    //MANAGE TIME TEXT
    public void UpdateUISpeedX(float newSpeedX)
    {
        speedXText.text = newSpeedX.ToString();
    }
    //MANAGE TIME TEXT
    public void UpdateUISpeedY(float newSpeedY)
    {
        speedYText.text = newSpeedY.ToString();
    }
    //MANAGE GAME OVER SCREEN
    public void ShowGameOver()
    {
        Time.timeScale = 0; 
        OverGame.SetActive(true); 
        Game.SetActive(false); 
    }
    //MANAGE WIN GAME SCREEN
    public void ShowGameWin()
    {
        Time.timeScale = 0; 
        WinGame.SetActive(true);
        Game.SetActive(false); 
        finalScoreText2.text = "score: "+ GameManager.Instance.GlobalScore; 
    }
    //MANAGE PAUSE SCREEN
    public void PauseGame ()
    {
        if (Time.timeScale == 1)
        {
            pauseText.text = "Continuar";
            Time.timeScale = 0;
            CamaraController.GetComponent<AudioSource>().Pause();
        }
        else
        {
            pauseText.text = "Pausar";
            Time.timeScale = 1;
            CamaraController.GetComponent<AudioSource>().Play();
        }
    }
    //MANAGE GAME CREDITS SCREEN
    public void ShowCredits()
    {
        Credits.SetActive(true);
        Records.SetActive(false); 
    }
    //MANAGE GAME RECORDS SCREEN
    public void ShowRecords()
    {
        Credits.SetActive(false); 
        Records.SetActive(true);
    }
    //MANAGE GAME RESTART
    public void ShowTitle()
    {
        Credits.SetActive(false);
        Records.SetActive(false);
    }
    //MANAGE GAME RESTART
    public void StartAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    //MANAGE PLAY AGAIN BUTTON
    public void PlayAgain()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("Game"); 
    }
}
