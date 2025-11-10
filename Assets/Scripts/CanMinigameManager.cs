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
    [SerializeField] private GameObject[] layerTriggers;
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
                // get cursor pos and convert to world pos
                Vector3 worldPos = myCam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10));
                placingCan.transform.position = new Vector3 (worldPos.x, 5, worldPos.z);
                RaycastShadow();
                // if mouse input left place at pos
                if (InputSystem.actions.FindAction("RightClick").WasPressedThisFrame() && !canFalling) { PlaceCan(); }
            }
        }
    }

    private void RaycastShadow()
    {
        RaycastHit hit;
        if (Physics.Raycast(placingCan.transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            Vector3 hitPoint = hit.point;
            placingCan.transform.GetChild(0).position = new Vector3(hitPoint.x, hitPoint.y + placingCan.transform.GetChild(0).localScale.y, hitPoint.z);
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
        Destroy(placingCan.transform.GetChild(0).gameObject);
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
        bool success = false;
        switch (maxCansNo)
        {
            case 3:
                {
                    // check layer 1 has 2 cans and layer 2 has 1 can
                    Collider[] layer1Result = Physics.OverlapBox(layerTriggers[0].gameObject.transform.position, layerTriggers[0].transform.localScale / 2);
                    Collider[] layer2Result = Physics.OverlapBox(layerTriggers[1].gameObject.transform.position, layerTriggers[1].transform.localScale / 2);
                    if (layer1Result.Length != 2 || layer2Result.Length != 1)
                    {
                        success = false;
                        break;
                    }
                    // check all cans are upright
                    for (int i = 0; i < activeCans.Count; i++)
                    {
                        if (activeCans[i].transform.rotation.z > 15 || activeCans[i].transform.rotation.z < -15)
                        {
                            Debug.Log(activeCans[i] + " is not upright");
                            success = false;
                            break;
                        }
                    }

                    success = true; // this should only be run if neither previous break statement is reached
                    break;
                }
            case 6:
                {
                    // check layer 1 has 3 cans, layer 2 has 2 cans and layer 3 has 1 can
                    Collider[] layer1Result = Physics.OverlapBox(layerTriggers[0].gameObject.transform.position, layerTriggers[0].transform.localScale / 2);
                    Collider[] layer2Result = Physics.OverlapBox(layerTriggers[1].gameObject.transform.position, layerTriggers[1].transform.localScale / 2);
                    Collider[] layer3Result = Physics.OverlapBox(layerTriggers[2].gameObject.transform.position, layerTriggers[2].transform.localScale / 2);
                    if (layer1Result.Length != 3 || layer2Result.Length != 2 || layer3Result.Length != 1)
                    {
                        success = false;
                        break;
                    }
                    // check all cans are upright
                    for (int i = 0; i < activeCans.Count; i++)
                    {
                        if (Mathf.Abs(activeCans[i].transform.rotation.x) >= 15 || Mathf.Abs(activeCans[i].transform.rotation.y) >= 15 || Mathf.Abs(activeCans[i].transform.rotation.z) >= 15)
                        {
                            success = false;
                            break;
                        }
                    }

                    success = true; // this should only be run if neither previous break statement is reached
                    break;
                }
            case 10:
                {
                    // check layer 1 has 4 cans, layer 2 has cans, layer 3 has 2 cans and layer 4 has 1 can
                    Collider[] layer1Result = Physics.OverlapBox(layerTriggers[0].gameObject.transform.position, layerTriggers[0].transform.localScale / 2);
                    Collider[] layer2Result = Physics.OverlapBox(layerTriggers[1].gameObject.transform.position, layerTriggers[1].transform.localScale / 2);
                    Collider[] layer3Result = Physics.OverlapBox(layerTriggers[2].gameObject.transform.position, layerTriggers[2].transform.localScale / 2);
                    Collider[] layer4Result = Physics.OverlapBox(layerTriggers[3].gameObject.transform.position, layerTriggers[3].transform.localScale / 2);
                    if (layer1Result.Length != 4 || layer2Result.Length != 3 || layer3Result.Length != 2 || layer4Result.Length != 1)
                    {
                        success = false;
                        break;
                    }
                    // check all cans are upright
                    for (int i = 0; i < activeCans.Count; i++)
                    {
                        if (Mathf.Abs(activeCans[i].transform.rotation.x) >= 15 || Mathf.Abs(activeCans[i].transform.rotation.y) >= 15 || Mathf.Abs(activeCans[i].transform.rotation.z) >= 15)
                        {
                            success = false;
                            break;
                        }
                    }

                    success = true; // this should only be run if neither previous break statement is reached
                    break;
                }
            default:
                {
                    Debug.LogError("Invalid number of max cans set in the can minigame! Womp womp");
                    break;
                }
        }


        if (success) { Debug.Log("Success!"); }
        else { Debug.Log("Not success! :("); }
    }
}
