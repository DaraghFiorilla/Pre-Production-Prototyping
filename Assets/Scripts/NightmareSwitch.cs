using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NightmareSwitch : MonoBehaviour
{
    [Header("Nightmare State Variables")]
    [SerializeField] private GameObject[] objectsToSwitch; // list of objects to initiate the dissolve
    [SerializeField] private GameObject[] objectsToEnable;

    public UnityAction switchNightmareState;

    [Tooltip("True = nightmare state, false = daytime state")]
    public bool nightmareState;
    public bool debug;

    [Header("Blinking Mech. Variables")]
    [SerializeField] private float maxBlinkingTimer;

    [Tooltip("How much the blinking timer will decrease upon a successful blink")]
    [SerializeField] private float timer = 15;

    [Tooltip("If paused, what to set the timer to on unpause")]
    [SerializeField] private float interruptedTimer;

    [Header("Blink Warning")]
    [SerializeField] private float blinkWarningDuration = 1f;

    private bool warningActive;
    private float warningTimer;

    [SerializeField] private Animator[] eyelidAnimators = new Animator[2];

    public bool pauseBlink;
    public bool scriptedBlink;

    private MainManager mainManager;
    [SerializeField] private GameObject blinkPrompt;
    [SerializeField] private LightControl lightControl;

    // vars used in the initiate blink function
    float startTime;
    bool blinkActive;

    //public BGMusicController music;

    public MuzakScript muzakScript;

    private void Start()
    {
        mainManager = GetComponent<MainManager>();
        switchNightmareState += ChangeTextures;
        switchNightmareState += EnableObjs;

        if (!debug) timer = 0f;
        else timer = 5;

        if (nightmareState)
        {
            ChangeTextures();
            EnableObjs();
            Switch();
        }
    }

    private void Update()
    {
        if (!nightmareState || debug)
        {
            if (!pauseBlink && !scriptedBlink)
            {
                if (!blinkActive && !warningActive)
                {
                    timer += Time.deltaTime;
                }

                // Start warning
                if (timer >= maxBlinkingTimer && !blinkActive && !warningActive)
                {
                    StartBlinkWarning();
                }

                // During warning phase
                if (warningActive)
                {
                    warningTimer -= Time.deltaTime;

                    // Player successfully prevents blink
                    if (InputSystem.actions.FindAction("Interact").WasPressedThisFrame())
                    {
                        CancelBlink();
                    }

                    // Player fails
                    if (warningTimer <= 0f)
                    {
                        warningActive = false;
                        InitiateBlink();
                    }
                }

                if (blinkActive)
                {
                    if (InputSystem.actions.FindAction("Interact").WasPressedThisFrame())
                    {
                        Blink();
                    }
                }
            }
        }
        
        if (nightmareState)
        {
            if (InputSystem.actions.FindAction("Interact").WasPressedThisFrame())
            {
                StartCoroutine(lightControl.FlickerLights());
            }
        }
    }

    public void Switch()
    {
        blinkActive = false;
        timer = maxBlinkingTimer;
        Debug.Log("Switch");

        if (nightmareState)
        {
            nightmareState = true;
            lightControl.TurnDay();
            RenderSettings.ambientIntensity = 1;
            RenderSettings.reflectionIntensity = 1;
            Debug.Log("Switching to regular state");
            //music.enterDay();
            muzakScript.MusicChange(false);
        }
        else 
        {
            lightControl.TurnNight();
            nightmareState = false;
            RenderSettings.ambientIntensity = 0.3f;
            RenderSettings.reflectionIntensity = 0.3f;
            Debug.Log("Switching to nightmare state");
            //music.enterNight();
            muzakScript.MusicChange(true);
        }
        switchNightmareState?.Invoke();
    }

    void ChangeTextures()
    {
        foreach (GameObject obj in objectsToSwitch)
        {
            if(obj.GetComponent<DissolveShader>() == null)
            {
                return;
            }
            obj.GetComponent<DissolveShader>().active = true;
        }
    }

    void EnableObjs()
    {
        Debug.Log("enableobj called");
        foreach (GameObject go in objectsToEnable)
        {
            if(go != null)
            {
                go.SetActive(true);
            }
        }
    }

    void InitiateBlink()
    {
        timer = 0f;

        blinkPrompt.SetActive(true);
        TextMeshProUGUI blinkPromptText = blinkPrompt.GetComponent<TextMeshProUGUI>();

        startTime = Time.time;
        blinkActive = true;

        eyelidAnimators[0].Play("TopLidClose");
        eyelidAnimators[1].Play("BottomLidClose");

        mainManager.canInteract = false;
        Debug.Log("Blink started");
    }

    void Blink()
    {
        blinkPrompt.SetActive(false);
        Debug.Log("interact pressed");

        eyelidAnimators[0].SetTrigger("forceOpen");
        eyelidAnimators[1].SetTrigger("forceOpen");

        mainManager.canInteract = true;
        blinkActive = false;

        timer = 0f;
    }

    public void EyesClosed()
    {
        blinkPrompt.SetActive(false);

        eyelidAnimators[0].SetTrigger("forceOpen");
        eyelidAnimators[1].SetTrigger("forceOpen");

        Debug.Log("EyesClosed");

        mainManager.canInteract = true;

        Switch();

        timer = 0f;
    }

    public void PauseBlink(bool paused)
    {
        pauseBlink = paused;

        if (!paused)
        {
            Debug.Log("Unpausing blink");

            if (timer < interruptedTimer)
            {
                timer = interruptedTimer;
            }
        }
        
        else
        {
            Debug.Log("Pausing blink");
        }

        foreach (Animator anim in eyelidAnimators)
        {
            anim.speed = paused ? 0 : 1;
            anim.GetComponent<Image>().enabled = !paused;
        }
    }

    void StartBlinkWarning()
    {
        warningActive = true;
        warningTimer = blinkWarningDuration;

        blinkPrompt.SetActive(true);
        
        Debug.Log("Blink warning");
    }

    void CancelBlink()
    {
        warningActive = false;

        blinkPrompt.SetActive(false);

        timer = 0f;

        Debug.Log("Blink prevented");
    }
}