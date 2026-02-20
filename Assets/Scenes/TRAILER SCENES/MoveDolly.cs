using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class MoveDolly : MonoBehaviour
{
    public float normalizedSpeed;
    public CinemachineSplineDolly dolly;
    public bool started = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dolly.CameraPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputSystem.actions.FindAction("Interact").WasPressedThisFrame()) {  started = true; }

        if (started)
        {
            if (dolly.CameraPosition < 1) dolly.CameraPosition += normalizedSpeed * Time.deltaTime;
        }
    }
}
