using UnityEngine;

public class GridBasedStockCage : MonoBehaviour
{
    //public Transform movePoint;
    public float moveSpeed = 5f;
    public GameObject player;
    public float force = 3f;
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //movePoint.parent = null;

        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
      void Update()
    {
        
       // transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

    }

    public void PushStockCage()
    {
        //if(Vector3.Distance(transform.position, movePoint.position) <= 0.5f)
       // {
            Vector3 direction = player.transform.position - transform.position;

            direction.y = 0;

           direction.Normalize();

            Debug.Log("Stock cage should get pushed now");

           // movePoint.position = direction;

            rb.AddForceAtPosition(-direction * force, player.transform.position, ForceMode.Impulse);

            //transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

            //GetComponent<Rigidbody>().AddForce(direction * force);
        //}
    }
}
