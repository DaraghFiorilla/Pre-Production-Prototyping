using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]

public class MuzakScript : MonoBehaviour
{

    private StudioEventEmitter emitter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.dayMuzak, this.gameObject);
        MuzakPlay();
    }

    public void MuzakPlay()
    {
        emitter.Play();
    }

    public void MuzakStop()
    {
        emitter.Stop();
    }
}
