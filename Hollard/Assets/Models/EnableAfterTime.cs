using UnityEngine;
using System.Collections;

public class EnableAfterTime : MonoBehaviour
{
    // Public variable to assign the target GameObject from the Inspector
    public GameObject targetGameObject;

    // Time in seconds after which the target GameObject will be enabled
    public float delay = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine
        StartCoroutine(EnableGameObjectAfterTime(delay));
    }

    // Coroutine that waits for a specified amount of time before enabling the target GameObject
    IEnumerator EnableGameObjectAfterTime(float time)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(time);

        // Check if the target GameObject is not null
        if(targetGameObject != null)
        {
            // Enable the target GameObject
            targetGameObject.SetActive(true);
        }
    }
}
