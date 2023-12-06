using System.Collections.Generic;
using UnityEngine;


public class GunShooting : MonoBehaviour
{

    [SerializeField] Shooter shooter;

    // need a float for increase heat per shot
    // decrease heat per second
    // overheating lock duration
    
    [SerializeField] WeaponType weaponType;

    [SerializeField] Transform bulletSpawnPoint;
    
    bool isPressed;
    float pressedThreshold = 0.9f;
    float releaseThreshold = 0.1f;
    
    void Update()
    {
        float triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        
        Debug.Log("trigger value: " + triggerValue);
        //float rightTriggerValue = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        if (triggerValue > pressedThreshold)
        { if (!isPressed)
            {
                Shoot();
            }
            isPressed = true;
        }
        else if (triggerValue < releaseThreshold)
        {
            isPressed = false;
        }
    }
    
    
    void Shoot()
    {
        shooter.ShootAndDetermineTarget(bulletSpawnPoint.position, bulletSpawnPoint.forward, weaponType);
    }

}