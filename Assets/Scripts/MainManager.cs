using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public int canMinigamesNo;
    public int canMinigamesComplete;
    public int minigamesBeforeSwitch;
    [HideInInspector] public NightmareSwitch nightmareManager;
    public PlayerController playerController;
    public bool canInteract;

    private void Awake()
    {
        nightmareManager = GetComponent<NightmareSwitch>();
    }

    public void UpdateCanMinigameNo()
    {
        canMinigamesComplete++;
        if (canMinigamesComplete == minigamesBeforeSwitch && !nightmareManager.nightmareState)
        {
            nightmareManager.Switch();
        }
        else if (canMinigamesComplete >= canMinigamesNo)
        {
            SceneManager.LoadScene(1);
        }
    }
}
