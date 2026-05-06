using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[RequireComponent(typeof(StudioEventEmitter))]

public class MuzakScript : MonoBehaviour
{
    public StudioEventEmitter emitter;
    private EventInstance nightMusic;
    private EventInstance titleMusic;
    private EventInstance alarmSFX;

    public bool isNight;

    public bool titleScreen;

    public bool isEnd;

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
        alarmSFX = AudioManager.instance.CreateInstance(FMODEvents.instance.alarmSFX);
    }

    private void SoundUpdate()
    {
        if (isEnd)
        {
            PLAYBACK_STATE playbackState;
            alarmSFX.getPlaybackState(out playbackState);
            nightMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            titleMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                alarmSFX.start();
            }
        }
        else if (titleScreen)
        {
            PLAYBACK_STATE playbackState;
            titleMusic.getPlaybackState(out playbackState);
            nightMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            alarmSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                titleMusic.start();
            }
        }
        else if (isNight)
        {
            PLAYBACK_STATE playbackState;
            nightMusic.getPlaybackState(out playbackState);
            titleMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            alarmSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                nightMusic.start();
            }
        }
        else
        {
            nightMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            titleMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            alarmSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

    }
}
