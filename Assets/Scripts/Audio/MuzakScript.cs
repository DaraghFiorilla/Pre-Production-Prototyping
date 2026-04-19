using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[RequireComponent(typeof(StudioEventEmitter))]

public class MuzakScript : MonoBehaviour
{

    private StudioEventEmitter emitter;
    private EventInstance nightMusic;

    private bool isNight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        nightMusic = AudioManager.instance.CreateInstance(FMODEvents.instance.nightMusic);

        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.dayMuzak, this.gameObject);
        MuzakPlay();
    }

    void Update()
    {
        SoundUpdate();
    }

    public void MusicChange(bool night)
    {
        if (!night)
        {
            isNight= false;
            MuzakPlay();
        }
        else
        {
            isNight= true;
            MuzakStop();
        }
    }

    public void MuzakPlay()
    {
        emitter.Play();
    }

    public void MuzakStop()
    {
        emitter.Stop();
    }

    private void SoundUpdate()
    {
        if (isNight)
        {
            PLAYBACK_STATE playbackState;
            nightMusic.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                nightMusic.start();
            }
        }
        else
        {
            nightMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

    }
}
