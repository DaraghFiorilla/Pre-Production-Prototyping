using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    private PlayerInput playerInput;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerInput.actions["Interact"].performed += OnPlayerInteract;
    }

   /* private void OnInteract(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    } */

    private void OnDisable()
    {
        
    }

    private void OnPlayerInteract(InputAction.CallbackContext context)
    {
       // throw new NotImplementedException();
        Debug.Log("Interact");
    }

}
