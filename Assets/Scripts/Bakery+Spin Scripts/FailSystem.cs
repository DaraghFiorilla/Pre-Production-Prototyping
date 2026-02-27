using UnityEngine;
using TMPro;

public class FailSystem : MonoBehaviour
{
    public static FailSystem Instance;

    [Header("Fail Limit")]

    public int maxFailures = 3;
    private int currentFailures = 0;

    [Header("Objects")]
    
    public GameObject failDialogue;
    public TextMeshProUGUI failureText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        FailText();
    }

    public void ReportFailure()
    {
        currentFailures++;

        FailText();

        if (currentFailures >= maxFailures)
        {
            FailTrigger();
        }
    }

    void FailText()
    {
        failureText.text = "Failed: " + currentFailures + "/3";
    }

    void FailTrigger()
    {
        failDialogue.SetActive(true);
    }
}

/*

ADD THIS LINE IN CODE FAIL PART OF A MINIGAME:

FailSystem.Instance.ReportFailure();

*/