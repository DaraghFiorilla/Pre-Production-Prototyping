using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;

public class PlayerCleanup : MonoBehaviour
{
    public float raycastDistance;
    private RaycastHit hit;
    [SerializeField] private GameObject interactPrompt;
    private CleanupObj cleanupObj;

    private EventInstance mopSFX;

    bool mopping;

    void Start()
    {
        mopSFX = AudioManager.instance.CreateInstance(FMODEvents.instance.mopSFX);
    }

    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * raycastDistance;
        Debug.DrawRay(transform.position, forward, Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastDistance))
        {
            if (hit.transform.gameObject.CompareTag("Cleanup"))
            {
                interactPrompt.SetActive(true);
                Debug.Log("Hit cleanup");
                cleanupObj = hit.transform.GetComponent<CleanupObj>();
                if (InputSystem.actions.FindAction("Interact").IsPressed())
                {
                    Debug.Log("Interact held, cleaning");
                    cleanupObj.Clean();
                    mopping = true;
                }
                else
                {
                    mopping = false;
                }
            }
            else
            {
                interactPrompt.SetActive(false);
                cleanupObj = null;
                mopping = false;
            }
        }
        SoundUpdate();
    }

    private void SoundUpdate()
    {
        if (mopping)
        {
            PLAYBACK_STATE playbackState;
            mopSFX.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                mopSFX.start();
            }
        }
        else
        {
            mopSFX.stop(STOP_MODE.ALLOWFADEOUT);
        }

    }
}
