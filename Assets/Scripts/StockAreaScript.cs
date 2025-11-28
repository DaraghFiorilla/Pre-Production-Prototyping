using UnityEngine;

public class StockAreaScript : MonoBehaviour
{
    public string correctAreaProductType;
    public bool isStocked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isStocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GridBasedStockCage" && other.gameObject.GetComponent<StockCageScript>().correctProductType == correctAreaProductType)
        {

            isStocked = true;
        }
    }
}
