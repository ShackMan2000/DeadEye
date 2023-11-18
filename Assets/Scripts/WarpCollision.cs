using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class WarpCollision : MonoBehaviour
{
    // Define the UnityEvent to be triggered upon collision
    public UnityEvent OnCollisionEvent;
    public UnityEvent OnExitEvent;

    // Set the cooldown duration (in seconds)
    public float cooldownDuration = 2f;

    // Flag to check if we are in the cooldown period
    private bool inCooldown = false;
    private bool inPortalCooldown = false;

    // This method is automatically called when a collision occurs
     private void OnTriggerEnter(Collider other) {
        
   
    
        // Check if we are not in the cooldown period
        if (!inCooldown)
        {
           // Check if the colliding object has a certain tag (optional)
            if (other.gameObject.CompareTag("HEAD"))
            { 
                // Invoke the UnityEvent when the collision occurs
                OnCollisionEvent.Invoke();

                // Start the cooldown coroutine
                StartCoroutine(CooldownCoroutine());
            }
        }
    
     }
    // Coroutine for the cooldown period
    private IEnumerator CooldownCoroutine()
    {
        // Set the cooldown flag to true
        inCooldown = true;

        // Wait for the specified duration
        yield return new WaitForSeconds(cooldownDuration);

        // Reset the cooldown flag after the specified duration
        inCooldown = false;
    }

        

    // This method is automatically called when a collision occurs
     private void OnTriggerExit(Collider other) {
        
   
    
        // Check if we are not in the cooldown period
        if (!inPortalCooldown)
        {
           // Check if the colliding object has a certain tag (optional)
            if (other.gameObject.CompareTag("HEAD"))
            { 
                // Invoke the UnityEvent when the collision occurs
                OnExitEvent.Invoke();

                // Start the cooldown coroutine
                StartCoroutine(OtherPortalCoroutine());
            }
        }
    
     }
    // Coroutine for the cooldown period
    private IEnumerator OtherPortalCoroutine()
    {
        // Set the cooldown flag to true
        inPortalCooldown = true;

        // Wait for the specified duration
        yield return new WaitForSeconds(cooldownDuration);

        // Reset the cooldown flag after the specified duration
        inPortalCooldown = false;
    }


}




