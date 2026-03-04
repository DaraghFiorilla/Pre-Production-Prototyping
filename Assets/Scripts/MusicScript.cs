using UnityEngine;

public class MusicScript : MonoBehaviour
{

    public AudioSource audioSource;
    public float loopStart;
    public float loopEnd;

    public float fadeTime;

    float timeElapsed = 0.0f;

    public bool playing;

    private void Start()
    {
        // Set the initial playback position to the loop start position
        audioSource.time = loopStart;

        if (playing)
        {

            MusicStart();

        }
        else
        {

            MusicEnd();

        }

    }

    private void Update()
    {
        // Check if the audio clip has reached the end of the loop section
        if (audioSource.time >= loopEnd)
        {
            // Set the time to the start of the loop section
            audioSource.time = loopStart;
        }

        if (!playing)
        {

            if (audioSource.volume > 0.05f)
            {
                audioSource.volume = Mathf.Lerp(1.0f, 0.0f, timeElapsed / fadeTime);
                timeElapsed += Time.deltaTime;
            }
            else
            {
                audioSource.volume = 0.0f;
                audioSource.Stop();

            }

        }
        else if (audioSource.volume < 0.95f)
        {

            audioSource.volume = Mathf.Lerp(0.0f, 1.0f, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;

        }
        else
        {

            audioSource.volume = 1.0f;

        }

    }

    public void MusicStart()
    {

        if (!playing)
        {

            timeElapsed = 0.0f;
            playing = true;
            audioSource.Play();

        }


    }

    public void MusicEnd()
    {

        if (playing)
        {

            timeElapsed = 0.0f;
            playing = false;

        }

    }

}
