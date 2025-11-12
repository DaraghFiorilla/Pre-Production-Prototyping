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
        bool? success = null;
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
                    }
                    if (success != false) // if cans are in the right triggers, check all cans are upright
                    {
                        for (int i = 0; i < activeCans.Count; i++)
                        {
                            if (activeCans[i].transform.rotation.eulerAngles.z >= 15 || activeCans[i].transform.rotation.eulerAngles.z <= -15)
                            {
                                Debug.Log(activeCans[i] + " is not upright");
                                success = false;
                            }
                        }

                        if (success != false) // if all cans are upright, check they're all touching correctly
                        {
                            int i = 0; // total number of can contacts, for 3 cans this should be 4
                            foreach (GameObject can in activeCans)
                            {
                                i += can.GetComponent<Can>().touchingObjs.Count;
                            }
                            if (i >= 4) { success = true; } // top can touching 2, bottom cans touching 1 (2+1+1 = 4)
                            else { success = false; }
                        }
                    }
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
                    }
                    if (success != false) // if cans are in the right triggers, check all cans are upright
                    {
                        for (int i = 0; i < activeCans.Count; i++)
                        {
                            if (activeCans[i].transform.rotation.eulerAngles.z >= 15 || activeCans[i].transform.rotation.eulerAngles.z <= -15)
                            {
                                Debug.Log(activeCans[i] + " is not upright");
                                success = false;
                            }
                        }

                        if (success != false) // if all cans are upright, check they're all touching correctly
                        {
                            int i = 0; // total number of can contacts, for 6 cans this should be 12
                            foreach (GameObject can in activeCans)
                            {
                                i += can.GetComponent<Can>().touchingObjs.Count;
                            }
                            if (i >= 12) { success = true; }// bottom corner cans touching 1, bottom middle and top middle touching 2, both middle row touching 3 (1+1+2+2+3+3 = 12)
                            else { success = false; }
                        }
                    }
                    break;
                }
            case 10:
                {
                    // check layer 1 has 4 cans, layer 2 has 3 cans, layer 3 has 2 cans and layer 4 has 1 can
                    Collider[] layer1Result = Physics.OverlapBox(layerTriggers[0].gameObject.transform.position, layerTriggers[0].transform.localScale / 2);
                    Collider[] layer2Result = Physics.OverlapBox(layerTriggers[1].gameObject.transform.position, layerTriggers[1].transform.localScale / 2);
                    Collider[] layer3Result = Physics.OverlapBox(layerTriggers[2].gameObject.transform.position, layerTriggers[2].transform.localScale / 2);
                    Collider[] layer4Result = Physics.OverlapBox(layerTriggers[3].gameObject.transform.position, layerTriggers[3].transform.localScale / 2);
                    if (layer1Result.Length != 4 || layer2Result.Length != 3 || layer3Result.Length != 2 || layer4Result.Length != 1)
                    {
                        success = false;
                    }

                    if (success != false) // if cans are in the right triggers, check all cans are upright
                    {
                        for (int i = 0; i < activeCans.Count; i++)
                        {
                            if (activeCans[i].transform.rotation.eulerAngles.z >= 15 || activeCans[i].transform.rotation.eulerAngles.z <= -15)
                            {
                                Debug.Log(activeCans[i] + " is not upright");
                                success = false;
                            }
                        }

                        if (success != false) // if all cans are upright, check they're all touching correctly
                        {
                            int i = 0; // total number of can contacts, for 10 cans this should be 23
                            foreach (GameObject can in activeCans)
                            {
                                i += can.GetComponent<Can>().touchingObjs.Count;
                            }
                            if (i >= 23) { success = true; } // drew it out but yea 23 trust me 
                            else { success = false; }
                        }
                    }
                    break;
                }
            // next iterations would be 15, 21, 28, 36
            // not implemented atm cause they didnt fit the camera scene and it might get excessive but can be added if needed
            default:
                {
                    Debug.LogError("Invalid number of max cans set in the can minigame! Womp womp");
                    break;
                }
        }


        if (success == true) { Debug.Log("Success!"); }
        else if (success == false) { Debug.Log("Not success! :("); }
        else { Debug.Log("Bool is still null wtf"); }
    }
}
