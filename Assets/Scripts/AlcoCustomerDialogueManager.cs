using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AlcoCustomerDialogueManager : MonoBehaviour
{

    [SerializeField] public string correctProductType;
    [SerializeField] private int requiredAmount;
    [SerializeField] private int currentAmount;
    public string tagText;

    public GameObject alcoholUICanvas;
    public PlayerController playerController;

    public List<AlcoCustomerDialogueQuestionsAndAnswers> QnA;
    public GameObject[] dialogueOptions;
    public int currentCustomerQuestion;

        public TextMeshProUGUI questionText;
    public string incorrectResponse = "No that doesn't seem right at all! Are you sure you know what you're doing?";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        alcoholUICanvas.SetActive(false);
        //GenerateQuestions();
    }

    public void CorrectAnswer()
    {
        Debug.Log("Correct!");

        if (currentCustomerQuestion == QnA.Count - 1)
        {
            DisableDialogueUI();
        }
        else
        {

            GenerateNextQuestion();
        }
    }
    public void IncorrectAnswer()
    {
        questionText.text = incorrectResponse;
    }

    void SetAnswers()
    {
        for (int i = 0; i < dialogueOptions.Length; i++)
        {
            dialogueOptions[i].GetComponent<AlcoCustomerAnswers>().isCorrect = false;
            dialogueOptions[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentCustomerQuestion].PlayerQuestions[i];

            if (QnA[currentCustomerQuestion].correctQuestion == i+1)
            {
                dialogueOptions[i].GetComponent<AlcoCustomerAnswers>().isCorrect = true;
            }
        }
    }

    public void EnableDialogueUI()
    {
        alcoholUICanvas.SetActive(true);
        playerController.pitchLimit = 5;
        playerController.SetDialoguePitchLimit();
        playerController.lookSensitvity.x = 0.1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GenerateQuestions();
    }

    public void DisableDialogueUI()
    {
        alcoholUICanvas.SetActive(false);
        playerController.pitchLimit = 180;
        playerController.DisableDialoguePitchLimit();
        playerController.lookSensitvity.x = 0.1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void GenerateQuestions()
    {
        currentCustomerQuestion = 0;

        questionText.text = QnA[currentCustomerQuestion].CustomerQuestion;

        SetAnswers();

    }

    void GenerateNextQuestion()
    {
        currentCustomerQuestion++;

        questionText.text = QnA[currentCustomerQuestion].CustomerQuestion;

        SetAnswers();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<StockBoxHandler>().stockTypeLabel == correctProductType)
        {
            currentAmount++;

            if (currentAmount == requiredAmount)
            {
                Debug.Log(correctProductType + " Wine Snob satisfied");
                Destroy(gameObject);
                //rbConstraints = RigidbodyConstraints.None;

            }
            Destroy(other.gameObject);

        }
        else
        {
            return;
        }
    }

}
