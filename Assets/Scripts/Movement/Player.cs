using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] PlayerController playerController;

    private void OnMove(InputValue value)
    {
        playerController.moveInput = value.Get<Vector2>();
    }

    void OnLook(InputValue value)
    {
        playerController.lookInput = value.Get<Vector2>();
    }

    void OnSprint(InputValue value)
    {
        playerController.sprintInput = value.isPressed;
    }

    private void OnValidate()
    {
        if (playerController == null)
        {
           playerController =  GetComponent<PlayerController>();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
