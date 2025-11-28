using UnityEngine;
using UnityEngine.InputSystem;

public class StartMeatMinigameTrigger : MonoBehaviour
{
    //HelpURLAttribute HELP HELP HELP HELP HELP HELP HELP HELP HELP HELP HELP HELP HELP HLEP
    [SerializeField] NEWRhythm meatMinigame;
    [SerializeField] GameObject interactPrompt;
    bool playerInTrigger;

    private void Update()
    {
        if (playerInTrigger)
        {
            if (InputSystem.actions.FindAction("Interact").WasPressedThisFrame())
            {
                interactPrompt.SetActive(false);
                meatMinigame.TriggerStart();
                Destroy(gameObject);
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
