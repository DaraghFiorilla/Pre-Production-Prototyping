using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    public Transform playerCam;

    private void Update()
    {
        //transform.forward = new Vector3(transform.position.x - playerCam.transform.position.x, 0, transform.position.z - playerCam.transform.position.z);
    }
}
