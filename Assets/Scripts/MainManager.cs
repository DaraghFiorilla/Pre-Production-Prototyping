using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public int canMinigamesNo;
    public int canMinigamesComplete;
    public int canMinigamesBeforeSwitch;

    public int meatMinigamesNo;
    public int meatMinigamesComplete;
    public int meatMinigamesBeforeSwitch;

    [HideInInspector] public NightmareSwitch nightmareManager;
    [HideInInspector] public DialogueManager dialogueManager;
    [HideInInspector] public NEWRhythm meatManager;
    [SerializeField] private CanMinigameManager[] canManagers;

    public EventProgress eventManager;
    public PlayerController playerController;

    public bool canInteract;
    public bool canPause;

    private void Awake()
    {
        nightmareManager = GetComponent<NightmareSwitch>();
        dialogueManager = GetComponent<DialogueManager>();
        meatManager = GetComponent<NEWRhythm>();
    }

    public void PauseMinigames()
    {
        nightmareManager.PauseBlink(true);

        meatManager.Pause(true);
        dialogueManager.Pause(true);

        foreach (CanMinigameManager manager in canManagers)
        {
            manager.Pause(true);
        }
    }

    public void UnpauseMinigames()
    {
        nightmareManager.PauseBlink(false);

        dialogueManager.Pause(false);

        foreach (CanMinigameManager manager in canManagers)
        {
            manager.Pause(false);
        }
    }

}
