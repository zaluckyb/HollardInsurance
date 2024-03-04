using UnityEngine;

public class MoveXRWithObject : MonoBehaviour
{
    public Transform xrOrigin; // Reference to your XR origin (e.g., camera rig)
    public Transform targetObject; // Reference to the object you want to follow

    private float initialYOffset; // Initial Y offset between the XR origin and the target object

    void Start()
    {
        if (xrOrigin == null || targetObject == null)
        {
            Debug.LogError("XR origin or target object not assigned!");
            enabled = false; // Disable the script if references are missing
            return;
        }

        // Calculate the initial Y offset between the XR origin and the target object
        initialYOffset = targetObject.position.y - xrOrigin.position.y;
    }

    void Update()
    {
        // Calculate the new Y position for the XR origin based on the target object's position
        float newYPosition = targetObject.position.y - initialYOffset;

        // Update the XR origin's position while preserving its original X and Z coordinates
        Vector3 newPosition = new Vector3(xrOrigin.position.x, newYPosition, xrOrigin.position.z);
        xrOrigin.position = newPosition;
    }
}
