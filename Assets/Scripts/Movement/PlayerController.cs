using UnityEngine;
using FMOD.Studio;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float maxSpeed => sprintInput ? sprintSpeed : walkSpeed;
    public float acceleration = 15f;

    [SerializeField] float walkSpeed = 3.5f;
    [SerializeField] float sprintSpeed = 7f;


    public Vector3 currentVelocity { get; private set; }
    public float currenSpeed { get; private set; }

    [Header("Physics Parameters")]
    public Vector3 CurrentVelocity { get; private set; }
    public float CurrentSpeed {  get; private set; }

    [Header("Looking Parameters")]
    public Vector2 lookSensitvity = new Vector2(0.1f, 0.1f);

    public float pitchLimit = 85f;

    public float dialoguePitchLimit = 5f;

    float currentDialoguePitchLimit = 0f;

    [SerializeField] float currentPitch = 0f;

    public float CurrentPitch
    {
        get => currentPitch;

        set
        {
            currentPitch = Mathf.Clamp(value, -pitchLimit, pitchLimit);
        }
    }

    public float CurrentDialoguePitchLimit
    {
        get => currentDialoguePitchLimit;

        set
        {
            currentDialoguePitchLimit = Mathf.Clamp(value, -dialoguePitchLimit, dialoguePitchLimit);
        }
    }

    [Header("Input")]

    public Vector2 moveInput;
    public Vector2 lookInput;
    public bool sprintInput;
    public bool canMove;

    [Header("Components")]
    [SerializeField] private Camera myCamera;
    [SerializeField] CharacterController characterController;

    private EventInstance playerSteps;
    private EventInstance fastPlayerSteps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canMove = true;
        myCamera = GetComponentInChildren<Camera>();
        if (characterController == null)
        {
            GetComponent<CharacterController>();
        }

       playerSteps = AudioManager.instance.CreateInstance(FMODEvents.instance.playerSteps);
        fastPlayerSteps = AudioManager.instance.CreateInstance(FMODEvents.instance.fastPlayerSteps);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MoveUpdate();
            LookUpdate();
        }

        SoundUpdate();

    }

    void MoveUpdate()
    {
        Vector3 motion = transform.forward * moveInput.y + transform.right * moveInput.x;

        motion.y = 0f;

        motion.Normalize();

        

        if (motion.sqrMagnitude >= 0.01f) 
        { 
            currentVelocity = Vector3.MoveTowards(currentVelocity, motion * maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, acceleration * Time.deltaTime);
        }


        characterController.Move(currentVelocity * Time.deltaTime);

        currenSpeed = currentVelocity.magnitude;
    }

    void LookUpdate()
    {
        Vector2 input = new Vector2(lookInput.x * lookSensitvity.x, lookInput.y * lookSensitvity.y);

        CurrentPitch -= input.y;

        myCamera.transform.localRotation = Quaternion.Euler(CurrentPitch, 0f, 0f);

        transform.Rotate(Vector2.up * input.x);
    }

    public void SetDialoguePitchLimit()
    {
        Vector2 input = new Vector2(lookInput.x * lookSensitvity.x, lookInput.y * lookSensitvity.y);

        currentDialoguePitchLimit -= input.x;
    }

    public void DisableDialoguePitchLimit()
    {
        Vector2 input = new Vector2(lookInput.x * lookSensitvity.x, lookInput.y * lookSensitvity.y);
    }

    private void SoundUpdate()
    {
        if (currenSpeed != 0 && currenSpeed <= 4)
        {
            fastPlayerSteps.stop(STOP_MODE.ALLOWFADEOUT);
            PLAYBACK_STATE playbackState;
            playerSteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerSteps.start();
            }
        }
        else if(currenSpeed >4 )
        {
            playerSteps.stop(STOP_MODE.ALLOWFADEOUT);
            PLAYBACK_STATE playbackState;
            fastPlayerSteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                fastPlayerSteps.start();
            }
        }
        else
        {
            playerSteps.stop(STOP_MODE.ALLOWFADEOUT);
            fastPlayerSteps.stop(STOP_MODE.ALLOWFADEOUT);
        }

    }
}
