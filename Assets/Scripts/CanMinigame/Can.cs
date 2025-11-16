using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour
{
    [HideInInspector] public CanMinigameManager manager;
    private Rigidbody rb;
    public List<GameObject> touchingCans;
    public List<GameObject> touchingDisplay;
    public GameObject touchedLayer;
    private bool finished;
    float maxWaitingTime = 3.5f;
    float timer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        timer = maxWaitingTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Can>() != null)
        {
            touchingCans.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (touchingCans.Contains(collision.gameObject))
        {
            touchingCans.Remove(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!finished) { timer -= Time.deltaTime; }
        if (Mathf.Abs(rb.linearVelocity.y) < 0.001 && Mathf.Abs(rb.angularVelocity.z) < 0.1 && !finished) { finished = true; manager.CanStopped(); }
        else if (timer <= 0) { finished = true; manager.CanStopped(); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("DisplayCylinder")) { touchingDisplay.Add(other.gameObject); }
        if (other.gameObject.name.Contains("Layer")) { touchedLayer = other.gameObject; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("DisplayCylinder") && touchingDisplay.Contains(other.gameObject)) { touchingDisplay.Remove(other.gameObject); }
    }
}
