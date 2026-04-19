using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCleanup : MonoBehaviour
{
    public float raycastDistance;
    private RaycastHit hit;
    [SerializeField] private GameObject interactPrompt;
    private CleanupObj cleanupObj;

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
                }
                else
                {
                    cleanupObj.StopSound();
                }
            }
            else
            {
                if (cleanupObj!= null)
                {
                    cleanupObj.StopSound();
                }
                interactPrompt.SetActive(false);
                cleanupObj = null;
            }
        }
    }
}
