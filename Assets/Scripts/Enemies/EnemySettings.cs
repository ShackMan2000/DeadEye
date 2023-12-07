using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[InlineEditor()]
[CreateAssetMenu]
public class EnemySettings : ScriptableObject
{
    
    public float MovementSpeed;
    public float RotationSpeed;
    
    public float pointsForKill = 10f;
 
    public int minWaveLevel;
   
    public EnemyMovementType  MovementType;
    public bool RotateTowardsPlayer = true;


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

    public Vector3 SideDroneMovementAxisWorld = Vector3.back; 
    public float SideDronePlaceBehind = 1f;


}