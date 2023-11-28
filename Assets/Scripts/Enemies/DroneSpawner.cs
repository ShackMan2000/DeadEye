using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class DroneSpawner : MonoBehaviour
{


    [ShowInInspector]
    Dictionary<DroneSettings, Vector3> spawnPositions;

    
    
    
    [SerializeField] MultiDrone launchDronePrefab;


    [Button]
    void SpawnLaunchDrone()
    {
        MultiDrone launchDrone = Instantiate(launchDronePrefab, transform);
        launchDrone.transform.position = transform.position;
        launchDrone.transform.rotation = transform.rotation;
    }



}