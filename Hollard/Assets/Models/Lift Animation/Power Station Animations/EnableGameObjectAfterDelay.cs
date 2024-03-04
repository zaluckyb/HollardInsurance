using UnityEngine;

public class EnableGameObjectAfterDelay : MonoBehaviour
{
    public GameObject objectToEnable;
    public float delayInSeconds = 2f;

    private void Start()
    {
        Invoke("EnableObject", delayInSeconds);
    }

    private void EnableObject()
    {
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }
        else
        {
            Debug.LogError("Object to enable is not assigned.");
        }
    }
}
