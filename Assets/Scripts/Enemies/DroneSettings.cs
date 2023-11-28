using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[InlineEditor()]
[CreateAssetMenu]
public class DroneSettings : ScriptableObject
{
    
    // maybe use inheritance if it becomes too much, e.g. one for the multidrone

    [InfoBox("Core")] 
    public float MovementSpeed;
    public Vector3 MovementAxisWorld = Vector3.forward; 
    
    
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
    public float RotationSpeed = 1f;
    
    
    //only for testing the launch drone
    public float SideDroneMovementSpeed;
    public Vector3 SideDroneMovementAxisWorld = Vector3.back; 
    public float SideDronePlaceBehind = 1f;


}