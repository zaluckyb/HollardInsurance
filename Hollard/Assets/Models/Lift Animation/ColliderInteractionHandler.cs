using UnityEngine;

public class ColliderInteraction : MonoBehaviour
{
    // Fields to be assigned through the Unity Inspector
    public Collider collider1;
    public Collider collider2;
    public Animator animator;
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    private void Update()
    {
        // Check if the colliders are intersecting
        if (collider1.bounds.Intersects(collider2.bounds))
        {
            // Enable the animator if it's not already enabled
            if (!animator.enabled)
            {
                animator.enabled = true;
            }

            // Play the first sound if it's not already playing
            if (!audioSource1.isPlaying)
            {
                audioSource1.Play();
            }

            // Play the second sound if it's not already playing
            if (!audioSource2.isPlaying)
            {
                audioSource2.Play();
            }
        }
    }
}
