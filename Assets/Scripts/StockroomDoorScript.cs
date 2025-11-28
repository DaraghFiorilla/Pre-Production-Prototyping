using UnityEngine;

public class StockroomDoorScript : MonoBehaviour
{
    public GameObject stockArea1;
    public GameObject stockArea2;
    public GameObject stockArea3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(stockArea1.gameObject.GetComponent<StockAreaScript>().isStocked == true && stockArea2.gameObject.GetComponent<StockAreaScript>().isStocked == true && stockArea3.gameObject.GetComponent<StockAreaScript>().isStocked == true)
        {
            Destroy(gameObject);
        }
    }
}
