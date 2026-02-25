using UnityEngine;
using UnityEngine.UI;

public class PastryItem : MonoBehaviour
{
    public Pastry pastry;

    public int GetCost()
    {
        return pastry.cost;
    }

}
