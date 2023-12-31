using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[InlineEditor()]
[CreateAssetMenu]
public class EnemySettings : ScriptableObject
{

    public string GUID;
    
    [HorizontalGroup("main")]
    
    [VerticalGroup("main/left"), HideLabel, Title("Prefab")]
    public EnemyBase Prefab;

    [VerticalGroup("main/left"), PreviewField(ObjectFieldAlignment.Left), ShowInInspector, HideLabel]
    public GameObject prefabGO => Prefab.gameObject;
    
    [VerticalGroup("main/right"), Title("Icon")]
    public Color IconColor;
    
    [VerticalGroup("main/right"), HideLabel, PreviewField(ObjectFieldAlignment.Right)]
    public Sprite Icon;

    
    
    
    


    [Space(50f)]
    [EnumToggleButtons]
    public EnemyDifficulty Difficulty;

    public EnemyMovementType MovementType;

    public float MovementSpeed;

    public List<WeaponType> CorrectWeaponsToGetShot;
    public float pointsForKill = 10f;

    public bool RotateTowardsPlayer = true;


    [InfoBox("Shooting")] public bool CanShoot;
    
    [ShowIf("CanShoot")]
    [SerializeField] ShootingSettings shootingSettings;
    
    [SerializeField] struct ShootingSettings
    {
        public EnemyBullet BulletPrefab;
        public float ShootWarningTime;
        public Vector2 ShootingInterval;

    }
    public EnemyBullet BulletPrefab;
    public float ShootWarningTime = 3f;
    public Vector2 ShootingInterval = new Vector2(5f, 10f);


    [InfoBox("Laser to Side Drones")] public float LaserPivotRotation;
    public float LaserExpansionTime = 0.2f;
    public float LaserStayTime = 1f;
    public float LaserShrinkTime = 0.2f;


    [InfoBox("SideDrone")] public float SideDronesHitRangeInUnits;
    public SideDronesMovementType SideDronesMovementType;
    public float BackAndForthSpeed = 1f;
    public float BackAndForthDistance = 1f;
    public AnimationCurve MovementCurve;

    public float LaserKnockbackSpeed = 1f;
    public float LaserKnockbackTime = 1f;

    public Vector3 PlacementAxis = Vector3.right;
    public float PlacementDistance = 1f;

    public Vector3 RotationAxis = Vector3.up;
    public float SideDronesRotationSpeed = 1f;

    public Vector3 SideDroneMovementAxisWorld = Vector3.back;
    public float SideDronePlaceBehind = 1f;

    
    
    public float SideDroneOffsetForKillRelative()
    {
        return SideDronesHitRangeInUnits / MaxSideDroneOffsetInUnits();
    }

    public float MaxSideDroneOffsetInUnits()
    {
        if (SideDronesMovementType == SideDronesMovementType.BackAndForth)
        {
            return BackAndForthDistance;
        }
        else
        {
            return PlacementDistance;
        }
    }


    [Button("Generate GUID")]
    void OnValidate()
    {
        if (string.IsNullOrEmpty(GUID))
        {
            GUID = Guid.NewGuid().ToString();
#if UNITY_EDITOR
            
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }
    }
}


public enum EnemyDifficulty
{
    [LabelText("Easy", SdfIconType.EmojiSmile)] Easy,
    [LabelText("Hard", SdfIconType.EmojiAngry)] Hard
}

public enum SideDronesMovementType
{
    BackAndForth,
    RotateAround
}