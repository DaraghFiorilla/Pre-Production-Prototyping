using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerPrompt : MonoBehaviour
{
    //Haaaaaaaaaaaaaiiiiiiiiiiiiiiiiiii IM STEALING THIS :3 :3 <3
    [Header("Objects")]
    [SerializeField] GameObject miniGame;
    [SerializeField] GameObject interactPrompt;
    [SerializeField] GameObject breakTrigger;

    [SerializeField] GameObject camDisable;

    [SerializeField] private PlayerController playerController;

    bool playerInTrigger;

    public MainManager mainManager;

    private void Update()
    {
        if (playerInTrigger)
        {
            if (InputSystem.actions.FindAction("Interact").WasPressedThisFrame())
            {
                interactPrompt.SetActive(false);
                miniGame.SetActive(true);
                camDisable.SetActive(false);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                playerController.canMove = false;

                mainManager.nightmareManager.PauseBlink(true);
                
                Destroy(breakTrigger);
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
