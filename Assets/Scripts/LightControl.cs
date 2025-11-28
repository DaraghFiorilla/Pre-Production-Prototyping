using UnityEngine;

public class LightControl : MonoBehaviour
{

    public LightingScript[] Lights;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //Lights = new LightingScript[30];

    }

    public void TurnDay()
    {

        for (int i = 0; i <= Lights.Length; i++)
        {

            Lights[i].Day();

        }

    }

    public void TurnNight()
    {

        for (int i = 0; i <= Lights.Length; i++)
        {

            Lights[i].Night();

        }

    }

}
