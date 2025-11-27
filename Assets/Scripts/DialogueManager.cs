using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    public Queue<string> displayText;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject textArrow;
    private NightmareSwitch nmManager;
    private MainManager mainManager;
    private Camera mainCam;
    private bool canAdvance;
    [HideInInspector] public Transform speakingChar;
    [HideInInspector] public GameObject canvasToDisable;
    [SerializeField] private GameObject interactPrompt;

    private void Awake()
    {
        mainCam = Camera.main;
        nmManager = GetComponent<NightmareSwitch>();
        displayText = new Queue<string>();
        mainManager = GetComponent<MainManager>();
    }

    private void Update()
    {
        if (canAdvance)
        {
            if (InputSystem.actions.FindAction("Interact").WasPressedThisFrame())
            {
                canAdvance = false;
                if (displayText.Count > 0)
                {
                    UpdateText();
                }
                else
                {
                    Finish();
                }
            }
        }
    }

    public void AddText(string addedText)
    {
        displayText.Enqueue(addedText);
    }

    public void StartDialogue()
    {
        if (canvasToDisable != null) { canvasToDisable.SetActive(false); }
        interactPrompt.SetActive(false);
        mainCam.transform.LookAt(speakingChar);
        dialogueBox.SetActive(true);
        mainManager.canInteract = false;
        nmManager.PauseBlink();
        canAdvance = false;
        mainManager.playerController.canMove = false;
        UpdateText();
    }

    private void UpdateText()
    {
        textMesh.text = "";
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        textArrow.SetActive(false);
        for (int i = 0; i < displayText.Peek().Length + 1; i++)
        {
            textMesh.text = displayText.Peek().Substring(0, i);
            yield return new WaitForSeconds(scrollSpeed);
        }
        displayText.Dequeue();
        canAdvance = true;
        textArrow.SetActive(true);
    }

    private void Finish()
    {
        if (canvasToDisable != null) { canvasToDisable.SetActive(true); }
        textMesh.text = "";
        dialogueBox?.SetActive(false);
        nmManager.PauseBlink();
        mainManager.canInteract = true;
        mainManager.playerController.canMove = true;
    }
}
