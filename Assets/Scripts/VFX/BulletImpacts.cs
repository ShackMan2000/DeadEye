using System;
using System.Collections.Generic;
using MinMaxSlider;
using UnityEngine;


public class BulletImpacts : MonoBehaviour
{

    
    // listen to shooter
    // allright, change of plans, just make sure that if something was hit that wasn't shootable, a bullet impact will be spawned. use th
    
    // get start and target positions
    // spawn and align the object in between if it has a minimum distance.
    // could also just show the trail somewhat closer to the actual bullet, make that a setting with variance.
    // bullet should move real fast real quick, try out settings.

 
    
    [SerializeField] GameObject bulletTrailPrefab;

    [SerializeField] float bulletTime;

    void OnEnable()
    {
        Shooter.OnShootBulletStartToTarget += SpawnBulletTrailToTarget;
        Shooter.OnShootBulletStartToInfiniteDirection += SpawnBulletTrailToInfiniteDirection;
    }
    
    void OnDisable()
    {
        Shooter.OnShootBulletStartToTarget -= SpawnBulletTrailToTarget;
        Shooter.OnShootBulletStartToInfiniteDirection -= SpawnBulletTrailToInfiniteDirection;
    }
    
    

    void SpawnBulletTrailToTarget(Vector3 arg1, Vector3 arg2)
    {
        throw new NotImplementedException();
    }
    
    
    void SpawnBulletTrailToInfiniteDirection(Vector3 arg1, Vector3 arg2)
    {
        
    }


    void SpawnBulletTrail(Vector3 start, Vector3 target)
    {
        
    }
}