using Unity.VisualScripting;
using UnityEngine;

public class ProgressTest : MonoBehaviour
{
    public EventProgress eventProgress;
    public int id;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        eventProgress.UpdateFlag(id);
        Destroy(gameObject);
    }
}
