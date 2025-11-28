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
        if (!nightmareManager.pauseBlink) { nightmareManager.PauseBlink(); }
        meatManager.Pause(true);
        dialogueManager.Pause(true);
        foreach (CanMinigameManager manager in canManagers)
        {
            manager.Pause(true);
        }
    }

    public void UnpauseMinigames()
    {
        if (nightmareManager.pauseBlink) { nightmareManager.PauseBlink(); }
        meatManager.Pause(false);
        dialogueManager.Pause(false);
        foreach (CanMinigameManager manager in canManagers)
        {
            manager.Pause(false);
        }
    }

    public void UpdateCanMinigameNo()
    {
        canMinigamesComplete++;
        if (SwitchRequirementsMet())
        {
            nightmareManager.Switch();
        }
        else if (EndRequirementsMet())
        {
            SceneManager.LoadScene(1);
        }
    }

    public void UpdateMeatMinigameNo()
    {
        meatMinigamesComplete++;
        if (SwitchRequirementsMet())
        {
            nightmareManager.Switch();
        }
        else if (EndRequirementsMet())
        {
            SceneManager.LoadScene(1);
        }
    }

    public bool SwitchRequirementsMet()
    {
        if (canMinigamesComplete == canMinigamesBeforeSwitch && meatMinigamesComplete == meatMinigamesBeforeSwitch && !nightmareManager.nightmareState)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool EndRequirementsMet()
    {
        if (canMinigamesComplete >= canMinigamesNo && meatMinigamesComplete >= meatMinigamesBeforeSwitch)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
