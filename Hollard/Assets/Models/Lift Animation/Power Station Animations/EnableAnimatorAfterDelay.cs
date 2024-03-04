using UnityEngine;

public class EnableAnimatorAfterDelay : MonoBehaviour
{
    public Animator animator;
    public float delayInSeconds = 2f;

    private void Start()
    {
        Invoke("EnableAnimator", delayInSeconds);
    }

    private void EnableAnimator()
    {
        if (animator != null)
        {
            animator.enabled = true;
        }
        else
        {
            Debug.LogError("Animator component is not assigned.");
        }
    }
}
