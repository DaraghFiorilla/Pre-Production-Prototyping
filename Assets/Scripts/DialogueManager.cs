using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    public Queue<string> displayText;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject textArrow;
    private NightmareSwitch nmManager;

    [SerializeField] private MainManager mainManager;


    private Camera mainCam;
    private bool canAdvance;
    [HideInInspector] public Transform speakingChar;
    [HideInInspector] public GameObject canvasToDisable;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private Dialogue[] dialoguesInScene;
    [SerializeField] private bool paused;

    [Header("Event flag variables")]
    public bool updateFlag;
    public int flagID;

    private void Awake()
    {
        mainCam = Camera.main;
        nmManager = GetComponent<NightmareSwitch>();
        displayText = new Queue<string>();
        mainManager = GetComponent<MainManager>();
    }

    private void Update()
    {
        if (canAdvance && !paused)
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

    public void Pause(bool isPaused)
    {
        paused = isPaused;
        foreach (Dialogue dialogue in dialoguesInScene)
        {
            dialogue.Pause(isPaused);
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
        mainCam.transform.LookAt(new Vector3(speakingChar.transform.position.x, mainCam.transform.position.y, speakingChar.transform.position.z));
        dialogueBox.SetActive(true);
        mainManager.canInteract = false;
        mainManager.nightmareManager.PauseBlink(true);
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
        mainManager.canPause = false;
        textArrow.SetActive(false);
        for (int i = 0; i < displayText.Peek().Length + 1; i++)
        {
            textMesh.text = displayText.Peek().Substring(0, i);
            yield return new WaitForSeconds(scrollSpeed);
        }
        displayText.Dequeue();
        canAdvance = true;
        textArrow.SetActive(true);
        mainManager.canPause = true;
    }

    private void Finish()
    {
        if (canvasToDisable != null) { canvasToDisable.SetActive(true); }
        textMesh.text = "";
        dialogueBox?.SetActive(false);
        mainManager.nightmareManager.PauseBlink(false);
        mainManager.canInteract = true;
        mainManager.playerController.canMove = true;

        if (updateFlag) mainManager.eventManager.UpdateFlag(flagID);
    }

    public void StartEvent(int id)
    {
        updateFlag = true;
        flagID = id;
    }
}
