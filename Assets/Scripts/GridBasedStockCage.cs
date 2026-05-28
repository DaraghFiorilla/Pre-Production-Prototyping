using UnityEngine;

public class GridBasedStockCage : MonoBehaviour
{
    public GameObject player;

    public Vector3 playerPosition;


    public void PushStockCage()
    {
        Vector3 direction = player.transform.position - transform.position; //calculates direction between stock cage and player

        direction.y = 0; //ensures the player can't push or pull the cage on the y axis

        direction.Normalize();

        Debug.Log("Stock cage should get pushed now");

    }

    public void PullStockCage()
    {
        Vector3 direction = player.transform.position - transform.position;

        direction.y = 0; //ensures the player can't push or pull the cage on the y axis

        direction.Normalize();

        Debug.Log("Stock cage should get pulled now");
    }
}
