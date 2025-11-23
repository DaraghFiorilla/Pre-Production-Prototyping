using UnityEngine;
using UnityEngine.InputSystem;

public class StartCanMinigameTrigger : MonoBehaviour
{
    [SerializeField] CanMinigameManager myManager;
    [SerializeField] GameObject myCanvasObj;
    private bool playerInTrigger;

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
                myCanvasObj.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
