using UnityEngine;

public class StockBoxHandler : MonoBehaviour
{

    [SerializeField] private StockBoxes stockBoxesData;

    public string stockTypeLabel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stockTypeLabel = stockBoxesData.productType;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
