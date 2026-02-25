using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class EventProgress : MonoBehaviour
{
    public GameObject[] event2Boxes; // TEMP VAR
    public List<GameEvent> eventQueue = new();

    private void Start()
    {
        eventQueue[0].startingEvent.Invoke();
    }

    public void UpdateFlag(int id)
    {
        if (id >= eventQueue[0].endFlags.Length) Debug.LogError("ERROR id is outside range of flags array");
        /*for (int i = 0; i < eventQueue[0].endFlags.Length; i++)
        {
            if (eventQueue[0].endFlags[i] == false)
            {
                eventQueue[0].endFlags[i] = true;
                break;
            }
        }*/
        if (eventQueue[0].endFlags[id] == false)
        {
            Debug.Log("Setting flag true");
            eventQueue[0].endFlags[id] = true;
        }
        else
        {
            Debug.LogError("ERROR flag is already set to true");
        }
        CheckEndConditions();
    }

    public void CheckEndConditions()
    {
        Debug.Log("Checking end conditions");
        bool failed = false;
        foreach (bool flag in eventQueue[0].endFlags)
        {
            if (!flag) failed = true;
        }

        if (!failed)
        {
            Debug.Log("End conditions met, moving to next event");
            QueueNextEvent();
        }
        else Debug.Log("End conditions not met");
    }

    public void QueueNextEvent()
    {
        eventQueue.Remove(eventQueue[0]);
        eventQueue.TrimExcess();
        eventQueue[0].startingEvent.Invoke();
    }

    public void BoxesEvent() // TEMP FUNCTION FOR TESTING - IN MAIN SCENE, THIS FUNCTION WILL BE IN A MINIGAME / OBJECT'S RESPECTIVE SCRIPT
    {
        foreach (GameObject box in event2Boxes) box.SetActive(true);
    }

    [Serializable] public struct GameEvent
    {
        public string eventName;
        public UnityEvent startingEvent;
        [TextArea(8, 20)][Tooltip("This is purely so we can keep track of which event covers what - no script uses this data")] public string eventDescription;
        public bool[] endFlags;
    }
}
