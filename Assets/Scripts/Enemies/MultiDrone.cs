using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MultiDrone : MonoBehaviour
{
    // 

    // drone should be aligned perependicularly to the player

// account for laser being thicker than the raycast
    [SerializeField] ShotReceiver coreShotReceiver;

    [SerializeField] float laserDistance = 10f;

    [SerializeField] List<Transform> lasers;

    [SerializeField] List<SideDrone> sideDrones;

    // must have a side thing[s], that is only damageable and destructable by the laser

    
    // spawn new core...
    
    void OnEnable()
    {
        coreShotReceiver.OnDestroyedByCorrectWeapon += SpawnLaser;
    }

  

    void OnDisable()
    {
        coreShotReceiver.OnDestroyedByCorrectWeapon -= SpawnLaser;
    }

    
    [Button]
    void SpawnLaser(bool correctWeapon)
    {
        // raycast from the lasers to check if it hits one of the side things, if it does, spawn the laser


            int sideDronesHit = 0;
        foreach (Transform laser in lasers)
        {
            //raycast all 
            RaycastHit[] hits = Physics.RaycastAll(laser.position, laser.forward, laserDistance);
            
            
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out SideDrone sideDrone))
                {
                    sideDronesHit++;
                    sideDrone.GetHitByLaser();
                    
                }
            }

        }
            if (sideDronesHit > 0 && sideDronesHit != sideDrones.Count)
            {
                Debug.LogError("Side drones hit is not 0 or equal to side drones count. Probably be an error because laser shout hit all or none");
            }
    }

    // the core can be damaged by any weapon, when that happens simply spawn the laser (and maybe stop moving all together.
}