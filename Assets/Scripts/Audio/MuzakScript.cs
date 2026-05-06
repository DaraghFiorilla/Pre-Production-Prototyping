using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[RequireComponent(typeof(StudioEventEmitter))]

public class MuzakScript : MonoBehaviour
{

    public StudioEventEmitter emitter;
    private EventInstance nightMusic;
    private EventInstance titleMusic;

    private bool isNight;

    public bool titleScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        MusakInitialise();

        if (!isNight && !titleScreen)
        {
            MuzakPlay();
        }
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

    private void MusakInitialise()
    {
        //emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.dayMuzak, this.gameObject);

        nightMusic = AudioManager.instance.CreateInstance(FMODEvents.instance.nightMusic);
        titleMusic = AudioManager.instance.CreateInstance(FMODEvents.instance.titleMusic);
    }

    private void SoundUpdate()
    {
        if (titleScreen)
        {
            PLAYBACK_STATE playbackState;
            titleMusic.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                titleMusic.start();
            }
        }
        else if (isNight)
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
            titleMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

    }
}
