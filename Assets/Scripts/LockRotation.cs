
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        // Calculate the new rotation based on the initial rotation and the parent's Y rotation
        Quaternion newRotation = Quaternion.Euler(initialRotation.eulerAngles.x, transform.eulerAngles.y, initialRotation.eulerAngles.z);

        // Apply the new rotation to the child object
        transform.rotation = newRotation;
    }
}