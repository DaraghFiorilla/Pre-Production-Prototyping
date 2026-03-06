using UnityEngine;

public class TasklistAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetShowingBoolTrue()
    {
        Debug.Log("setshowing true");
        animator.ResetTrigger("toggle");
        animator.SetBool("showing", true);
    }

    public void SetShowingBoolFalse()
    {
        Debug.Log("setshowing false");
        animator.ResetTrigger("toggle");
        animator.SetBool("showing", false);
    }
}
