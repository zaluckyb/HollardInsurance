using System.Collections;
using UnityEngine;

public class StartButtonActivate : MonoBehaviour
{
    // Fields to be assigned through the Unity Inspector
    public Collider collider1;
    public Collider collider2;
    public GameObject targetGameObject; // The GameObject to be enabled/disabled
    public AudioSource audioSource1;

    private void Update()
    {
        // Check if the colliders are intersecting
        if (collider1.bounds.Intersects(collider2.bounds))
        {
            // Disable and then re-enable the target GameObject after a delay
            StartCoroutine(ToggleGameObject());

            // Play the first sound if it's not already playing
            if (!audioSource1.isPlaying)
            {
                audioSource1.Play();
            }
        }
    }

    private IEnumerator ToggleGameObject()
    {
        // Check if the target GameObject is already enabled
        if (targetGameObject.activeSelf)
        {
            // Disable the GameObject
            targetGameObject.SetActive(false);
        }

        // Wait for a second
        yield return new WaitForSeconds(1f);

        // Re-enable the GameObject
        targetGameObject.SetActive(true);
    }
}