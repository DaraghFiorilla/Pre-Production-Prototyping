using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneCrap : MonoBehaviour
{

    public MuzakScript music;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        music.CleanUp();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}