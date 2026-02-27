using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCleanup : MonoBehaviour
{
    public float raycastDistance;
    private RaycastHit hit;
    //[SerializeField] private GameObject interactPrompt;
    private CleanupObj cleanupObj;
    //[SerializeField] private float forwardOffset;
    //private Vector3 raycastOrigin;

    private void Update()
    {
        //raycastOrigin = new Vector3(transform.position.x, transform.position.y, transform.position.z + forwardOffset);
        Vector3 forward = transform.TransformDirection(Vector3.forward) * raycastDistance;
        Debug.DrawRay(transform.position, forward, Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastDistance))
        {
            Debug.Log("Raycast hit " + hit.transform.gameObject);
            if (hit.transform.gameObject.CompareTag("Cleanup"))
            {
                //interactPrompt.SetActive(true);
                Debug.Log("Hit cleanup");
                cleanupObj = hit.transform.GetComponent<CleanupObj>();
                if (InputSystem.actions.FindAction("Interact").IsPressed())
                {
                    Debug.Log("Interact held, cleaning");
                    cleanupObj.Clean();
                }
            }
            else
            {
                //interactPrompt.SetActive(false);
                cleanupObj = null;
            }
        }
    }
}
