using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Image image;
    CanvasGroup group;
    public Transform parentItem;

    private void Start()
    {
        image = GetComponent<Image>();
        group = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentItem = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        group.alpha = .5f;
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentItem);

        group.alpha = 1f;
        image.raycastTarget = true;
    }
}