using UnityEngine;

public class StockCageScript : MonoBehaviour
{
    [SerializeField] public string correctProductType;

    [SerializeField] private int requiredAmount;
    [SerializeField] private int currentAmount;

    [SerializeField] public GameObject[] ArrayOfFillBoxes;

    [Header("Event Progress")]
    public int flagID;
    [SerializeField] private EventProgress eventManager;
    
    void Awake()
    {
        for (int i = 0; i < ArrayOfFillBoxes.Length; ++i)
        {
            ArrayOfFillBoxes[i].SetActive(false);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        StockBoxHandler stockBox = other.GetComponent<StockBoxHandler>();

        if (other.gameObject.GetComponent<StockBoxHandler>().stockTypeLabel == correctProductType)
        {
            int flagIndex = currentAmount;

            currentAmount++;

            Destroy(other.gameObject);

            eventManager.UpdateFlag(flagID + flagIndex);
        }

        for (int i = 0; i < ArrayOfFillBoxes.Length; i++)
        {
            ArrayOfFillBoxes[i].SetActive(i < currentAmount);
        }

        if (currentAmount == requiredAmount)
        {
            Debug.Log(correctProductType + " Stock Cage Full");

            this.gameObject.GetComponent<Collider>().isTrigger = false;
        }

        AudioManager.instance.PlayOneShot(FMODEvents.instance.stockDrop, this.transform.position);
    }
}

