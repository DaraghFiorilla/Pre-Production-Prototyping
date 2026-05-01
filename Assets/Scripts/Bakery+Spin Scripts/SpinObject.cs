using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float spinSpeed = 180f;

    public float life = 6;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);

        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime, Space.Self);
    }
}
