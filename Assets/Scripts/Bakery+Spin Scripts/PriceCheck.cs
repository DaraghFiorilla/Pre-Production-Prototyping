using UnityEngine;
using TMPro;

public class PriceCheck : MonoBehaviour
{
    public ItemSlot[] slots;

    [Header("Price")]
    public int[] targetPrice;
    private int price;

    public TextMeshProUGUI priceText;
    public GameObject canvasOff;

    [SerializeField] private PlayerController playerController;

    void Awake()
    {
        SetPrice();
    }

    void SetPrice()
    {
        int randomIndex = Random.Range(0, targetPrice.Length);
        price = targetPrice[randomIndex];

        UpdatePriceUI();
    }

    void UpdatePriceUI()
    {
        priceText.text = "€" + price.ToString();
    }

    public void Compare()
    {
        int totalCost = 0;

        foreach (ItemSlot slot in slots)
        {
            if (slot.CurrentItem != null)
            {
                totalCost += slot.CurrentItem.GetCost();
            }
        }

        if (totalCost == price)
        {
            Debug.Log("RIGHT PRICE!!!");
            canvasOff.SetActive(false);
            SetPrice();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            playerController.canMove = true;
        }
        else
        {
            Debug.Log("NO");
            EjectItem();
            SetPrice();

            FailSystem.Instance.ReportFailure();
        }
    }

    void EjectItem()
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.CurrentItem != null)
            {
                PastryItem item = slot.CurrentItem;
                item.transform.SetParent(null);
                Destroy(item.gameObject);
            }
        }
    }
}