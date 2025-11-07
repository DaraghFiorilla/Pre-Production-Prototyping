using UnityEngine;

public class Can : MonoBehaviour
{
    [HideInInspector] public CanMinigameManager manager;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Mathf.Abs(rb.linearVelocity.y) < 0.001) { manager.CanStopped(); Destroy(this); }
    }
}
