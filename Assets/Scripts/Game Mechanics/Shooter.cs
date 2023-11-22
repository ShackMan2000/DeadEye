using System;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public void ShootBullet(Vector3 startPosition, Vector3 direction, WeaponType weaponType)
    {
        // raycast from start position into direction, infinite, get colliders
        
        ShotReceiver shotReceiver = null;
        GameObject hitObject = null;
        
       if(Physics.Raycast(startPosition, direction, out RaycastHit hit, Mathf.Infinity))
       {
           hitObject = hit.collider.gameObject;
           // Try get is a little bit slower than geting a component that exists, but a lot faster than getting one that doesn't exist
           shotReceiver = hit.collider.TryGetComponent(out shotReceiver) ? shotReceiver : null;
           
       }
        
       Debug.Log("Fired a shot with  " + weaponType + " and hit " + hitObject + " which has a shot receiver of " + shotReceiver);
        
    }

}