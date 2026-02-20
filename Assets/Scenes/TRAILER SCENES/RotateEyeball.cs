using UnityEngine;

public class RotateEyeball : MonoBehaviour
{
    private Transform playerCam;

    void Awake()
    {
        playerCam = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);
    }

    void Update()
    {
        transform.LookAt(playerCam);
    }
}
