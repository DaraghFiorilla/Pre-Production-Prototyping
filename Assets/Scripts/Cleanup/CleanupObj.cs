using UnityEngine;
using FMODUnity;

public class CleanupObj : MonoBehaviour
{
    public float cleanSpeed;
    public float objHealth = 100;
    public int flagID;
    [SerializeField] private EventProgress eventManager;

    private bool isPlaying;

    private void Awake()
    {

    }

    public void Clean()
    {
        objHealth -= cleanSpeed * Time.deltaTime;
        if (objHealth <= 0)
        {
            eventManager.UpdateFlag(flagID);
            Destroy(gameObject);
        }

    }
}
