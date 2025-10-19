using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public bool topOfList;
    public bool inTrigger;
    public int arrowType;
    public RhythmMinigame minigameManager;

    private void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        if (topOfList && inTrigger)
        {
            DetectInput();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RhythmInput") { inTrigger = true; }
        else if (collision.gameObject.tag == "DeleteArrow") { minigameManager.DeleteArrow(gameObject, arrowType); }
    }

    private void DetectInput()
    {
        int distanceResult;
        switch (arrowType)
        {
            case 0: // left
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        // detect how close to the centre the arrow is
                        // send this info to the game manager
                        if (Mathf.Abs(transform.position.y) >= 150) { distanceResult = 0; } // this is a miss
                        else if (Mathf.Abs(transform.position.y) >= 100) { distanceResult = 1; } // this is bad
                        else if (Mathf.Abs(transform.position.y) >= 50) { distanceResult = 2; } // this is good
                        else { distanceResult = 3; } // this is perfect
                        minigameManager.GetInputResult(distanceResult);
                        minigameManager.DeleteArrow(gameObject, arrowType);
                    }
                    break;
                }
            case 1: // down
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        if (Mathf.Abs(transform.position.y) >= 150) { distanceResult = 0; } // this is a miss
                        else if (Mathf.Abs(transform.position.y) >= 100) { distanceResult = 1; } // this is bad
                        else if (Mathf.Abs(transform.position.y) >= 50) { distanceResult = 2; } // this is good
                        else { distanceResult = 3; } // this is perfect
                        minigameManager.GetInputResult(distanceResult);
                        minigameManager.DeleteArrow(gameObject, arrowType);
                    }
                    break;
                }
            case 2: // up
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        if (Mathf.Abs(transform.position.y) >= 150) { distanceResult = 0; } // this is a miss
                        else if (Mathf.Abs(transform.position.y) >= 100) { distanceResult = 1; } // this is bad
                        else if (Mathf.Abs(transform.position.y) >= 50) { distanceResult = 2; } // this is good
                        else { distanceResult = 3; } // this is perfect
                        minigameManager.GetInputResult(distanceResult);
                        minigameManager.DeleteArrow(gameObject, arrowType);
                    }
                    break;
                }
            case 3: // right
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (Mathf.Abs(transform.position.y) >= 150) { distanceResult = 0; } // this is a miss
                        else if (Mathf.Abs(transform.position.y) >= 100) { distanceResult = 1; } // this is bad
                        else if (Mathf.Abs(transform.position.y) >= 50) { distanceResult = 2; } // this is good
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
