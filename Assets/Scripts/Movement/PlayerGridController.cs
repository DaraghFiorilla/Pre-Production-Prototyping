using System.Numerics;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;
public class PlayerGridController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Transform movePoint;
    public InputAction playerControls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movePoint.parent = null;

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        transform.position = UnityEngine.Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) <= .05 )
        {

            movePoint.position += playerControls.ReadValue<UnityEngine.Vector3>();
        }
       /* if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f )
        {
            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        }

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
        } */
        
    }
}
