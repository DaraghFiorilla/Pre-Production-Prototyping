using TMPro;
using UnityEngine;

public class FreshCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    private int count = 0;
    private int maxScore = 10;

    void Awake()
    {
        UpdateText();
    }

    public void AddFresh()
    {
        if (count < maxScore)
        {
            count++;
            UpdateText();
        }
    }

    void UpdateText()
    {
        counterText.text = count + " / " + maxScore;
    }
}