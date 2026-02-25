using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            eventData.pointerDrag.GetComponent<DragHandler>().parentItem = transform;
        }
    }

    public PastryItem CurrentItem
    {
        get
        {
            if (transform.childCount == 0)
                return null;

            return transform.GetChild(0).GetComponent<PastryItem>();
        }
    }
}
