using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float spinSpeed = 180f;

    public float life;

    void Start()
    {
        Destroy(gameObject, life);
    }

    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);

        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime, Space.Self);
    }
}
