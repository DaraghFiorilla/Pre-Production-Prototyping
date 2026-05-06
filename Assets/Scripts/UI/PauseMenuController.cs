using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

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

        playerController.canMove = false;

    }

    public void Resume()
    {
        if (inMain)
        {

            mainManager.UnpauseMinigames();

        }

        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuButton, this.transform.position);

        Time.timeScale = 1;

        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerController.canMove = true;
    }

    public void Restart()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuButton, this.transform.position);

        Time.timeScale = 1; //added as time was still set to 0 after restart

        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuButton, this.transform.position);

        Application.Quit();
    }
}
