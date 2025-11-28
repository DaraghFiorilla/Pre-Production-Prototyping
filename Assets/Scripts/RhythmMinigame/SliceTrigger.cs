using UnityEngine;

public class SliceTrigger : MonoBehaviour
{
    [SerializeField] private NEWRhythm minigameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        minigameManager.SliceTriggerEntered(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        minigameManager.SliceTriggerExited(collision);
    }
}
