using UnityEngine;
using System.Collections;

public class EnableDisableObjectsOnTrigger : MonoBehaviour
{
    public GameObject objectToEnable; // Object to enable
    public GameObject objectToDisable; // Object to disable
    public float enableDelay = 2f; // Delay time in seconds before enabling the object
    public float disableDelay = 5f; // Delay time in seconds before disabling the object

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the XR Origin player
        if (other.CompareTag("XRPlayer"))
        {
            // Start coroutine to enable the object after a delay
            StartCoroutine(EnableAfterDelay(enableDelay));

            // Start coroutine to disable the object after a different delay
            StartCoroutine(DisableAfterDelay(disableDelay));
        }
    }

    private IEnumerator EnableAfterDelay(float delay)
    {
        // Wait for the specified delay time before enabling
        yield return new WaitForSeconds(delay);
        objectToEnable.SetActive(true);
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        // Wait for the specified delay time before disabling
        yield return new WaitForSeconds(delay);
        objectToDisable.SetActive(false);
    }
}
