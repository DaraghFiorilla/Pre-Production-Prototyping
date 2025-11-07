using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanMinigameManager : MonoBehaviour
{
    public bool minigameActive;
    public bool placingState;
    [SerializeField] private int maxCansNo;
    [SerializeField] private int currentCansNo;
    [SerializeField] private GameObject canPrefab;
    [SerializeField] List<GameObject> activeCans;
    private Camera myCam;
    [SerializeField] private GameObject placingCan;
    public bool maxCansPlaced;
    private bool canFalling;

    private void Awake()
    {
        StartMinigame();
    }

    private void Update()
    {
        if (minigameActive)
        {
            if (placingState)
            {
                Vector2 screenPos = Mouse.current.position.ReadValue();
                //Debug.Log("Screenpos = " + screenPos);
                // get cursor pos and convert to world pos
                //Debug.Log("Placing!!");
                Vector3 worldPos = myCam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10));
                Debug.Log("World pos = " + worldPos);
                placingCan.transform.position = new Vector3 (worldPos.x, 4, worldPos.z);
                // if mouse input left place at pos
                if (InputSystem.actions.FindAction("RightClick").WasPressedThisFrame()) { PlaceCan(); }
            }
        }
    }

    public void StartMinigame()
    {
        myCam = Camera.main;
        minigameActive = true;
        Cursor.visible = true;
    }

    public void CreateNewCan()
    {
        if (!placingState) // check we're not already placing another can
        {
            if (maxCansPlaced) { return; } // make sure we're not creating too many cans
            // instantiate new can
            // follow mouse pos converted to world pos (z axis locked)
            placingState = true;
            placingCan = Instantiate(canPrefab);
        }
    }

    private void PlaceCan()
    {
        GameObject addedCan = placingCan;
        Rigidbody rb = placingCan.GetComponent<Rigidbody>();
        activeCans.Add(addedCan);
        placingCan = null;
        placingState = false;
        currentCansNo++;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        if (currentCansNo >= maxCansNo) { maxCansPlaced = true; }

        CheckCanMovement(rb);
    }

    private void CheckCanMovement(Rigidbody canRb)
    {
        while (canRb.linearVelocity.y > 0)
        {
            canFalling = true;
        }
        
    }
}
