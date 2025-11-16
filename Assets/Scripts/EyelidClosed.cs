using UnityEngine;

public class EyelidClosed : MonoBehaviour
{
    [SerializeField] private NightmareSwitch manager;
    
    public void Close()
    {
        if (!manager.nightmareState) { manager.EyesClosed(); }
        else { manager.Switch(); }
    }
}
