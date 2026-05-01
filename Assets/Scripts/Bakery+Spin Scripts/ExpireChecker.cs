using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ExpireChecker : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    private int count = 0;
    private int maxCount = 10;

    [SerializeField] GameObject camEnable;

    [Header("Event Progress")]
    public int flagID;
    [SerializeField] private EventProgress eventManager;

    [SerializeField] private PlayerController playerController;

    void Awake()
    {
        UpdateText();
    }

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
                else if (clickedObject.CompareTag("Fresh"))
                {
                    Debug.Log("fresh");

                    if (count < maxCount)
                    {
                        count++;
                        UpdateText();
                    }
                }
            }
        }
    }

    void UpdateText()
    {
        counterText.text = count + " / " + maxCount;

        if(count == 10)
        {
            eventManager.UpdateFlag(flagID);
            camEnable.SetActive(true);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            playerController.canMove = true;

        }
    }
}