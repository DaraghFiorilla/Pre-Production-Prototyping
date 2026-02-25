using UnityEngine;
using UnityEngine.InputSystem;

public class PastrySpawner : MonoBehaviour
{
    public RectTransform pastryItem;
    public Canvas canvas;

    public void Spawn()
    {
        RectTransform spawnedItem = Instantiate(pastryItem, canvas.transform);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Mouse.current.position.ReadValue(),
            canvas.worldCamera,
            out localPoint
        );

        spawnedItem.localPosition = localPoint;
    }
}
