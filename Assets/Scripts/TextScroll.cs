using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TextScroll : MonoBehaviour
{
    [SerializeField][TextArea] private string[] displayText;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private TextMeshProUGUI textMesh;
    private int currentChar = 0;
    [SerializeField] private GameObject displayCanvas;
    private bool playerInTrigger;
    private Camera mainCam;
    [SerializeField] private bool rotateThis;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (rotateThis)
        {
            Rotate();
        }
        else if (playerInTrigger)
        {

            Rotate();
        }
    }

    public void SetText(string text)
    {
        displayText[0] = text;
        UpdateText();
    }

    private void UpdateText()
    {
        textMesh.text = "";
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        for (int i = 0; i < displayText[currentChar].Length + 1; i++)
        {
            textMesh.text = displayText[currentChar].Substring(0, i);
            yield return new WaitForSeconds(scrollSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            displayCanvas.SetActive(true);
            playerInTrigger = true;
            UpdateText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            displayCanvas.SetActive(false);
        }
    }

    private void Rotate()
    {
        if (rotateThis)
        {
            transform.forward = new Vector3(transform.position.x - mainCam.transform.position.x, 0, transform.position.z - mainCam.transform.position.z);
        }
        else
        {
            displayCanvas.transform.forward = new Vector3(transform.position.x - mainCam.transform.position.x, 0, transform.position.z - mainCam.transform.position.z);
        }
    }
}
