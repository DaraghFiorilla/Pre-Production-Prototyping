using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// TESTING VERSION OF THIS SCRIPT
// ARROWS ARE SET TO BE SPAWNED AT SET INTERVAL (TIMEBETWEENARROWS) USING A TIMER
// ONCE THE TIMER MEETS THE INTERVAL, IT SPAWNS A NEW ARROW THEN RESETS
// THE ARROW'S DIRECTION IS RANDOMLY DETERMINED USING RANDOM.RANGE - THIS DICTATES ITS SPAWNING POSITION AND ROTATION
// WHEN INSTANTIATED, THE ARROW IS ADDED TO A LIST DEPENDING ON ITS ORIENTATION - E.G., ALL LEFT ARROWS WILL BE ADDED TO LIST[0] ETC.
// ARROWS ARE INSTANTIATED OFF SCREEN AND CONTINUALLY MOVE UPWARDS DEPENDING ON THE ARROWSPEED VARIABLE
// ARROWS WILL CONTINUE TO MOVE UNTIL THEY DETECT RELEVANT INPUT WITHIN THE INPUT RANGE, OR EXIT THE INPUT AREA (MISSED) - THESE RANGES WILL PROBABLY BE MANAGED THROUGH RECTS WITH BOX COLLIDERS
// WHEN INSTANTIATED OR DELETED, AN ARROW'S LIST WILL BE REORDERED TO ENSURE THE CLOSEST ARROW TO INPUT IS ALWAYS THE FIRST OBJECT IN THE LIST

public class RhythmMinigame : MonoBehaviour
{
    public List<GameObject>[] noteLists = new List<GameObject>[4]; // ORDER: LEFT = 0, DOWN = 1, UP = 2, RIGHT = 3
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float timeBetweenArrows; // how much time to pass between spawning a new arrow
    [SerializeField] float arrowSpeed; // how fast the arrows move
    [SerializeField] private Vector2[] spawnPos; // for some reason, this array breaks the inspector, apparently this is a known bug in this version of unity, i fixed it by going into preferences and setting the editor font to system font
    [SerializeField] private Transform arrowParent;
    float timer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenArrows)
        {
            InstantiateArrow();
            timer = 0;
        }
    }

    void InstantiateArrow() // spawn new arrow
    {
        int i = Random.Range(0, 3); // decide which type of arrow to spawn (0 = left, 1 = down, 2 = up, 3 = right)
        GameObject obj = Instantiate(arrowPrefab, arrowParent); // spawn arrow
        obj.transform.position = spawnPos[i]; // set arrow pos depending on type
        Arrow arrowScript = obj.GetComponent<Arrow>();
        arrowScript.speed = arrowSpeed;
        arrowScript.minigameManager = this;
        switch (i) // behaviours dependent on arrow type
        {
            case 0: // left
                {
                    obj.transform.rotation = Quaternion.Euler(0, 0, 270); // set rotation of arrow sprite; these variables assume the sprite is facing downwards by default
                    noteLists[i].Add(obj);
                    TrimList(i);
                    break;
                }
            case 1: // down
                {
                    obj.transform.rotation = Quaternion.Euler(Vector3.zero);
                    TrimList(i);
                    break;
                }
            case 2: // up
                {
                    obj.transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
                }
            case 3: // right
                {
                    obj.transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                }
            default:
                {
                    // PANIC
                    Debug.Log(this + "says: Please panic now");
                    break;
                }
        }
    }

    public void DeleteArrow(GameObject deleteObj, int listNo)
    {
        Destroy(deleteObj);
        TrimList(listNo);
    }

    public void TrimList(int listNo)
    {
        noteLists[listNo].TrimExcess();
        noteLists[listNo][0].GetComponent<Arrow>().topOfList = true;
    }
}
