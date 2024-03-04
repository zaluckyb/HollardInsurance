using UnityEngine;

public class EnableObjectsOnTrigger : MonoBehaviour
{
    public GameObject[] objectsToEnable;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the XR Origin player
        if (other.CompareTag("XRPlayer"))
        {
            // Enable each GameObject in the array
            foreach (GameObject obj in objectsToEnable)
            {
                obj.SetActive(true);
            }
        }
    }
}
