using UnityEngine;

public class CanMinigameManager : MonoBehaviour
{
    public bool placingState;
    [SerializeField] private int maxCansNo;
    [SerializeField] private int currentCansNo;
    [SerializeField] private GameObject canPrefab;

    public void OnButtonClick()
    {
        if (!placingState)
        {
            // instantiate new can
            // follow mouse pos converted to world pos (z axis locked)

        }
    }
}
