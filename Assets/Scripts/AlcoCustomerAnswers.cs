using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AlcoCustomerAnswers : MonoBehaviour
{

    public bool isCorrect = false;

    public AlcoCustomerDialogueManager quizManager;
    public void Answer()
    {
        if (isCorrect)
        {

            quizManager.CorrectAnswer();
        }
        else
        {
            Debug.Log("Wrong!");
            quizManager.IncorrectAnswer();

        }
    }
}
