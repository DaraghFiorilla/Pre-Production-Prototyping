using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class NEWRhythm : MonoBehaviour
{
    [Header("Game variables")]
    [SerializeField] private float sliceBarSpeed;
    public bool minigameStarted;
    public int score;
    [SerializeField] private int totalInputs;
    [SerializeField] private bool paused;

    [Header("Object references")]
    [SerializeField] private GameObject sliceBar;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private Camera myCam;
    private MainManager mainManager;
    [SerializeField] private TextMeshProUGUI textResultDisplay;
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private TextMeshProUGUI finalText;
    [SerializeField] private GameObject playerObj;

    private Camera mainCam;
    private Vector3[] pathPoints = new Vector3[5];
    private int currentTarget;
    private GameObject activeInput;
    private string activeInputType;
    private bool pressed;

    private void Awake()
    {
        mainManager = GetComponent<MainManager>();
        currentTarget = 0;
        mainCam = Camera.main;

        //StartCoroutine(StartMinigame());
    }

    private void Update()
    {
        if (minigameStarted && !paused)
        {
            //Debug.Log("Started");
            //Debug.Log("Movetowards = " + sliceBar.transform.position + ", " + pathPoints[currentTarget] + ", " + sliceBarSpeed * Time.deltaTime);
            sliceBar.transform.position = Vector2.MoveTowards(sliceBar.transform.position, pathPoints[currentTarget], sliceBarSpeed * Time.deltaTime);

            if (activeInput != null)
            {
                Debug.Log("activeInput");
                if (InputSystem.actions.FindAction(activeInputType) != null) { Debug.Log("Action " + activeInputType + " exists"); }
                if (InputSystem.actions.FindAction(activeInputType).WasPressedThisFrame())
                {
                    Debug.Log("EEEEEE");
                    pressed = true;
                    CheckInputResult();
                }
            }

            if (sliceBar.transform.position == pathPoints[currentTarget])
            {
                Debug.Log("Met target");
                currentTarget++;
                if (currentTarget == 5)
                {
                    StartCoroutine(FinishMinigame());
                }
            }
        }
    }

    public void Pause(bool isPaused)
    {
        paused = isPaused;
    }

    private void CheckInputResult()
    {
        if (textResultDisplay.gameObject.activeSelf == false) { textResultDisplay.gameObject.SetActive(true); }
        if (activeInput == null)
        {
            //Debug.Log("Miss");
            textResultDisplay.text = "Miss!";
        }
        else
        {
            float xDif = Mathf.Abs(sliceBar.transform.position.x - activeInput.transform.position.x);
            //Debug.Log("xDif = Mathf.Abs(" + sliceBar.transform.position.x + " - " + activeInput.transform.position.x + ", result = " + xDif);
            if (xDif <= 5)
            {
                //Debug.Log("Perf!");
                textResultDisplay.text = "Perf!";
                score += 3;
            }
            else if (xDif <= 10)
            {
                //Debug.Log("Good!");
                textResultDisplay.text = "Good!";
                score += 2;
            }
            else
            {
                //Debug.Log("Bad!");
                textResultDisplay.text = "Bad!";
                score++;
            }
        }

        if (activeInput != null)
        {
            activeInput.SetActive(false);
            activeInput = null;
            activeInputType = "";
        }
    }

    public void SliceTriggerEntered(Collider2D other)
    {
        if (other.CompareTag("RhythmInput"))
        {
            activeInput = other.gameObject;
            activeInputType = activeInput.name;
        }
    }

    public void SliceTriggerExited(Collider2D other)
    {
        if (other.CompareTag("RhythmInput"))
        {
            activeInput = null;
            activeInputType = "";
            if (!pressed) { CheckInputResult(); }
            if (other.gameObject.activeSelf) { other.gameObject.SetActive(false); }
            pressed = false;
        }
    }

    public void TriggerStart()
    {
        StartCoroutine(StartMinigame());
    }

    public IEnumerator StartMinigame()
    {
        mainManager.nightmareManager.PauseBlink();
        mainManager.canPause = false;
        //mainManager.canInteract = false;
        mainCam.enabled = false;
        playerObj.GetComponent<PlayerController>().canMove = false;
        myCam.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(true);
        countdownText.gameObject.SetActive(true);

        for (int i = 0; i < 5; i++)
        {
            pathPoints[i] = sliceBar.transform.GetChild(i).position;
        }

        for (int i = 0; i < 3; i++)
        {
            countdownText.text = (3 - i).ToString();
            yield return new WaitForSeconds(0.7f);
        }

        countdownText.text = "Chop!";
        yield return new WaitForSeconds(0.5f);

        countdownText.gameObject.SetActive(false);
        minigameStarted = true;
        mainManager.canPause = true;
    }

    public IEnumerator FinishMinigame()
    {
        mainManager.canPause = false;
        minigameStarted = false;
        finalText.gameObject.SetActive(true);
        textResultDisplay.gameObject.SetActive(false);

        // check score
        if (score >= totalInputs * 3)
        {
            finalText.text = "Perfect!\nWell done!";
            // perfect you're awesome
        }
        else if (score >= totalInputs * 2)
        {
            finalText.text = "Good!\nNice job!";
            // good you're cool
        }
        else if (score >= totalInputs * 1.5)
        {
            finalText.text = "OK!\nIt'll do!";
            // ok you'll live
        }
        else
        {
            finalText.text = "FAIL!\nThat's awful!";
            // FAIL!! YYOUR DEAD MATE
        }

        yield return new WaitForSeconds(2f);
        Destroy(mainCanvas);
        playerObj.GetComponent<PlayerController>().canMove = true;
        mainManager.canPause = true;
        mainCam.enabled = true;
        //mainManager.canInteract = true;
        Destroy(myCam.gameObject);
        Destroy(this);
    }
}
