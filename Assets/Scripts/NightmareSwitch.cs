using System;
using UnityEngine;
using UnityEngine.Events;

public class NightmareSwitch : MonoBehaviour
{
    public GameObject[] objectsToSwitch; // list of objects to initiate the dissolve
    public GameObject[] objectsToEnable;
    public UnityAction switchNightmareState;
    public bool nightmareState;

    public void Awake()
    {
        switchNightmareState += ChangeTextures;
        switchNightmareState += EnableObjs;

        Switch();
    }

    public void Switch()
    {
        if (nightmareState) { nightmareState = false; }
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
}