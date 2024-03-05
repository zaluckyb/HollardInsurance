using System.Collections;
using UnityEngine;

public class ColliderSwitchObjects : MonoBehaviour
{
    // Fields to be assigned through the Unity Inspector
    public Collider collider1;
    public Collider collider2;
    public GameObject gameObjectToDisable;
    public GameObject gameObjectToEnable;
    public float disableDelay = 1f; // Delay before disabling the GameObject
    public float enableDelay = 1f; // Delay before enabling the GameObject
    public AudioSource audioSource1;

    private void Update()
    {
        // Check if the colliders are intersecting
        if (collider1.bounds.Intersects(collider2.bounds))
        {
            // Start the coroutine to disable and enable GameObjects after the specified delays
            StartCoroutine(DisableEnableGameObjects());

            // Play the first sound if it's not already playing
            if (!audioSource1.isPlaying)
            {
                audioSource1.Play();
            }
        }
    }

    private IEnumerator DisableEnableGameObjects()
    {
        // Disable the specified GameObject after the disableDelay
        yield return new WaitForSeconds(disableDelay);
        gameObjectToDisable.SetActive(false);

        // Wait for the enableDelay before enabling the other GameObject
        yield return new WaitForSeconds(enableDelay);
        gameObjectToEnable.SetActive(true);
    }
}
