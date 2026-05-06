using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{

    public void LoadGame()
    {

        SceneManager.LoadScene(1);
        Debug.Log("Clicked");

    }

    public void Quit()
    {

        Application.Quit();

    }
}
