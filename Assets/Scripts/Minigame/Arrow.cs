using UnityEngine;
using UnityEngine.InputSystem;
public class Arrow : MonoBehaviour
{
    public float speed;
    //public bool topOfList;
    public bool inTrigger;
    public int arrowType;
    public RhythmMinigame minigameManager;

    private void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        if (/*topOfList &&*/ inTrigger)
        {
            DetectInput();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RhythmInput") { inTrigger = true; }
        else if (collision.gameObject.tag == "DeleteArrow") { minigameManager.GetInputResult(0); minigameManager.DeleteArrow(gameObject, arrowType); }
    }

    int distanceResult;
    float absPos;

    private void DetectInput()
    {
        switch (arrowType)
        {
            case 0: // left
                {
                    if (InputSystem.actions.FindAction("LeftArrow").WasPressedThisFrame())
                    {
                        // detect how close to the centre the arrow is
                        // send this info to the game manager
                        absPos = Mathf.Abs(transform.localPosition.y);
                        Debug.Log("Distance = " + absPos);
                        if (absPos >= 150) { distanceResult = 0; } // this is a miss
                        else if (absPos >= 100) { distanceResult = 1; } // this is bad
                        else if (absPos >= 50) { distanceResult = 2; } // this is good
                        else { distanceResult = 3; } // this is perfect
                        minigameManager.GetInputResult(distanceResult);
                        minigameManager.DeleteArrow(gameObject, arrowType);
                    }
                    break;
                }
            case 1: // down
                {
                    if (InputSystem.actions.FindAction("DownArrow").WasPressedThisFrame())
                    {
                        absPos = Mathf.Abs(transform.localPosition.y);
                        Debug.Log("Distance = " + absPos);
                        if (absPos >= 150) { distanceResult = 0; } // this is a miss
                        else if (absPos >= 100) { distanceResult = 1; } // this is bad
                        else if (absPos >= 50) { distanceResult = 2; } // this is good
                        else { distanceResult = 3; } // this is perfect
                        minigameManager.GetInputResult(distanceResult);
                        minigameManager.DeleteArrow(gameObject, arrowType);
                    }
                    break;
                }
            case 2: // up
                {
                    if (InputSystem.actions.FindAction("UpArrow").WasPressedThisFrame())
                    {
                        absPos = Mathf.Abs(transform.localPosition.y);
                        Debug.Log("Distance = " + absPos);
                        if (absPos >= 150) { distanceResult = 0; } // this is a miss
                        else if (absPos >= 100) { distanceResult = 1; } // this is bad
                        else if (absPos >= 50) { distanceResult = 2; } // this is good
                        else { distanceResult = 3; } // this is perfect
                        minigameManager.GetInputResult(distanceResult);
                        minigameManager.DeleteArrow(gameObject, arrowType);
                    }
                    break;
                }
            case 3: // right
                {
                    if (InputSystem.actions.FindAction("RightArrow").WasPressedThisFrame())
                    {
                        absPos = Mathf.Abs(transform.localPosition.y);
                        Debug.Log("Distance = " + absPos);
                        if (absPos >= 150) { distanceResult = 0; } // this is a miss
                        else if (absPos >= 100) { distanceResult = 1; } // this is bad
                        else if (absPos >= 50) { distanceResult = 2; } // this is good
                        else { distanceResult = 3; } // this is perfect
                        minigameManager.GetInputResult(distanceResult);
                        minigameManager.DeleteArrow(gameObject, arrowType);
                    }    
                    break;
                }
            default:
                {
                    // PANIC
                    Debug.Log(gameObject + " is panicking due to incorrect arrow type int");
                    break;
                }
        }
    }
}
