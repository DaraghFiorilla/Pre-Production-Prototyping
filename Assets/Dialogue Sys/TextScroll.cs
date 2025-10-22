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
    //[SerializeField] private GameObject displayArrow;

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
            displayCanvas.SetActive(true);
            UpdateText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            displayCanvas.SetActive(false);
        }
    }
}
