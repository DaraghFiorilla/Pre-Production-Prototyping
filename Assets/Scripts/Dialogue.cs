using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private string[] dialogueSentences;
    [SerializeField] private GameObject canvasToDisable;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private MainManager mainManager;
    [SerializeField] private GameObject interactPrompt;
    private bool playerInTrigger;

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
        if (playerInTrigger && InputSystem.actions.FindAction("Interact").WasPressedThisFrame())
        {
            StartDialogue();
        }
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
        Destroy(this);
    }
}
