using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

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
    [Header("Lists")]
    public List<GameObject> leftNoteList; public List<GameObject> downNoteList; public List<GameObject> upNoteList; public List<GameObject> rightNoteList;

    [Header("Game Variables")]
    [SerializeField] float timeBetweenArrows; // how much time to pass between spawning a new arrow
    [SerializeField] float arrowSpeed; // how fast the arrows move
    public int combo;

    [Header("Object References")]
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] private Transform[] arrowParents; // for some reason, this array breaks the inspector, apparently this is a known bug in this version of unity, i fixed it by going into preferences and setting the editor font to system font
    [SerializeField] private TextMeshProUGUI comboDisplay;
    [SerializeField] private TextMeshProUGUI[] textResultPrefabs;

    float timer = 0;

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
        int i = Random.Range(0, 4); // decide which type of arrow to spawn (0 = left, 1 = down, 2 = up, 3 = right)
        GameObject obj = Instantiate(arrowPrefab, arrowParents[i]); // spawn arrow
        obj.transform.localPosition = new Vector2(0, -1000); // set arrow pos depending on type
        Arrow arrowScript = obj.GetComponent<Arrow>();
        arrowScript.speed = arrowSpeed;
        arrowScript.minigameManager = this;
        arrowScript.arrowType = i;
        switch (i) // behaviours dependent on arrow type
        {
            case 0: // left
                {
                    leftNoteList.RemoveAll(GameObject => GameObject == null); // Remove all deleted arrow from the list before adding this one
                    obj.transform.rotation = Quaternion.Euler(0, 0, 270); // set rotation of arrow sprite; these variables assume the sprite is facing downwards by default
                    leftNoteList.Add(obj); // add this arrow to the list
                    TrimList(i); // this function just makes sure the list doesnt have extra elements and the objects are in order
                    break;
                }
            case 1: // down
                {
                    downNoteList.RemoveAll(GameObject => GameObject == null);
                    obj.transform.rotation = Quaternion.Euler(Vector3.zero);
                    downNoteList.Add(obj);
                    TrimList(i);
                    break;
                }
            case 2: // up
                {
                    upNoteList.RemoveAll(GameObject => GameObject == null);
                    obj.transform.rotation = Quaternion.Euler(0, 0, 180);
                    upNoteList.Add(obj);
                    break;
                }
            case 3: // right
                {
                    rightNoteList.RemoveAll(GameObject => GameObject == null);
                    obj.transform.rotation = Quaternion.Euler(0, 0, 90);
                    rightNoteList.Add(obj);
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

    public void GetInputResult(int result) // get the input result from the arrow input
    {
        switch (result)
        {
            case 0: // miss
                {
                    combo = 0;
                    break;
                }
            case 1: // bad
                {
                    combo++;
                    break;
                }
            case 2: // good
                {
                    combo++;
                    break;
                }
            case 3: // perfect
                {
                    combo++;
                    break;
                }
        }
    }

    public void TrimList(int listNo)
    {
        switch (listNo)
        {
            case 0: // left
                {
                    leftNoteList.TrimExcess();
                    leftNoteList[0].GetComponent<Arrow>().topOfList = true;
                    break;
                }
            case 1: // down
                {
                    downNoteList.TrimExcess();
                    downNoteList[0].GetComponent<Arrow>().topOfList = true;
                    break;
                }
            case 2: // up
                {
                    upNoteList.TrimExcess();
                    upNoteList[0].GetComponent<Arrow>().topOfList = true;
                    break;
                }
            case 3: // right
                {
                    rightNoteList.TrimExcess();
                    rightNoteList[0].GetComponent<Arrow>().topOfList = true;
                    break;
                }
            default:
                {
                    // PANIC
                    break;
                }
        }
    }
}
