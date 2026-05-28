using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TriggerNextScene : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject interactPrompt;

    bool playerInTrigger;

    public MuzakScript music;

    private void Update()
    {
        if (playerInTrigger)
        {
            if (InputSystem.actions.FindAction("Interact").WasPressedThisFrame())
            {          
                LoadNextScene();
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

    void LoadNextScene()
    {
        music.CleanUp();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
