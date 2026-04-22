using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using TMPro;

public class CanMinigameManager : MonoBehaviour
{
    [Header("Event Progress")]
    public int flagID;
    [SerializeField] private EventProgress eventManager;

    [Header("Changeable variables")]
    [SerializeField] private float finishTime;
    [SerializeField] private bool testing;
    [SerializeField] private bool rotated;

    [Header("State variables")]
    public bool minigameActive;
    public bool placingState;
    private int maxCansNo;
    [SerializeField] private int currentCansNo;
    [SerializeField] List<GameObject> activeCans;
    [SerializeField] private GameObject placingCan;
    [SerializeField] private GameObject lastPlacedCan;
    public bool maxCansPlaced;
    [SerializeField] private bool canFalling;
    [SerializeField] private bool undoUsed;
    [SerializeField] private bool paused;

    [Header("Object references")]
    [SerializeField] private GameObject canPrefab;
    [SerializeField] private Camera myCam;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject[] layerTriggers;
    [SerializeField] private CanLayout canLayout;
    [SerializeField] private TextMeshProUGUI cansRemainingText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Transform cansParent;
    [SerializeField] private MainManager mainManager;
    [SerializeField] private GameObject tableObj;
    [SerializeField] private GameObject canOverlay;
    [SerializeField] private GameObject[] objectsToEnable;
    [SerializeField] private GameObject undoButton;

    private void Awake()
    {
        if (testing) { StartMinigame(); }
    }

    private void Update()
    {
        if (minigameActive && !paused)
        {
            if (!Cursor.visible) { Cursor.visible = true; }
            if (Cursor.lockState == CursorLockMode.Locked) { Cursor.lockState = CursorLockMode.Confined; }
            if (placingState)
            {
                UpdateCanPos();
            }
        }
    }

    public void Pause(bool isPaused)
    {
        paused = isPaused;
    }

    Ray ray;
    RaycastHit hit;
    float xPos;

