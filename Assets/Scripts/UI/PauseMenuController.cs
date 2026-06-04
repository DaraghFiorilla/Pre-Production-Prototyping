using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    public GameObject pauseMenu;
    private MainManager mainManager;
    public GameObject mainPause;
    public GameObject settings;

    public bool inMain;

    bool paused;
    bool inSettings;

    void Awake()
    {
        if (inMain)
        {

            mainManager = GetComponent<MainManager>();

        }

        mainPause.SetActive(false);
        settings.SetActive(false);
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (InputSystem.actions.FindAction("Pause").WasPressedThisFrame() && (!inMain || mainManager.canPause) && !(inSettings || paused))
        {

            Pause();

        }
        else if (InputSystem.actions.FindAction("Pause").WasPressedThisFrame() && (!inMain || mainManager.canPause) && inSettings)
        {

            CloseSettings();

        }
    }

    public void Pause()
    {
        Time.timeScale = 0;

        if (inMain)
        {

            mainManager.PauseMinigames();

            playerController.canMove = false;

        }

        pauseMenu.SetActive(true);
        mainPause.SetActive(true);
        settings.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        paused = true;
    }

    public void Resume()
    {
        if (inMain)
        {

            mainManager.UnpauseMinigames();

            playerController.canMove = true;

        }

        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuButton, this.transform.position);

        Time.timeScale = 1;

        pauseMenu.SetActive(false);
        mainPause.SetActive(false);
        settings.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        paused = false;
    }

    public void Restart()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuButton, this.transform.position);

        Time.timeScale = 1; //added as time was still set to 0 after restart

        SceneManager.LoadScene(0);
    }

    public void Settings()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuButton, this.transform.position);

        inSettings = true;

        mainPause.SetActive(false);
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuButton, this.transform.position);

        inSettings = false;

        mainPause.SetActive(true);
        settings.SetActive(false);
    }

    public void Quit()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuButton, this.transform.position);

        Application.Quit();
    }
}
