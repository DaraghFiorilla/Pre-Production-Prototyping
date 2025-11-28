using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{

    public void LoadGame()
    {

        SceneManager.LoadScene(1);

    }

    public void Quit()
    {

        Application.Quit();

    }
}
