using UnityEngine;
using UnityEngine.InputSystem;

public class StartMeatMinigameTrigger : MonoBehaviour
{
    //HelpURLAttribute HELP HELP HELP HELP HELP HELP HELP HELP HELP HELP HELP HELP HELP HLEP
    [SerializeField] NEWRhythm meatMinigame;
    [SerializeField] GameObject interactPrompt;
    [SerializeField] GameObject disableTrigger;
    [SerializeField] GameObject disableTasklist;

    bool playerInTrigger;

    private void Update()
    {
        if (playerInTrigger)
        {
            if (InputSystem.actions.FindAction("Interact").WasPressedThisFrame())
            {
                interactPrompt.SetActive(false);
                meatMinigame.TriggerStart();
                disableTrigger.SetActive(false);
                disableTasklist.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            interactPrompt.SetActive(false);
        }
    }
}
