using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class DroneSpawner : MonoBehaviour
{


    [ShowInInspector]
    Dictionary<EnemySettings, Vector3> spawnPositions;

    
    
    
    [SerializeField] DepthDrone launchDronePrefab;


    [Button]
    void SpawnLaunchDrone()
    {
        DepthDrone launchDrone = Instantiate(launchDronePrefab, transform);
        launchDrone.transform.position = transform.position;
        launchDrone.transform.rotation = transform.rotation;
    }



}