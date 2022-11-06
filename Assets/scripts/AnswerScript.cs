using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    //variables
    public bool isCorrect = false;
    public QuizManager quizManager;
    //Quiz Validation
    public void Answer ()
    {
        if (isCorrect)
        {
            quizManager.correct();
        }
        else
        {
            quizManager.wrong();
        }
    }
}
