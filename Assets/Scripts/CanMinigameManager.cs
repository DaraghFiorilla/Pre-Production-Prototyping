using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanMinigameManager : MonoBehaviour
{
    public bool minigameActive;
    public bool placingState;
    [Tooltip("This number should be 3, 6 or 10")][SerializeField] private int maxCansNo; // 3 for 2 layers, 6 for 3 layers, 10 for 4 layers, cant fit 5 layers but it would be 15
    [SerializeField] private int currentCansNo;
    [SerializeField] private GameObject canPrefab;
    [SerializeField] List<GameObject> activeCans;
    private Camera myCam;
    [SerializeField] private GameObject placingCan;
    public bool maxCansPlaced;
    [SerializeField] private bool canFalling;
    [SerializeField] private Collider[] layerTriggers;
    //[SerializeField] private float yLevel;

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
                //Debug.Log("World pos = " + worldPos);
                placingCan.transform.position = new Vector3 (worldPos.x, 5, worldPos.z);
                // if mouse input left place at pos
                if (InputSystem.actions.FindAction("RightClick").WasPressedThisFrame() && !canFalling) { PlaceCan(); }
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
        if (!placingState && !canFalling) // check we're not already placing another can
        {
            if (maxCansPlaced) { return; } // make sure we're not creating too many cans
            // instantiate new can
            // follow mouse pos converted to world pos (z axis locked)
            placingState = true;
            placingCan = Instantiate(canPrefab);
            placingCan.GetComponent<Can>().manager = this;
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
        canFalling = true;
        if (currentCansNo >= maxCansNo) { maxCansPlaced = true; }
    }

    public void CanStopped()
    {
        canFalling = false;
        if (maxCansPlaced) { CheckResult(); }
    }

    private void CheckResult()
    {
        switch (maxCansNo)
        {
            case 3:
                {
                    // check layer 1 has 2 cans and layer 2 has 1 can
                    // check all cans are upright
                    break;
                }
            case 6:
                {
                    // check layer 1 has 3 cans, layer 2 has 2 cans and layer 3 has 1 can
                    // check all cans are upright
                    break;
                }
            case 10:
                {
                    // check layer 1 has 4 cans, layer 2 has cans, layer 3 has 2 cans and layer 4 has 1 can
                    // check all cans are upright
                    break;
                }
            default:
                {
                    Debug.LogError("Invalid number of max cans set in the can minigame! Womp womp");
                    break;
                }
        }
    }
}
