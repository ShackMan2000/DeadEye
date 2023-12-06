using System;
using UnityEngine;

/// <summary>
/// Responsible for taking Input, raycasting and informing all relevant systems
/// Idea is that it can take a mouse input for testing as well as a VR input
/// </summary>
public class Shooter : MonoBehaviour
{

    
    
    // public static event Action<Vector3, Vector3> OnShootBulletStartToTarget = delegate { };
    // public static event Action<Vector3, Vector3> OnShootBulletStartToInfiniteDirection = delegate { };

    
    public static event Action<WeaponType> OnShotFired = delegate { };
    
    public static event Action<Vector3, Vector3> OnHitObjectNotShootable = delegate { };
    public void ShootAndDetermineTarget(Vector3 startPosition, Vector3 direction, WeaponType weaponType)
    {
        // raycast from start position into direction, infinite, get colliders

        ShotReceiver shotReceiver = null;
        GameObject hitObject = null;

        if (Physics.Raycast(startPosition, direction, out RaycastHit hit, Mathf.Infinity))
        {
            hitObject = hit.collider.gameObject;
            // Try get is a little bit slower than geting a component that exists, but a lot faster than getting one that doesn't exist
            shotReceiver = hit.collider.TryGetComponent(out shotReceiver) ? shotReceiver : null;

            
            if(shotReceiver == null)
            {
                OnHitObjectNotShootable(hit.point, hit.normal);
            }
            else
            {
                shotReceiver.GetShot(weaponType);
            }
        }

        OnShotFired?.Invoke(weaponType);
        Debug.Log("Fired a shot with  " + weaponType + " and hit " + hitObject + " which has a shot receiver of " + shotReceiver);
    }


 
}