using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlcoholUIScript : MonoBehaviour
{
    public TextMeshProUGUI alcoholUIText;
    public GameObject mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        alcoholUIText.text = "I";
    }

    public void ChangeBeerText()
    {
        alcoholUIText.text = mainCamera.GetComponent<MoveStockCage>().alcoholDetailText.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
       // alcoholUIText.text = mainCamera.GetComponent<MoveStockCage>().alcoholDetailText.ToString();
    }
}
