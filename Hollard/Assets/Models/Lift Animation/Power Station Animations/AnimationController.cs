using UnityEngine;

public class EnableAnimatorOnCollision : MonoBehaviour
{
    public Animator animatorToEnable;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves XR controllers
        if (collision.collider.CompareTag("XRController"))
        {
            // Enable the Animator component
            animatorToEnable.enabled = true;
        }
    }
}
