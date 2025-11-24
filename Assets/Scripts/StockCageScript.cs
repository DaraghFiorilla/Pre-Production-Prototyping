using UnityEngine;

public class StockCageScript : MonoBehaviour
{
    //public int moveSpeed = 5;
    // public Transform movePoint;
    // public GameObject playerObject;
    // Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private string correctProductType;
    [SerializeField] private int requiredAmount;
    [SerializeField] private int currentAmount;
    public string tagText;
    Rigidbody rb;

    //public StockBoxes stockBoxType;
    void Start()
    {

        tagText = ("StockCage");


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
            Destroy(other.gameObject);
            if (currentAmount == requiredAmount)
            {
                Debug.Log(correctProductType + " Stock Cage Full");
                this.gameObject.tag = tagText;
                this.gameObject.GetComponent<Collider>().isTrigger = false;
                
            }
        }
    }




}
