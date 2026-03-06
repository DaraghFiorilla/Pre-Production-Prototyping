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
    [Tooltip("True = nightmare state, false = daytime state")]public bool nightmareState;
    public bool debug;

    [Header("Blinking Mech. Variables")]
    [SerializeField] private float maxBlinkingTimer;
    [Tooltip("How much the blinking timer will decrease upon a successful blink")][SerializeField] private float blinkingTimerIncrement;
    [SerializeField] private float timer;
    [Tooltip("How much the player's time to input will decrease upon a successful blink")][SerializeField] private float animMultIncrement;
    [Tooltip("If paused, what to set the timer to on unpause")][SerializeField] private float interruptedTimer;
    [SerializeField] private int timesBlinked;
    [SerializeField] private Animator[] eyelidAnimators = new Animator[2];
    public bool pauseBlink;
    public bool scriptedBlink;
    [SerializeField] private int maxTimesBlinked;
    private MainManager mainManager;
    //[SerializeField] private GameObject blinkPrompt;
    [SerializeField] private string currentInputButton;
    [SerializeField] private LightControl lightControl;

    // vars used in the initiate blink function
    float startTime;
    bool blinkActive;

    public BGMusicController music;

    private void Awake()
    {
        mainManager = GetComponent<MainManager>();
        switchNightmareState += ChangeTextures;
        switchNightmareState += EnableObjs;

        //Switch();
        if (!debug) timer = maxBlinkingTimer;
        else timer = 5;

        if (nightmareState)
        {
            ChangeTextures();
        }
    }

    private void Update()
    {
        if (!nightmareState || nightmareState && debug)
        {
            if (!pauseBlink && !scriptedBlink)
            {
                if (!blinkActive && timer > 0) { timer -= Time.deltaTime; }
                if (timer <= 0 && !blinkActive)
                {
                    blinkActive = true;
                    InitiateBlink();
                }
                if (blinkActive)
                {
                    if (InputSystem.actions.FindAction(currentInputButton).WasPressedThisFrame())
                    {
                        Blink();
                    }
                }
            }
        }
        if (nightmareState)
        {
            if (InputSystem.actions.FindAction("DownArrow").WasPressedThisFrame())
            {
                StartCoroutine(lightControl.FlickerLights());
            }
        }
    }

    public void Switch()
    {
        blinkActive = false;
        timer = maxBlinkingTimer;
        //Debug.Log("interact not pressed");
        timesBlinked = 0;
        if (nightmareState)
        {
            nightmareState = false;
            lightControl.TurnDay();
            RenderSettings.ambientIntensity = 1;
            RenderSettings.reflectionIntensity = 1;
            Debug.Log("Switching to regular state");
            music.enterDay();
        }
        else 
        {
            lightControl.TurnNight();
            nightmareState = true;
            RenderSettings.ambientIntensity = 0.3f;
            RenderSettings.reflectionIntensity = 0.3f;
            Debug.Log("Switching to nightmare state");
            music.enterNight();
        }
        switchNightmareState?.Invoke();
    }

    void ChangeTextures()
    {
        foreach (GameObject obj in objectsToSwitch)
        {
            obj.GetComponent<DissolveShader>().active = true;
        }
    }

    void EnableObjs()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            if (obj.activeSelf) { obj.SetActive(false); }
            else { obj.SetActive(true); }
        }
    }

    void InitiateBlink()
    {
        RandomiseInputButton();

        //blinkPrompt.SetActive(true);
        //TextMeshProUGUI blinkPromptText = blinkPrompt.GetComponent<TextMeshProUGUI>();
        //if (currentInputButton == "Interact") { blinkPromptText.text = "Press E to blink!!!"; }
        //else { blinkPromptText.text = "Press " + currentInputButton + " to blink!!"; }

        startTime = Time.time;
        blinkActive = true;
        eyelidAnimators[0].Play("TopLidClose");
        eyelidAnimators[1].Play("BottomLidClose");
        mainManager.canInteract = false;
        //Debug.Log("Blink started");
    }

    void Blink()
    {
        //blinkPrompt.SetActive(false);
        //Debug.Log("interact pressed");
        timesBlinked++;

        if (timesBlinked >= maxTimesBlinked)
        {
            Debug.Log("Times blinked is too high man wtf");
            EyesClosed();
        }
        else
        {
            timer = maxBlinkingTimer - blinkingTimerIncrement * timesBlinked;          
            eyelidAnimators[0].SetTrigger("forceOpen");
            eyelidAnimators[1].SetTrigger("forceOpen");
            eyelidAnimators[0].SetFloat("speedMult", 1 + animMultIncrement * timesBlinked);
            eyelidAnimators[1].SetFloat("speedMult", 1 + animMultIncrement * timesBlinked);
            Debug.Log("Speedmult = " + eyelidAnimators[0].GetFloat("speedMult") + ", " + "1 " + "+ " + animMultIncrement + " * " + timesBlinked);
        }
        mainManager.canInteract = true;
        blinkActive = false;
    }

    public void EyesClosed()
    {
        //blinkPrompt.SetActive(false);
        eyelidAnimators[0].SetTrigger("forceOpen");
        eyelidAnimators[1].SetTrigger("forceOpen");
        Debug.Log("EyesClosed");
        mainManager.canInteract = true;
        Switch();
    }

    public void PauseBlink()
    {
        if (pauseBlink)
        {
            Debug.Log("Unpausing blink");
            pauseBlink = false;
            if (timer < interruptedTimer) { timer = interruptedTimer; }
            foreach (Animator anim in eyelidAnimators)
            {
                /*if (anim.gameObject.name == "BottomLid")
                {
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("BottomLidClose"))
                    {

                    }
                }*/
                anim.speed = 1;
                anim.GetComponent<Image>().enabled = true;
            }
        }
        else
        {
            //if (blinkPrompt.activeSelf) { blinkPrompt.SetActive(false); }
            Debug.Log("Pausing blink");
            pauseBlink = true;
            foreach (Animator anim in eyelidAnimators)
            {
                anim.speed = 0;
                anim.GetComponent<Image>().enabled = false;
            }
        }
    }

    private void RandomiseInputButton()
    {
        int i = UnityEngine.Random.Range(0, 4);
        Debug.Log(i);
        switch (i)
        {
            case 0:
                currentInputButton = "Interact";
                break;

            case 1:
                currentInputButton = "Q";
                break;

            case 2:
                currentInputButton = "F";
                break;

            case 3:
                currentInputButton = "R";
                break;

            default:
                Debug.LogError("Getting a random input outside the bounds of range");
                break;
        }
    }
}