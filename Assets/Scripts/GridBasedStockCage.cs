using UnityEngine;

public class GridBasedStockCage : MonoBehaviour
{
    //public Transform movePoint;
    public float moveSpeed = 5f;
    public GameObject player;
    public float force = 3f;
    Rigidbody rb;

    public Vector3 playerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //movePoint.parent = null;

        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
      void Update()
    {
        //playerPosition = player.transform.position;
       // transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

    }

    public void PushStockCage()
    {
            Vector3 direction = player.transform.position - transform.position; //calculates direction between stock cage and player

            direction.y = 0; //ensures the player can't push or pull the cage on the y axis

           direction.Normalize();

            Debug.Log("Stock cage should get pushed now");


            rb.AddForceAtPosition(-direction * force, player.transform.position, ForceMode.Impulse);

    }

    public void PullStockCage()
    {
        Vector3 direction = player.transform.position - transform.position;

        direction.y = 0; //ensures the player can't push or pull the cage on the y axis

        direction.Normalize();

        Debug.Log("Stock cage should get pulled now");


        rb.AddForceAtPosition(direction * force, player.transform.position, ForceMode.Impulse);
    }
}
