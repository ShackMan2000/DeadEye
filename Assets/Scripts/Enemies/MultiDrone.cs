using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MultiDrone : MonoBehaviour
{
    [SerializeField] EnemyBase enemyBase;

    [SerializeField] ShotReceiver coreShotReceiver;

    EnemySettings settings => enemyBase.Settings;

    [SerializeField] float laserDistance = 10f;

    [SerializeField] List<Transform> lasers;
    [SerializeField] List<Renderer> laserRenderers;

    [SerializeField] List<SideDrone> sideDrones;

    [SerializeField] Renderer coreRender;


    [SerializeField] Transform laserPivot;
    [SerializeField] Transform pivot;

    public float HitRangeInUnits = 1f;

    [SerializeField] PlayerPosition playerPosition;

    static readonly int AlphaReveal = Shader.PropertyToID("_AlphaReveal");

    [SerializeField] float sideDronesCurrentRangeRelative;

    Coroutine showLasersRoutine;

    bool freezeSideDrones = false;
    // must have a side thing[s], that is only damageable and destructable by the laser

    // need this because freezing and unfreezing the side drones should use same time.time
    float totalTimePassed = 0;
    static readonly int Burn = Shader.PropertyToID("_Burn");


    public static event Action<MultiDroneHitInfo> OnMultiDroneShot = delegate { };

    void OnEnable()
    {
        enemyBase.OnShotByAnyWeapon += OnGettingShot;
        enemyBase.OnInitialized += SetSideDronesAndLaserPositions;
        if (enemyBase.IsInitialized)
        {
            SetSideDronesAndLaserPositions();
        }
    }

    void OnDisable()
    {
        enemyBase.OnShotByAnyWeapon -= OnGettingShot;
        enemyBase.OnInitialized -= SetSideDronesAndLaserPositions;
    }


    [Button]
    void SetSideDronesAndLaserPositions()
    {
        // for now assuming only 1 or 2 side drones, so just alternate between left and right
        float placementDirection = 1f;
        coreRender.material.SetFloat(Burn, 0f);

        foreach (Renderer laserRenderer in laserRenderers)
        {
            laserRenderer.material.SetFloat(AlphaReveal, 0f);
        }

        freezeSideDrones = false;

        foreach (SideDrone sideDrone in sideDrones)
        {
            sideDrone.gameObject.SetActive(true);

            placementDirection *= -1f;
            Vector3 startPosition = settings.PlacementAxis * settings.PlacementDistance * placementDirection;
            sideDrone.transform.localPosition = startPosition;
            sideDrone.StartPositionLocal = startPosition;

            sideDrone.ResetBurnMaterial();
        }


        // rotate laster pivot on Z axis
        laserPivot.Rotate(Vector3.forward, settings.LaserPivotRotation, Space.Self);
        laserPivot.localRotation = Quaternion.Euler(0, 0, settings.LaserPivotRotation);

    }


    public Vector3 sideDroneInLocalSpace;
    public float SideDroneCurrentOffsetRelative;
    public float sideDroneOffsetForKillRelative;

    [Button]
    void OnGettingShot()
    {
        showLasersRoutine = StartCoroutine(ShowLasersRoutine());

        sideDroneInLocalSpace = pivot.InverseTransformPoint(sideDrones[0].transform.position);

        float maxOffset;
        if (settings.SideDronesMovementType == SideDronesMovementType.BackAndForth)
        {
            maxOffset = settings.BackAndForthDistance;
        }
        else
        {
            maxOffset = settings.PlacementDistance;
        }
        
         SideDroneCurrentOffsetRelative = sideDroneInLocalSpace.z / maxOffset;
         sideDroneOffsetForKillRelative = HitRangeInUnits /maxOffset;

        if (Mathf.Abs(sideDroneInLocalSpace.z) <= HitRangeInUnits)
        {
            StartCoroutine(BurnCoreRoutine());
            sideDrones[0].GetHitByLaser(lasers[0].forward, settings);
            sideDrones[1].GetHitByLaser(lasers[1].forward, settings);
        }
        
        
        MultiDroneHitInfo hitInfo = new MultiDroneHitInfo()
        {
            Settings = settings,
            OffsetOnShot = SideDroneCurrentOffsetRelative,
            OffsetMaxToDestroy = sideDroneOffsetForKillRelative,
            RotationRelative = sideDronesCurrentRangeRelative
        };
        
        OnMultiDroneShot(hitInfo);
    }

    // int CheckSideDronesViaRaycast()
    // {
    //     int sideDronesHit = 0;
    //     foreach (Transform laser in lasers)
    //     {
    //         RaycastHit[] hits = Physics.RaycastAll(laser.position, laser.forward, laserDistance);
    //
    //         foreach (RaycastHit hit in hits)
    //         {
    //             if (hit.collider.gameObject.TryGetComponent(out SideDrone sideDrone))
    //             {
    //                 // to not hit other drones' side drones
    //                 if (sideDrones.Contains(sideDrone))
    //                 {
    //                     sideDronesHit++;
    //                     sideDrone.GetHitByLaser(laser.forward, settings);
    //                 }
    //             }
    //         }
    //     }
    //
    //     if (sideDronesHit > 0 && sideDronesHit != sideDrones.Count)
    //     {
    //         Debug.LogError("Side drones hit is not 0 or equal to side drones count. Probably an error because laser should hit all or none");
    //     }
    //
    //     return sideDronesHit;
    // }

    IEnumerator BurnCoreRoutine()
    {
        float timePassed = 0;
        float burnTime = settings.LaserExpansionTime + settings.LaserStayTime;
        while (timePassed < burnTime)
        {
            timePassed += Time.deltaTime;
            float t = timePassed / burnTime;
            coreRender.material.SetFloat(Burn, t);
            yield return null;
        }

        if (showLasersRoutine != null)
        {
            StopCoroutine(showLasersRoutine);
        }

        enemyBase.GetDestroyed();
    }


    IEnumerator ShowLasersRoutine()
    {
        coreShotReceiver.ShootingBlocked = true;

        freezeSideDrones = true;

        float timePassed = 0;

        while (timePassed < settings.LaserExpansionTime)
        {
            timePassed += Time.deltaTime;
            float t = timePassed / settings.LaserExpansionTime;
            foreach (Renderer laserRenderer in laserRenderers)
            {
                laserRenderer.material.SetFloat(AlphaReveal, t);
            }

            yield return null;
        }

        timePassed = 0;
        while (timePassed < settings.LaserStayTime)
        {
            timePassed += Time.deltaTime;
            yield return null;
        }

        timePassed = 0;
        while (timePassed < settings.LaserShrinkTime)
        {
            timePassed += Time.deltaTime;
            float t = 1 - timePassed / settings.LaserShrinkTime;
            foreach (Renderer laserRenderer in laserRenderers)
            {
                laserRenderer.material.SetFloat(AlphaReveal, t);
            }

            yield return null;
        }

        freezeSideDrones = false;
        coreShotReceiver.ShootingBlocked = false;
    }

    void Update()
    {
        if (settings == null || !freezeSideDrones)
        {
            totalTimePassed += Time.deltaTime;

            if (settings.SideDronesMovementType == SideDronesMovementType.BackAndForth)
            {
                MoveSideDrones();
            }
            else
            {
                RotateSideDrones();
            }
            
         //   OnGettingShot();
        }
    }


    // public bool inHitRange;
    //
    // void CheckIfSideDroneInHitRange()
    // {      
    //     Vector3 sideDroneInLocalSpace = pivot.InverseTransformPoint(sideDrones[0].transform.position);
    //
    //     float SideDroneCurrentOffsetRelative = sideDroneInLocalSpace.z / settings.PlacementDistance;
    //     float sideDroneOffsetForKillRelative = HitRangeInUnits / settings.PlacementDistance;
    //
    //     inHitRange = Mathf.Abs(sideDroneInLocalSpace.z) <= HitRangeInUnits;
    // }

    void MoveSideDrones()
    {
        float sineWave = Mathf.Sin(totalTimePassed * settings.BackAndForthSpeed);

        sideDronesCurrentRangeRelative = sineWave;

        foreach (SideDrone sideDrone in sideDrones)
        {
            float newZ = sineWave * settings.BackAndForthDistance;
            sideDrone.transform.localPosition = new Vector3(sideDrone.StartPositionLocal.x, sideDrone.StartPositionLocal.y, newZ);
        }
    }

    float rotation;

    void RotateSideDrones()
    {
        float rotationToAdd = Time.deltaTime * settings.SideDronesRotationSpeed;
        rotation += rotationToAdd;

        sideDronesCurrentRangeRelative = (rotation % 360f) / 360f;

        foreach (SideDrone sideDrone in sideDrones)
        {
            //sideDrone.transform.localRotation = Quaternion.Euler(0, rotation, 0);
            sideDrone.transform.RotateAround(pivot.position, pivot.transform.up, rotationToAdd);
            sideDrone.transform.LookAt(playerPosition.Position);
        }
    }


    [Button]
    void SetSideDronesHitRangeBasedOnCollider()
    {
        Debug.Log("WARNING! This assumes the side drones have a scale of 1,1,1");
        HitRangeInUnits = sideDrones[0].GetComponent<SphereCollider>().radius;
    }
}

public struct MultiDroneHitInfo
{
    public EnemySettings Settings;
    public float OffsetOnShot;
    public float OffsetMaxToDestroy;
    public float RotationRelative;

    public bool IsCorrectRange => Mathf.Abs(OffsetOnShot) <= OffsetMaxToDestroy;
}