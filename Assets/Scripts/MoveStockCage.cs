using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class MoveStockCage : MonoBehaviour
{

    public GameObject player;
    public Transform holdPos;
    public float throwForce = 500f; //force at which the object is thrown at
    public float pickUpRange = 5f; //how far the player can pickup the object from
    private float rotationSensitivity = 1f; //how fast/slow the object is rotated in relation to mouse movement
    private GameObject heldObj; //picked up object
    private Rigidbody heldObjRb; //rigidbody of object player picks up
    private bool canDrop = true; //this is needed so player doesn't throw/drop object when rotating the object though rotation is
                                 //broken at the moment
                                 
    private PlayerInput playerInput;
    public GameObject BeerUIText;
    public string alcoholDetailText;

    //public StockCageScript stockCageScript;
    // private int LayerNumber; //layer index


    private void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();

    }
    void Start()
    {
        //LayerNumber = LayerMask.NameToLayer("holdLayer"); 
        alcoholDetailText = "hi";

        //mouseLookScript = player.GetComponent<MouseLookScript>();
    }
    void Update()
    {
        if (InputSystem.actions.FindAction("Interact").WasPressedThisFrame())
        {
            Debug.Log("Should interact with object now");


            if (heldObj == null)
            {
                //perform raycast to check if player is looking at object within pickuprange
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    //make sure pickup tag is attached
                    if (hit.transform.gameObject.tag == "StockCage") // checks if player is trying to pick up stock
                    {
                        //passes in the object hit into the PickUpObject function
                        PickUpObject(hit.transform.gameObject);
                        //stockCageScript = hit.transform.GetComponent<StockCageScript>();
                        // stockCageScript.PushStockCage(); this is the other option for grid based pushing but it doesn't fully work
                    }

                    if (hit.transform.gameObject.tag == "GridBasedStockCage")
                    {
                        hit.transform.gameObject.GetComponent<GridBasedStockCage>().PushStockCage();
                    }

                    if (hit.transform.gameObject.tag == "CombinationWheel")
                    {
                        hit.transform.gameObject.GetComponent<Dial>().Rotate();
                    }

                    if (hit.transform.gameObject.tag == "AlcoholBottle")
                    {
                        Debug.Log("looking at beer");
                        string alcoholTypeText = hit.transform.gameObject.GetComponent<OffLicenseProductHandler>().alcoholType;
                        string flavourProfileText = hit.transform.gameObject.GetComponent<OffLicenseProductHandler>().flavourProfile;
                        string paleOrDarkText = hit.transform.gameObject.GetComponent<OffLicenseProductHandler>().paleOrDark;

                        Debug.Log(alcoholTypeText);
                        Debug.Log(flavourProfileText);
                        Debug.Log(paleOrDarkText);
                        alcoholDetailText =
                         "This is a "
                                 + hit.transform.gameObject.GetComponent<OffLicenseProductHandler>().paleOrDark
                                 + ", "
                                 + hit.transform.gameObject.GetComponent<OffLicenseProductHandler>().flavourProfile
                                 + " tasting "
                                 + hit.transform.gameObject.GetComponent<OffLicenseProductHandler>().alcoholType;

                       // PassOffBeerTextData();



                    }

                    if (hit.transform.gameObject.tag == "AlcoCustomer")
                    {
                        Debug.Log("looking at wine snob");

                        hit.transform.gameObject.GetComponent<AlcoCustomerDialogueManager>().EnableDialogueUI();
                       // string alcoholTypeText = hit.transform.gameObject.GetComponent<OfflicenseCustomerScript>().alcoholType;
                       // string flavourProfileText = hit.transform.gameObject.GetComponent<OfflicenseCustomerScript>().flavourProfile;
                       // string paleOrDarkText = hit.transform.gameObject.GetComponent<OfflicenseCustomerScript>().paleOrDark;

                       // Debug.Log(alcoholTypeText);
                       // Debug.Log(flavourProfileText);
                       // Debug.Log(paleOrDarkText);
                      /*  alcoholDetailText =
                         "Would like a "
                                 + hit.transform.gameObject.GetComponent<OfflicenseCustomerScript>().paleOrDark
                                 + ", "
                                 + hit.transform.gameObject.GetComponent<OfflicenseCustomerScript>().flavourProfile
                                 + " type of "
                                 + hit.transform.gameObject.GetComponent<OfflicenseCustomerScript>().alcoholType;

                        PassOffBeerTextData();*/

                    }
                }
                else
                {
                    if (canDrop == true)
                    {
                        StopClipping(); //prevents object from clipping through walls
                        DropObject();
                    }
                }

                if (heldObj != null) //if player is holding object
                {
                    MoveObject(); //keep object position at holdPos
                    RotateObject();
                    if (InputSystem.actions.FindAction("Attack").WasPressedThisFrame() && canDrop == true) //leftclick is used to throw however it
                    {                                                                                      // doesn't work right now
                        StopClipping();
                        ThrowObject();
                    }
                }
            }

            if (InputSystem.actions.FindAction("R").WasPressedThisFrame())
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    //make sure pickup tag is attached
                    if (hit.transform.gameObject.tag == "StockCage") // checks if player is trying to pick up stock
                    {
                        //passes in the object hit into the PickUpObject function
                        PickUpObject(hit.transform.gameObject);
                        //stockCageScript = hit.transform.GetComponent<StockCageScript>();
                        // stockCageScript.PushStockCage(); this is the other option for grid based pushing but it doesn't fully work
                    }

                    if (hit.transform.gameObject.tag == "GridBasedStockCage")
                    {
                        hit.transform.gameObject.GetComponent<GridBasedStockCage>().PullStockCage();
                    }
                }

            }


        }

    }

        public void PassOffBeerTextData()
        {
           // BeerUIText.GetComponent<AlcoholUIScript>().ChangeBeerText();

        }
        void PickUpObject(GameObject pickUpObj)
        {
            if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
            {
                heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
                heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
                heldObjRb.isKinematic = true;
                heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
                                                                // heldObj.layer = LayerNumber; //change the object layer to the holdLayer
                                                                //make sure object doesnt collide with player, it can cause weird bugs
                Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
            }
        }
        void DropObject()
        {
            //re-enable collision with player
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            heldObj.layer = 0; //object assigned back to default layer
            heldObjRb.isKinematic = false;
            heldObj.transform.parent = null; //unparent object
            heldObj = null; //undefine game object
        }
        void MoveObject()
        {
            //keep object position the same as the holdPosition position
            heldObj.transform.position = holdPos.transform.position;
        }
        void RotateObject()
        {
            if (InputSystem.actions.FindAction("RotateObject").WasPressedThisDynamicUpdate())//hold R key to rotate
            {
                canDrop = false; //make sure throwing can't occur during rotating

                //disable player being able to look around
                //mouseLookScript.verticalSensitivity = 0f;
                //mouseLookScript.lateralSensitivity = 0f;

                float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
                float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;
                //rotate the object depending on mouse X-Y Axis
                heldObj.transform.Rotate(Vector3.down, XaxisRotation);
                heldObj.transform.Rotate(Vector3.right, YaxisRotation);
            }
            else
            {
                //re-enable player being able to look around
                //mouseLookScript.verticalSensitivity = originalvalue;
                //mouseLookScript.lateralSensitivity = originalvalue;
                canDrop = true;
            }
        }
        void ThrowObject()
        {
            //same as drop function, but add force to object before undefining it
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            heldObj.layer = 0;
            heldObjRb.isKinematic = false;
            heldObj.transform.parent = null;
            heldObjRb.AddForce(transform.forward * throwForce);
            heldObj = null;
        }
        void StopClipping() //function only called when dropping/throwing
        {
            var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera
                                                                                              //have to use RaycastAll as object blocks raycast in center screen
                                                                                              //RaycastAll returns array of all colliders hit within the cliprange
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
            //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
            if (hits.Length > 1)
            {
                //change object position to camera position 
                heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
                                                                                              //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
            }
        }
    }

