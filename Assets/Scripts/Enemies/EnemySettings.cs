using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[InlineEditor()]
[CreateAssetMenu]
public class EnemySettings : ScriptableObject
{
    
    
    public float MovementSpeed;
    public float RotationSpeed;
    
 
    public int minWaveLevel;
    // the age old question whether the SO should hold the prefab. 
    // kinda would prefer to just have a list of prefabs. They already hold the value
    // Ideally have 3 prefabs. They get injected the settings. Issue is kinda that I want some 
    // prefabs to use the same settings. Yes, definitely, want to have a red and blue prefab, but can just use same settings
    // for now that's all, let's not over engineer. 
    
    // red ball, blue ball, depth drone back and forth, depth drone rotate.
    // just have a setting for each one. Don't need to be different for now. Just have them move the same speed.
    // balls move around, depth drone lingers, let's just do that for now and change later.
    // so just prefabs, with an Enemy script. EnemySettings stores which wave.
    // crap, need to use multiple settings... might be back at using prefabs inside SO... nah, sucks to loop like that
    // Wavespawner has list of prefabs, yes, class with a prefab and all possible settings


    [InfoBox("Laser")] 
    public float LaserPivotRotation;
    public float LaserExpansionTime = 0.2f;
    public float LaserStayTime = 1f;
    public float LaserShrinkTime = 0.2f;
    
    
    
    [InfoBox("SideDrone")]
    
    public float BackAndForthSpeed = 1f;
    public float BackAndForthDistance = 1f;
    
    public float LaserKnockbackSpeed = 1f;
    public float LaserKnockbackTime = 1f;
    
    public Vector3 PlacementAxis = Vector3.right;
    public float PlacementDistance = 1f;
    
    public Vector3 RotationAxis = Vector3.up;
    public float SideDronesRotationSpeed = 1f;
    
    
    //only for testing the launch drone
    public float SideDroneMovementSpeed;
    public Vector3 SideDroneMovementAxisWorld = Vector3.back; 
    public float SideDronePlaceBehind = 1f;


}