using UnityEngine;
using UnityEngine.Events;

public class Dial : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float animationDuration;
    private bool isRotating;
    [SerializeField] private int currentIndex;

    [Header("Events")]
    [SerializeField] private UnityEvent<Dial> onDialRotated;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentIndex = 0;
        isRotating = false;
        transform.localRotation = Quaternion.Euler(0, 0, currentIndex * -90);
    }

    public void Rotate()
    {
        if (isRotating)
            return;

        isRotating = true;

        currentIndex++;

        if (currentIndex >= 4) 
        
            currentIndex = 0;
        
        gameObject.transform.Rotate(0, 0, -90);
        Debug.Log(this.gameObject.name + this.currentIndex);
        RotationCompleteCallback();
        isRotating = false;
    }

    private void RotationCompleteCallback()
    {
        onDialRotated?.Invoke(this);
    }

    public int GetNumber()
    {
        Debug.Log(this.gameObject.name + " is returning an index of " + currentIndex);
        return currentIndex;
    }

    public void Lock()
    {
        isRotating = true;
    }

    public void Unlock()
    {
        isRotating = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
