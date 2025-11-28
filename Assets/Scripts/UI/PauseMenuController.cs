using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{

    public GameObject pauseMenu;
    private MainManager mainManager;

    void Awake()
    {
        mainManager = GetComponent<MainManager>();
        pauseMenu.SetActive(false);
    }

    void Update()
    {

        if (InputSystem.actions.FindAction("Pause").WasPressedThisFrame() && mainManager.canPause)
        {

            Pause();

        }

    }

    public void Pause()
    {
        Time.timeScale = 0;

        mainManager.PauseMinigames();

        pauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void Resume()
    {
            Time.timeScale = 1;

            pauseMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
    }

    public void Quit()
    {

        Application.Quit();

    }
}
