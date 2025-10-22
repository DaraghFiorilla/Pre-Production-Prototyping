using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextScroll : MonoBehaviour
{
    [SerializeField][TextArea] private string[] displayText;
    [SerializeField] private float scrollSpeed;
    private TextMeshProUGUI textMesh;
    private int currentChar = 0;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    public void SetText(string text)
    {
        displayText[0] = text;
        UpdateText();
    }

    private void UpdateText()
    {
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
}
