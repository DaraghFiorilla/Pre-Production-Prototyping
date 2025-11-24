using UnityEngine;

public class MainManager : MonoBehaviour
{
    public int canMinigamesNo;
    public int canMinigamesComplete;
    public int minigamesBeforeSwitch;
    [HideInInspector] public NightmareSwitch nightmareManager;
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
    }
}
