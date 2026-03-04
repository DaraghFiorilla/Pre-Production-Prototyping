using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using TMPro;

public class EventProgress : MonoBehaviour
{
    [SerializeField] private GameObject tasklistParentObj;
    [SerializeField] private GameObject tasklistTaskPrefab;
    public GameObject[] event2Boxes; // TEMP VAR
    public List<GameEvent> eventQueue = new();
    public List<TasklistTask> activeTasks = new();

    private void Start()
    {
        eventQueue[0].startingEvent.Invoke();
        CreateNewTasks();
    }

    public void UpdateFlag(int id)
    {
        if (id >= eventQueue[0].endFlags.Length) Debug.LogError("ERROR id is outside range of flags array");
        if (eventQueue[0].endFlags[id].flag == false)
        {
            Debug.Log("Setting flag true");
            eventQueue[0].endFlags[id].flag = true;
            /*for(int i = 0; i < activeTasks.Count; i++)
            {
                if (activeTasks[i].flagID == id)
                {
                    if (activeTasks[i].taskCountTarget > 1)
                    {
                        activeTasks[i].taskCount++;
                        if (activeTasks[i].taskCount >= activeTasks[i].taskCountTarget)
                        {
                            activeTasks[i].complete = true;
                            activeTasks[i].transform.GetChild(1).gameObject.SetActive(true);
                        }
                    }

                    else
                    {
                        activeTasks[i].complete = true;
                        activeTasks[i].transform.GetChild(1).gameObject.SetActive(true);
                    }
                }
            }*/
            TasklistTask task = activeTasks[eventQueue[0].endFlags[id].tasklistID];
            if (task.taskCountTarget > 1)
            {
                task.taskCount++;
                task.GetComponent<TextMeshProUGUI>().text = task.displayText + "(" + task.taskCount + "/" + task.taskCountTarget + ")";
                if (task.taskCount >= task.taskCountTarget)
                {
                    task.complete = true;
                    task.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
            else
            {
                task.complete = true;
                task.transform.GetChild(1).gameObject.SetActive(true);
            }
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
        /*foreach (bool flag in eventQueue[0].endFlags[].flag)
        {
            if (!flag) failed = true;
        }*/

        for (int i = 0; i < eventQueue[0].endFlags.Length; i++)
        {
            if (eventQueue[0].endFlags[i].flag == false) failed = true;
        }

        if (!failed)
        {
            for (int i = 0; i < activeTasks.Count; i++)
            {
                if (!activeTasks[i].complete) Debug.LogError("WARNING: Active task " + activeTasks[i].name + " is marked as incomplete despite flags being met");
            }
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
        CreateNewTasks();
    }

    public void CreateNewTasks()
    {
        foreach (TasklistTask task in activeTasks) Destroy(task.gameObject);

        activeTasks.Clear();

        foreach (TaskConstructor task in eventQueue[0].addedTasks)
        {
            GameObject g = Instantiate(tasklistTaskPrefab, tasklistParentObj.transform);
            TasklistTask t = g.GetComponent<TasklistTask>();
            //t.flagID = task.flagID;
            t.taskCountTarget = task.targetCount;
            t.displayText = task.displayText;
            activeTasks.Add(t);

            if (t.taskCountTarget > 1) g.GetComponent<TextMeshProUGUI>().text = t.displayText + "(" + t.taskCount + "/" + t.taskCountTarget + ")";
            else g.GetComponent<TextMeshProUGUI>().text = t.displayText;
        }
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
        public EndFlag[] endFlags;
        public TaskConstructor[] addedTasks;
    }

    [Serializable] public struct EndFlag
    {
        public bool flag;
        //public bool tasklistTask;
        public int tasklistID;
        public string description;
    }

    [Serializable] public struct TaskConstructor
    {
        public int targetCount;
        //public int flagID;
        public string displayText;
    }

    /*[Serializable] public struct TasklistStruct
    {
        public int flagID;
        public string displayText;
        public int taskCountTarget;
        public int taskCount;
        public GameObject objRef;
        public bool complete;
    }*/
}
