using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StartScreenVideoRemove : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject removeVid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer.loopPointReached += vidFinish;
    }

    void OnDestroy()
    {
        videoPlayer.loopPointReached -= vidFinish;
    }

    void vidFinish(VideoPlayer vp)
    {
        removeVid.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(InputSystem.actions.FindAction("Interact").WasPressedThisFrame())
        {
            vidFinish(videoPlayer);
            Debug.Log("video should be skipped");
        }
    }
}
