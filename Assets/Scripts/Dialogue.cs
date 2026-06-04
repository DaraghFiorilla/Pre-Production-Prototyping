using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private List<string> dialogueSentences;

    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private MainManager mainManager;

    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private GameObject canvasToDisable;
    
    [SerializeField] private Collider breakTrigger;

    private bool playerInTrigger;
    private bool paused;
    public bool active;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && mainManager.canInteract)
        {
            interactPrompt.SetActive(true);
            playerInTrigger = true;
        }
    }

    private void Update()
    {
        if (playerInTrigger && InputSystem.actions.FindAction("Interact").WasPressedThisFrame() && !paused && active)
        {
            StartDialogue();

            breakTrigger.enabled = false;
        }
    }

    public void Pause(bool isPaused)
    {
        paused = isPaused;
    }

    private void OnTriggerExit(Collider other)
    {
        interactPrompt.SetActive(false);
        playerInTrigger = false;
    }

    public void StartDialogue()
    {
        foreach (string s in dialogueSentences)
        {
            dialogueManager.displayText.Enqueue(s);
        }

        if (canvasToDisable != null) { dialogueManager.canvasToDisable = canvasToDisable; }
        dialogueManager.speakingChar = gameObject.transform;
        dialogueManager.StartDialogue();
        active = false;
    }

    public void ToggleActive()
    {
        if (active) active = false;
        else active = true;
    }

    public void ClearDialogue()
    {
        dialogueSentences.Clear();
    }

    public void AddDialougeLine(string[] lines)
    {
        foreach (string line in lines)
        dialogueSentences.Add(line);
    }
}
