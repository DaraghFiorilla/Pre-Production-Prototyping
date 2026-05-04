using UnityEngine;

public class LockRaycaster : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageInput();
    }

    public void ManageInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Raycast();
        }
    }

    public void Raycast()
    {
        RaycastHit hit;

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 5);

        if(hit.collider ==null) 
            return;

        Dial dial = hit.collider.GetComponent<Dial>();

        if(dial == null)
        
            return;

        dial.Rotate();
    }
}
