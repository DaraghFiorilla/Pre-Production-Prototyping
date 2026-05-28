using UnityEngine;
using UnityEngine.InputSystem;

public class FlagUpdateFast : MonoBehaviour
{
    [Header("Event Progress")]
    [SerializeField] private EventProgress eventManager;
    [SerializeField] private int flagID = 0;

    [Header("Quest Settings")]
    [SerializeField] private int requiredPresses = 4;

    private int currentProgress = 0;

    private void Update()
    {
        if (InputSystem.actions.FindAction("DebugFlag").WasPressedThisFrame())
        {
            AddProgress();
        }
    }

    private void AddProgress()
    {
        if (currentProgress >= requiredPresses)
            return;

        currentProgress++;

        eventManager.UpdateFlag(flagID);

        Debug.Log($"Progress: {currentProgress}/{requiredPresses}");

        if (currentProgress >= requiredPresses)
        {
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        Debug.Log("Quest complete!");
        // optional: trigger next event, reward, etc.
    }
}
