using UnityEngine;

public class TurretAim : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 5f;

    // private void Awake() 
    // {
    //     player = Camera.main.transform;    
    // }
    // private void Update()
    // {
    //     // Check if the player exists
    //    /* if (player == null)
    //     {
    //         Debug.LogWarning("Player object is missing or not assigned.");
    //         return;
    //     }
    //   */
    //     // Calculate the direction to the player
    //     Vector3 direction = player.position - transform.position;
    //     direction.y = 0f; // Optional: Keep the turret rotation on the same plane
    //
    //     // Calculate the target rotation based on the direction
    //     Quaternion targetRotation = Quaternion.LookRotation(direction);
    //
    //     // Smoothly rotate the turret towards the player
    //     transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    // }
}