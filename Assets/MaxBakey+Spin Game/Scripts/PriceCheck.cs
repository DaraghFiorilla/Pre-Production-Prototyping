using UnityEngine;

public class PriceCheck : MonoBehaviour
{
    public ItemSlot[] slots;
    public int targetPrice;

    public void Compare()
    {
        int totalCost = 0;

        foreach (ItemSlot slot in slots)
        {
            PastryItem item = slot.CurrentItem;
            totalCost += item.GetCost();
        }

        if (totalCost == targetPrice)
        {
            Debug.Log("RIGHT PRICE!!!");
        }
        else
        {
            EjectItem();
            Debug.Log("NO");
        }
    }

    void EjectItem()
    {
        foreach (ItemSlot slot in slots)
        {
            PastryItem item = slot.CurrentItem;
            
            item.transform.SetParent(null);
            Destroy(item.gameObject);
        }
    }
}
