using UnityEngine;
using UnityEngine.InputSystem;

public class StartCanMinigameTrigger : MonoBehaviour
{
    [SerializeField] CanMinigameManager myManager;
    [SerializeField] GameObject myCanvasObj;
    private bool playerInTrigger;

    public MainManager mainManager;

    public GameObject playerDisableTrigger;

    [SerializeField] GameObject breakTrigger;

    [SerializeField] private PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myCanvasObj.SetActive(true);
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myCanvasObj.SetActive(false);
            playerInTrigger = false;
        }
    }

    private void Update()
    {
        if (playerInTrigger)
        {
            if (InputSystem.actions.FindAction("Interact").WasPressedThisFrame())
            {
                myManager.StartMinigame();
                enabled = false;
                myCanvasObj.SetActive(false);
                playerDisableTrigger.SetActive(false);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                playerController.canMove = false;

                mainManager.nightmareManager.PauseBlink(true);

                Destroy(breakTrigger);
            }
        }
    }
}
