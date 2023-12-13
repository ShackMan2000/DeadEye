using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[InlineEditor()]
[CreateAssetMenu]
public class EnemySettings : ScriptableObject
{
    public EnemyBase Prefab;
    
    public Sprite Icon;
    public EnemyMovementType  MovementType;
    
    public float MovementSpeed;
    public float RotationSpeed;

    public List<WeaponType> CorrectWeaponsToGetShot;
    public float pointsForKill = 10f;

    public bool RotateTowardsPlayer = true;


    [InfoBox("Shooting")] 
    public bool CanShoot;
    public EnemyBullet BulletPrefab;
    public float ShootWarningTime = 3f;
    
    

    [InfoBox("Laser to Side Drones")] 
    public float LaserPivotRotation;
    public float LaserExpansionTime = 0.2f;
    public float LaserStayTime = 1f;
    public float LaserShrinkTime = 0.2f;

    
    
    [InfoBox("SideDrone")]
    
    public SideDronesMovementType SideDronesMovementType;
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


public enum SideDronesMovementType
{
    BackAndForth,
    RotateAround
}