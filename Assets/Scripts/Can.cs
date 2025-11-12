using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour
{
    [HideInInspector] public CanMinigameManager manager;
    private Rigidbody rb;
    public List<GameObject> touchingObjs;
    private bool finished;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Can>() != null)
        {
            touchingObjs.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (touchingObjs.Contains(collision.gameObject))
        {
            touchingObjs.Remove(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Mathf.Abs(rb.linearVelocity.y) < 0.001 && Mathf.Abs(rb.angularVelocity.z) < 0.1 && !finished) { finished = true; manager.CanStopped(); }
    }
}
