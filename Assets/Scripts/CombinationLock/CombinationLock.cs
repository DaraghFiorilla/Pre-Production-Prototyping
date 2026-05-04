using UnityEngine;
using UnityEngine.Events;

public class CombinationLock : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Dial[] dials;

    [Header("Settings")]
    [SerializeField] private string combinationCode;

    [Header("Events")]
    [SerializeField] private UnityEvent onCorrectCombinationFound;

    public void CheckCombination(Dial dial)
    {
        for (int i = 0; i < dials.Length; i++)
        {
            int combinationNumber = int.Parse(combinationCode[i].ToString());

            if (combinationNumber != dials[i].GetNumber())
            {
                dial.Unlock();
                return;
            }
        }

        CorrectCombination();
    }

    private void CorrectCombination()
    {
        for(int i = 0;i < dials.Length;i++)
      
            dials[i].Lock();
       
        onCorrectCombinationFound?.Invoke();
    }

}
