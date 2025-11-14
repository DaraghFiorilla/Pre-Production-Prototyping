using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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
    [SerializeField] private float reactionTime;
    [Tooltip("How much the player's time to input will decrease upon a successful blink")][SerializeField] private float reactionTimeIncrement;
    [SerializeField] private bool coroutineActive;
    [SerializeField] private int timesBlinked;
    [SerializeField] private GameObject[] eyelidObjs = new GameObject[2];

    private void Awake()
    {
        switchNightmareState += ChangeTextures;
        switchNightmareState += EnableObjs;

        //Switch();
        timer = maxBlinkingTimer;
    }

    private void Update()
    {
        if (!nightmareState)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && !coroutineActive)
            {
                InitiateBlink();
            }
        }
    }

    public void Switch()
    {
        if (nightmareState)
        { 
            nightmareState = false;

        }
        else { nightmareState = true; }
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
        float startTime = Time.time;
        Vector3 topLidTarget = new Vector3(0, 270, 0);
        Vector3 bottomLidTarget = new Vector3(0, -270, 0);

        float blinkTimer = reactionTime - reactionTimeIncrement * timesBlinked;
        Debug.Log(blinkTimer);
        coroutineActive = true;
        // blink 
        while (blinkTimer > 0)
        {
            if (InputSystem.actions.FindAction("Interact").WasPressedThisFrame())
            {
                Debug.Log("interact pressed");
                timesBlinked++;
                reactionTime -= reactionTimeIncrement;
                timer = maxBlinkingTimer - blinkingTimerIncrement * timesBlinked;
                RetractEyelids();
                return;
            }
            Debug.Log("interact not pressed");
            blinkTimer -= Time.deltaTime;
            float fracComplete = (Time.time - startTime) / (reactionTime - reactionTimeIncrement * timesBlinked);
            eyelidObjs[0].transform.position = Vector3.Slerp(eyelidObjs[0].transform.position, topLidTarget, fracComplete);
            eyelidObjs[1].transform.position = Vector3.Slerp(eyelidObjs[1].transform.position, bottomLidTarget, fracComplete);

        }
        Switch();
        RetractEyelids();
    }

    void RetractEyelids()
    {
        float startTime = Time.time;
        float retractTime = 0.8f;
        Vector3 topLidTarget = new Vector3(0, 810, 0);
        Vector3 bottomLidTarget = new Vector3(0, -810, 0);

        while (eyelidObjs[1].transform.position.y < -810)
        {
            float fracComplete = (Time.time - startTime) / retractTime;
            eyelidObjs[0].transform.position = Vector3.Slerp(eyelidObjs[0].transform.position, topLidTarget, fracComplete);
            eyelidObjs[1].transform.position = Vector3.Slerp(eyelidObjs[1].transform.position, bottomLidTarget, fracComplete);
        }
    }
}