using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    public GameObject pauseMenu;
    private MainManager mainManager;

    public bool inMain;

    void Awake()
    {
        if (inMain)
        {

            mainManager = GetComponent<MainManager>();

        }

        pauseMenu.SetActive(false);
    }

    void Update()
    {

        if (InputSystem.actions.FindAction("Pause").WasPressedThisFrame() && (!inMain || mainManager.canPause))
        {

            Pause();

        }

    }

    public void Pause()
    {
        Time.timeScale = 0;

        if (inMain)
        {

            mainManager.PauseMinigames();

        }

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

    public void Restart()
    {

        SceneManager.LoadScene(0);

    }

    public void Quit()
    {

        Application.Quit();

    }
}
