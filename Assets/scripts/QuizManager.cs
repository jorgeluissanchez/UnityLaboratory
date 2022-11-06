using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Text;
using System.Runtime.Serialization.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    //VARIABLES
    public List<QuestionsAndAnswers> questionsAndAnswers;
    Slow slow;
    public GameObject[] options;
    public GameObject QuizScreen;
    public Text questionText;
    public int currentQuestion;

    // CALLING THE API
    private async void Start()
    { 
        string url = "https://appracingcrash.herokuapp.com/api/pregunta-respuesta";
        using var www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Content-Type", "application/json");
        var operation = www.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }

        var jsonres = www.downloadHandler.text;

        try
        { 
            questionsAndAnswers = new List<QuestionsAndAnswers>(JsonHelper.JsonDeserialize<QuestionsAndAnswers[]>(jsonres));
            GenerateQuestion();
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    } 
    // GENERATE QUESTION
    void  GenerateQuestion()
    {
        currentQuestion = Random.Range(0, questionsAndAnswers.Count);
        questionText.text = questionsAndAnswers[currentQuestion].pregunta;
        setAnswers();
    }
    // SET ANSWERS
    void setAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = questionsAndAnswers[currentQuestion].respuestas[i];
            if (questionsAndAnswers[currentQuestion].respuestaCorrecta == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }
    // NEXT QUESTION
    public void correct()
    {
        QuizScreen.SetActive(false);
        Time.timeScale = 1;
        GenerateQuestion();
    }
    // WRONG ANSWER
    public void wrong()
    {
        QuizScreen.SetActive(false);
        Time.timeScale = 1;
        GenerateQuestion();
        FindObjectOfType<Player>().MoveSpeedY /= 4;
    }

}