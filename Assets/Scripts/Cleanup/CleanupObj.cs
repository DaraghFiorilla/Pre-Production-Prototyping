using UnityEngine;
using FMODUnity;

public class CleanupObj : MonoBehaviour
{
    public float cleanSpeed;
    public float objHealth = 100;
    private Material mat;
    public int flagID;
    [SerializeField] private EventProgress eventManager;

    private bool isPlaying;

    private StudioEventEmitter emitter;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.mopSFX, this.gameObject);
    }

    public void Clean()
    {
        objHealth -= cleanSpeed * Time.deltaTime;
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, objHealth / 100);
        PlaySound();
        if (objHealth <= 0)
        {
            StopSound();
            eventManager.UpdateFlag(flagID);
            Destroy(gameObject);
        }

    }

    private void PlaySound()
    {
        if (isPlaying)
        {
            return;
        }
        isPlaying = true;
        emitter.Play();
    }

    public void StopSound()
    {
        isPlaying = false;
        emitter.Stop();
    }
}
