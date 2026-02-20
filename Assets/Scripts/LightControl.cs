using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public LightingScript[] Lights;
    public NightmareSwitch nightmareManager; // set this in inspector

    void Start()
    {

    }

    public void TurnDay()
    {
        for (int i = 0; i < Lights.Length; i++)
        {
            Lights[i].Day();
        }
    }

    public void TurnNight()
    {
        for (int i = 0; i < Lights.Length; i++)
        {
            Lights[i].Night();
        }
    }

    public IEnumerator FlickerLights()
    {
        foreach (LightingScript light in Lights)
        {
            light.nightLight.SetActive(false);
        }
        yield return new WaitForSeconds(1f);
        foreach (LightingScript light in Lights)
        {
            light.nightLight.SetActive(true);
        }
        yield return new WaitForSeconds(.3f);
        foreach (LightingScript light in Lights)
        {
            light.nightLight.SetActive(false);
        }
        yield return new WaitForSeconds(.15f);
        foreach (LightingScript light in Lights)
        {
            light.nightLight.SetActive(true);
        }
        yield return new WaitForSeconds(.15f);
        foreach (LightingScript light in Lights)
        {
            light.nightLight.SetActive(false);
        }
        yield return new WaitForSeconds(.1f);
        foreach (LightingScript light in Lights)
        {
            light.nightLight.SetActive(true);
        }
        yield return new WaitForSeconds(.075f);
        foreach (LightingScript light in Lights)
        {
            light.nightLight.SetActive(false);
        }
        yield return new WaitForSeconds(.075f);
        foreach (LightingScript light in Lights)
        {
            light.nightLight.SetActive(true);
        }
        yield return new WaitForSeconds(.075f);
        foreach (LightingScript light in Lights)
        {
            light.nightLight.SetActive(false);
        }
        yield return new WaitForSeconds(.06f);
        foreach (LightingScript light in Lights)
        {
            light.nightLight.SetActive(true);
        }
    }
}
