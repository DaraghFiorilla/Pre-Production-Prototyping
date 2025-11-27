using UnityEngine;



[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float maxSpeed => sprintInput ? sprintSpeed : walkSpeed;
    public float acceleration = 15f;

    [SerializeField] float walkSpeed = 3.5f;
    [SerializeField] float sprintSpeed = 7f;

    [Space(15)]
    [SerializeField] float jumpHeight = 2f;

    public Vector3 currentVelocity { get; private set; }
    public float currenSpeed { get; private set; }

    [Header("Physics Parameters")]
    [SerializeField] float gravityScale = 3f; 

    public Vector3 CurrentVelocity { get; private set; }
    public float CurrentSpeed {  get; private set; }
    
    public bool isGrounded => characterController.isGrounded;

    public float verticalVelocity = 0f;

    [Header("Looking Parameters")]
    public Vector2 lookSensitvity = new Vector2(0.1f, 0.1f);

    public float pitchLimit = 85f;

    [SerializeField] float currentPitch = 0f;

    public float CurrentPitch
    {
        get => currentPitch;

        set
        {
            currentPitch = Mathf.Clamp(value, -pitchLimit, pitchLimit);
        }
    }

    [Header("Input")]

    public Vector2 moveInput;
    public Vector2 lookInput;
    public bool sprintInput;
    public bool canMove;

    [Header("Components")]
    [SerializeField] private Camera camera;
    [SerializeField] CharacterController characterController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (characterController == null)
        {
            GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MoveUpdate();
            LookUpdate();
        }
    }

    public void TryJump()
    {
        if (isGrounded == false)
        {
            return;
        }

        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * gravityScale);
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

   

        verticalVelocity += Physics.gravity.y * gravityScale * Time.deltaTime;

        Vector3 fullVelocity = new Vector3(currentVelocity.x, verticalVelocity, currentVelocity.z);

        characterController.Move(fullVelocity * Time.deltaTime);

        currenSpeed = currentVelocity.magnitude;
    }

    void LookUpdate()
    {
        Vector2 input = new Vector2(lookInput.x * lookSensitvity.x, lookInput.y * lookSensitvity.y);

        CurrentPitch -= input.y;

        camera.transform.localRotation = Quaternion.Euler(CurrentPitch, 0f, 0f);

        transform.Rotate(Vector2.up * input.x);
    }
}
