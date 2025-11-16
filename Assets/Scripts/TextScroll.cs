using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextScroll : MonoBehaviour
{
    [SerializeField][TextArea] private string[] displayText;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private TextMeshProUGUI textMesh;
    private int currentChar = 0;
    [SerializeField] private GameObject displayCanvas;
    private GameObject player;
    private bool playerInTrigger;
    private Camera mainCam;
    [SerializeField] private bool rotateThis;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (playerInTrigger)
        {
            
            if (rotateThis)
            {
                transform.LookAt(player.transform.position);
            }
            else { RotateCanvas(); }
        }
    }

    public void SetText(string text)
    {
        displayText[0] = text;
        UpdateText();
    }

    private void UpdateText()
    {
        //dialogueManager.canAdvance = false;
        //displayArrow.SetActive(false);
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
        //dialogueManager.canAdvance = true;
        //displayArrow.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
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

    private void RotateCanvas()
    {
        displayCanvas.transform.LookAt(mainCam.transform);
    }
}
