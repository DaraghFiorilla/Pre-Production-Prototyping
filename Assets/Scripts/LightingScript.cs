using UnityEngine;

public class LightingScript : MonoBehaviour
{

    public GameObject dayLight;
    public GameObject nightLight;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Day();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Day()
    {

        dayLight.SetActive(true);
        nightLight.SetActive(false);

    }

    public void Night()
    {

        dayLight.SetActive(false);
        nightLight.SetActive(true);

    }

}
