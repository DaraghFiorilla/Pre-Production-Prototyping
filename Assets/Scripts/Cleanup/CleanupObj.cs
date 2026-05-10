using UnityEngine;
using FMODUnity;

public class CleanupObj : MonoBehaviour
{
    public float cleanSpeed;
    public float objHealth = 100;
    public int flagID;
    [SerializeField] private EventProgress eventManager;

    private bool isPlaying;

    public StudioEventEmitter emitter;

    private void Awake()
    {
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.mopSFX, this.gameObject);
    }

    public void Clean()
    {
        objHealth -= cleanSpeed * Time.deltaTime;
        if (objHealth <= 0)
        {
            emitter.Stop();
            eventManager.UpdateFlag(flagID);
            Destroy(gameObject);
        }

    }

    public void SoundStart()
    {
        if (!isPlaying)
        {
            emitter.Play();

            isPlaying = true;
        }
    }

    public void SoundEnd()
    {
        if (isPlaying)
        {
            emitter.Stop();

            isPlaying = false;
        }
    }
}