    private void UpdateCanPos()
    {
        ray = myCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.gameObject.name == "GetInputQuad")
            {
                // if this gameobject has been rotated, we will have to get the Z AXIS input of the raycast
                if (!rotated) { xPos = hit.point.x; }
                else { xPos = hit.point.z; }
            }
            Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
        }
        placingCan.transform.position = cansParent.transform.position;

        if (!rotated) { placingCan.transform.position = new Vector3(xPos, placingCan.transform.position.y, placingCan.transform.parent.position.z); }
        else { placingCan.transform.position = new Vector3(placingCan.transform.position.x, placingCan.transform.position.y, xPos); }

        placingCan.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        

        placingCan.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        RaycastShadow();
        // if mouse input left place at pos
        if (InputSystem.actions.FindAction("RightClick").WasPressedThisFrame() && !canFalling) { PlaceCan(); }
    }

    private void RaycastShadow()
    {
        RaycastHit hit;
        bool hitBool = Physics.BoxCast(placingCan.transform.position, transform.localScale * 0.5f, Vector3.down, out hit, Quaternion.identity, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore);
        if (hitBool) { placingCan.transform.GetChild(0).position = new Vector3(placingCan.transform.position.x, hit.point.y + gameObject.transform.localScale.y, placingCan.transform.position.z); } 
    }

    public void StartMinigame()
    {
        Debug.Log("starting minigame");
        mainManager.nightmareManager.PauseBlink();
        Debug.Log("Enabling objs");
        foreach (GameObject obj in objectsToEnable) { obj.SetActive(true); }
        maxCansNo = canLayout.cansNo;
        cansRemainingText.text = "x" + maxCansNo.ToString();
        if (!testing) { playerObj.SetActive(false); }
        
        minigameActive = true;
        Cursor.visible = true;
    }

    public void CreateNewCan()
    {
        if (!placingState && !canFalling && minigameActive) // check we're not already placing another can
        {
            if (maxCansPlaced) { return; } // make sure we're not creating too many cans
            // instantiate new can
            placingState = true;
            //if (canLayout.transform.GetChild(0).gameObject.activeSelf) { canLayout.transform.GetChild(0).gameObject.SetActive(false); }
            DisableDisplayCans(true);
            placingCan = Instantiate(canPrefab, cansParent.transform);
            placingCan.gameObject.name = placingCan.gameObject.name + activeCans.Count;
            placingCan.GetComponent<BoxCollider>().enabled = false;
            placingCan.GetComponent<Can>().manager = this;
            cansRemainingText.text = "x" + (maxCansNo - activeCans.Count - 1).ToString();
            canOverlay.SetActive(true);
            if (!rotated) { placingCan.transform.rotation = Quaternion.Euler(0, -90, 0); }
            else { placingCan.transform.rotation = Quaternion.Euler(0, 180, 0); }
            if (undoButton.activeSelf) { undoButton.SetActive(false); }
        }
    }

    private void PlaceCan()
    {
        canOverlay.SetActive(false);
        Destroy(placingCan.transform.GetChild(0).gameObject);
        placingCan.GetComponent<BoxCollider>().enabled = true;
        GameObject addedCan = placingCan;
        Rigidbody rb = placingCan.GetComponent<Rigidbody>();
        activeCans.Add(addedCan);
        lastPlacedCan = addedCan;
        placingCan = null;
        placingState = false;
        //canLayout.transform.GetChild(0).gameObject.SetActive(true);
        currentCansNo++;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
        canFalling = true;
        if (currentCansNo >= maxCansNo) { maxCansPlaced = true; }
    }

    public void CanStopped()
    {
        canFalling = false;
        if (maxCansPlaced) { CheckResult(); }
        else if (!undoUsed) { undoButton.SetActive(true); }
    }

    private void DisableDisplayCans(bool disable)
    {
        MeshRenderer[] objMeshes = canLayout.transform.GetChild(0).GetComponentsInChildren<MeshRenderer>();
        if (disable)
        {
            foreach (MeshRenderer objMesh in objMeshes)
            {
                objMesh.enabled = false;
            }
        }
        else
        {
            foreach (MeshRenderer objMesh in objMeshes)
            {
                objMesh.enabled = true;
            }
        }
    }

    private void CheckResult()
    {
        DisableDisplayCans(false);
        bool? success = null;

        int layer1Result = 0;
        int layer2Result = 0;
        int layer3Result = 0;
        int layer4Result = 0;
        int layer5Result = 0;

        foreach (GameObject obj in activeCans)
        {
            Can can = obj.GetComponent<Can>();
            if (can.touchedLayer != null)
            {
                if (can.touchedLayer == layerTriggers[0]) { layer1Result++; }
                else if (can.touchedLayer == layerTriggers[1]) { layer2Result++; }
                else if (can.touchedLayer == layerTriggers[2]) { layer3Result++; }
                else if (can.touchedLayer == layerTriggers[3]) { layer4Result++; }
                else { layer5Result++; }
            }
            else
            {
                success = false;
                break;
            }
        }

        // check we have the right amount of cans in each layer
        if (layer1Result != canLayout.layer1Cans.Length || layer2Result != canLayout.layer2Cans.Length || layer3Result != canLayout.layer3Cans.Length || layer4Result != canLayout.layer4Cans.Length || layer5Result != canLayout.layer5Cans.Length)
        {
            Debug.Log("Failed because not right amount of cans in each layer");
            success = false;
        }
        if (success != false) // if cans are in the right triggers, check all cans are upright
        {
                int i = 0; // used to track number of contacts
                int j = 0; // used to track how many cans are in the right place
                foreach (GameObject can in activeCans)
                {
                    Can canScript = can.GetComponent<Can>();
                    i += canScript.touchingCans.Count;
                    if (canScript.touchingDisplay.Count > 0) { j++; }
                }
                Debug.Log("i = " + i + "j = " + j);
                if (i < canLayout.cansTouchingNo || j < activeCans.Count)
                {
                    if (i < canLayout.cansTouchingNo) { Debug.Log("Failed because not touching right amount of cans"); }
                    else { Debug.Log("Failed because not all cans are touching display"); }
                    success = false;
                }
                else
                {
                    success = true;
                }
        }

        if (success == true)
        {
            resultText.text = "Success!";
            StartCoroutine(Finish());
        }
        else if (success == false) { resultText.text = "Failure!"; }
        else { Debug.Log("Bool is still null wtf"); }
    }

    public void Restart()
    {
        if (minigameActive)
        {
            // reset states
            placingState = false;
            maxCansPlaced = false;
            canFalling = false;
            undoUsed = false;

            // clear objects
            DisableDisplayCans(false);
            //canLayout.transform.GetChild(0).gameObject.SetActive(true);
            currentCansNo = 0;
            if (placingCan != null) { Destroy(placingCan); }
            placingCan = null;
            foreach (GameObject can in activeCans) { Destroy(can); }
            activeCans.Clear();
            resultText.text = "";
            cansRemainingText.text = "x" + maxCansNo.ToString();
        }
    }

    public void Undo()
    {
        undoUsed = true;
        activeCans.Remove(lastPlacedCan);
        Destroy(lastPlacedCan);
        currentCansNo--;
        cansRemainingText.text = "x" + (maxCansNo - currentCansNo).ToString();
        undoButton.SetActive(false);
    }

    public IEnumerator Finish()
    {
        Debug.Log("Finishing minigame");
        minigameActive = false;

        eventManager.UpdateFlag(flagID);

        foreach (GameObject can in activeCans)
        {
            Rigidbody rb = can.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        yield return new WaitForSeconds(finishTime);
        if (!testing)
        {
            mainManager.UpdateCanMinigameNo();
            playerObj.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameObject.name = "CanTable";
            foreach (GameObject can in activeCans) { Destroy(can.GetComponent<Can>()); }
            foreach (GameObject obj in objectsToEnable) { Destroy(obj); }
            mainManager.nightmareManager.PauseBlink();
            Destroy(this);
        }
        else
        {
            Debug.Log("Finished yippers");
        }
    }
}
