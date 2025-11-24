using System;
using System.Collections;
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
    public bool nightmareState;

    [Header("Blinking Mech. Variables")]
    [SerializeField] private float maxBlinkingTimer;
    [Tooltip("How much the blinking timer will decrease upon a successful blink")][SerializeField] private float blinkingTimerIncrement;
    [SerializeField] private float timer;
    [Tooltip("How much the player's time to input will decrease upon a successful blink")][SerializeField] private float animMultIncrement;
    [SerializeField] private int timesBlinked;
    [SerializeField] private Animator[] eyelidAnimators = new Animator[2];
    [SerializeField] bool pauseBlink;
    [SerializeField] private int maxTimesBlinked;
    private MainManager mainManager;

    // vars used in the initiate blink function
    float startTime;
    bool blinkActive;

    private void Awake()
    {
        mainManager = GetComponent<MainManager>();
        switchNightmareState += ChangeTextures;
        switchNightmareState += EnableObjs;

        //Switch();
        timer = maxBlinkingTimer;
    }

    private void Update()
    {
        if (!nightmareState)
        {
            if (!pauseBlink)
            {
                if (!blinkActive && timer > 0) { timer -= Time.deltaTime; }
                if (timer <= 0 && !blinkActive)
                {
                    blinkActive = true;
                    InitiateBlink();
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
    }

    public void Switch()
    {
        if (nightmareState)
        {
            timer = maxBlinkingTimer;
            nightmareState = false;
            Debug.Log("Switching to regular state");
        }
        else 
        {
            nightmareState = true;
            Debug.Log("Switching to nightmare state");
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
        startTime = Time.time;
        blinkActive = true;
        eyelidAnimators[0].Play("TopLidClose");
        eyelidAnimators[1].Play("BottomLidClose");
        mainManager.canInteract = false;
        //Debug.Log("Blink started");
    }

    void Blink()
    {
        //Debug.Log("interact pressed");
        timesBlinked++;

        if (timesBlinked >= maxTimesBlinked)
        {
            EyesClosed();
        }
        else
        {
            timer = maxBlinkingTimer - blinkingTimerIncrement * timesBlinked;          
            eyelidAnimators[0].SetTrigger("forceOpen");
            eyelidAnimators[1].SetTrigger("forceOpen");
            eyelidAnimators[0].SetFloat("speedMult", 1 + animMultIncrement * timesBlinked);
            eyelidAnimators[1].SetFloat("speedMult", 1 + animMultIncrement * timesBlinked);
        }
        mainManager.canInteract = true;
        blinkActive = false;
    }

    public void EyesClosed()
    {
        blinkActive = false;
        timer = maxBlinkingTimer;
        //Debug.Log("interact not pressed");
        timesBlinked = 0;
        eyelidAnimators[0].SetTrigger("forceOpen");
        eyelidAnimators[1].SetTrigger("forceOpen");
        Switch();
    }

    public void PauseBlink()
    {
        if (pauseBlink)
        {
            Debug.Log("Unpausing blink");
            pauseBlink = false;
            foreach (Animator anim in eyelidAnimators)
            {
                anim.speed = 1;
                anim.GetComponent<Image>().enabled = true;
            }
        }
        else
        {
            Debug.Log("Pausing blink");
            pauseBlink = true;
            foreach (Animator anim in eyelidAnimators)
            {
                anim.speed = 0;
                anim.GetComponent<Image>().enabled = false;
            }
        }
    }
}