using UnityEngine;
using UnityEngine.Events;

public class TandemTrigger : MonoBehaviour
{
  public Collider[] targetColliders; // The collider to activate

    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger that entered the collider is the one you want to activate the target collider
        if (other.CompareTag("Tandem"))
        {
             // Activate all target colliders in the array
            foreach (Collider collider in targetColliders)
            {
                collider.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the trigger that exited the collider is the one you want to deactivate the target colliders
        if (other.CompareTag("Tandem"))
        {
            // Deactivate all target colliders in the array
            foreach (Collider collider in targetColliders)
            {
                collider.enabled = false;
            }
        }
    }
}