using UnityEngine;

public class StockCageScript : MonoBehaviour
{
    //public int moveSpeed = 5;
    // public Transform movePoint;
    // public GameObject playerObject;
    // Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] public string correctProductType;
    [SerializeField] private int requiredAmount;
    [SerializeField] private int currentAmount;
    public string tagText;
    Rigidbody rb;
    //RigidbodyConstraints rbConstraints;
    [SerializeField] public GameObject[] ArrayOfFillBoxes;


    //public StockBoxes stockBoxType;
    void Awake()
    {
        //rbConstraints = RigidbodyConstraints.FreezePosition;
        tagText = ("GridBasedStockCage");
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        for (int i = 0; i < ArrayOfFillBoxes.Length; ++i)
        {
            ArrayOfFillBoxes[i].SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {

        


        
    }
    

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<StockBoxHandler>().stockTypeLabel == correctProductType)
        {
            currentAmount++;
            
            int howManyNeededToFill = currentAmount;
            for(int i = 0; i < ArrayOfFillBoxes.Length; i++)
            {
                ArrayOfFillBoxes[i].SetActive(i < howManyNeededToFill);
            }
            if (currentAmount == requiredAmount)
            {
                Debug.Log(correctProductType + " Stock Cage Full");
                this.gameObject.tag = tagText;
                this.gameObject.GetComponent<Collider>().isTrigger = false;
                //rbConstraints = RigidbodyConstraints.None;
                rb.useGravity = true;

            }
            Destroy(other.gameObject);

        }
        else
        {
            return;
        }
    } 
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<StockBoxHandler>().stockTypeLabel == correctProductType)
        {
            currentAmount++;
            Destroy(collision.gameObject);
            if (currentAmount == requiredAmount)
            {
                Debug.Log(correctProductType + " Stock Cage Full");
                this.gameObject.tag = tagText;
                this.gameObject.GetComponent<Collider>().isTrigger = false;
                rb.useGravity = true;

            }
        }
    }*/




}
