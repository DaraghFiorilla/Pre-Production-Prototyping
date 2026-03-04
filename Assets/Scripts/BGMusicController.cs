using UnityEngine;

public class BGMusicController : MonoBehaviour
{

    public bool nightmare;
    public bool swapTime;

    public MusicScript dayMusic;
    public MusicScript nightMusic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (swapTime)
        {

            if (nightmare)
            {

                enterDay();

            }
            else
            {

                enterNight();

            }

            swapTime = false;

        }

    }

    public void enterDay()
    {
        if (nightmare)
        {

            nightmare = false;
            dayMusic.MusicStart();
            nightMusic.MusicEnd();

        }


    }

    public void enterNight()
    {
        if (!nightmare)
        {

            nightmare = true;
            dayMusic.MusicEnd();
            nightMusic.MusicStart();

        }

    }

}
