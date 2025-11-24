using UnityEngine;

public class MainManager : MonoBehaviour
{
    public int canMinigamesNo;
    public int canMinigamesComplete;
    [HideInInspector] public NightmareSwitch nightmareManager;
    public bool canInteract;

    private void Awake()
    {
        nightmareManager = GetComponent<NightmareSwitch>();
    }
}
