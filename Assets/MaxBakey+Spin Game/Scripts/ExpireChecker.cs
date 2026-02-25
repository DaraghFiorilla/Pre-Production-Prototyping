using UnityEngine;
using UnityEngine.InputSystem;

public class ExpireChecker : MonoBehaviour
{
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject.CompareTag("Expired"))
                {
                    Debug.Log("expired");
                }
                else if (clickedObject.CompareTag("Discount"))
                {
                    Debug.Log("discounted");
                }
                else if (clickedObject.CompareTag("Fresh"))
                {
                    Debug.Log("fresh");
                }
            }
        }
    }
}