using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{

    public GameObject pauseMenu;

    public bool paused;

    void Start()
    {

        pauseMenu.SetActive(false);

        paused = false;

    }

    void Update()
    {

        if (InputSystem.actions.FindAction("Pause").WasPressedThisFrame())
        {

            Pause();

        }

    }

    public void Pause()
    {

        if (!paused)
        {

            Time.timeScale = 0;

            pauseMenu.SetActive(true);

            paused = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

    }

    public void Resume()
    {

        if (paused)
        {

            Time.timeScale = 1;

            pauseMenu.SetActive(false);

            paused = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

    }

    public void Quit()
    {

        Application.Quit();

    }
}
