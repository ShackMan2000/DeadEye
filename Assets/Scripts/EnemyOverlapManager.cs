using System.Collections.Generic;
using UnityEngine;

public class EnemyOverlapManager : MonoBehaviour
{
    public LayerMask raycastMask;      // Layer mask for the raycast
    public int numberOfRays = 5;       // Number of rays in the wider cast
    public float raySpacing = 0.2f;    // Spacing between each ray

    private void Update()
    {
        // Get the camera's position
        Vector3 cameraPosition = Camera.main.transform.position;

        // Calculate the starting point for the raycast
        Vector3 raycastStart = cameraPosition - (Camera.main.transform.right * (raySpacing * (numberOfRays - 1)) / 2f);

        bool foundFirstEnemy = false; // To track if the first enemy is found
        bool foundSecondEnemy = false; // To track if the second enemy is found

        List<PerspectiveShield> shieldsToDisable = new List<PerspectiveShield>();

        // Cast multiple rays within the wider cast
        for (int i = 0; i < numberOfRays; i++)
        {
            Vector3 rayOrigin = raycastStart + Camera.main.transform.right * (raySpacing * i);
            Ray ray = new Ray(rayOrigin, Camera.main.transform.forward);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastMask))
            {
                // Check if the hit object is on the EnemyLayer
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("SHIELD"))
                {
                    // Find the script component on the hit object
                    PerspectiveShield scriptComponent = hit.collider.GetComponent<PerspectiveShield>();

                    // If the script component exists and hasn't been triggered before
                    if (scriptComponent != null && !scriptComponent.isScaling)
                    {
                        if (!foundFirstEnemy)
                        {
                            foundFirstEnemy = true;
                            shieldsToDisable.Add(scriptComponent); // Add the shield to the list
                        }
                        else if (!foundSecondEnemy)
                        {
                            foundSecondEnemy = true;
                            shieldsToDisable.Add(scriptComponent); // Add the shield to the list
                            Debug.Log("Both enemies triggered!");
                            // Do not break here so both shields can be disabled
                        }
                    }
                }
            }
        }

        // Disable both shields in the shieldsToDisable list
        if (foundSecondEnemy)
        {
            foreach (PerspectiveShield shield in shieldsToDisable)
            {
                shield.ShieldDown();
            }
        }
    }
}
