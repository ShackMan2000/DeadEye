using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DepthDrone : MonoBehaviour
{
    [SerializeField] EnemyBase enemyBase;

    [SerializeField] ShotReceiver coreShotReceiver;

    EnemySettings settings => enemyBase.Settings;

    [SerializeField] float laserDistance = 10f;

    [SerializeField] List<Transform> lasers;
    [SerializeField] List<Renderer> laserRenderers;

    [SerializeField] List<SideDrone> sideDrones;

    [SerializeField] Renderer coreRender;
  //  [SerializeField] Material burnMaterial;

    [SerializeField] Transform laserPivot;
    [SerializeField] Transform pivot;

    [SerializeField] PlayerPosition playerPosition;

    static readonly int AlphaReveal = Shader.PropertyToID("_AlphaReveal");


    Coroutine showLasersRoutine;

    bool freezeSideDrones = false;
    // must have a side thing[s], that is only damageable and destructable by the laser

    // need this because freezing and unfreezing the side drones should use same time.time
    float totalTimePassed = 0;
    static readonly int Burn = Shader.PropertyToID("_Burn");


 

    void OnEnable()
    {
        enemyBase.OnShotByAnyWeapon += SpawnLasers;
        enemyBase.OnInitialized += SetSideDronesAndLaserPositions;
        if (enemyBase.IsInitialized)
        {
            SetSideDronesAndLaserPositions();
        }
    }

    void OnDisable()
    {
        enemyBase.OnShotByAnyWeapon -= SpawnLasers;
        enemyBase.OnInitialized -= SetSideDronesAndLaserPositions;
    }


    [Button]
    void SetSideDronesAndLaserPositions()
    {
        // for now assuming only 1 or 2 side drones, so just alternate between left and right
        float placementDirection = 1f;

        foreach (SideDrone sideDrone in sideDrones)
        {
            placementDirection *= -1f;

            Vector3 startPosition = settings.PlacementAxis * settings.PlacementDistance * placementDirection;
            sideDrone.transform.localPosition = startPosition;
            sideDrone.StartPositionLocal = startPosition;


            //for launch drone
           // sideDrone.transform.localPosition += settings.SideDroneMovementAxisWorld * settings.SideDronePlaceBehind;
        }

        // rotate laster pivot on Z axis
        laserPivot.Rotate(Vector3.forward, settings.LaserPivotRotation, Space.Self);
        laserPivot.localRotation = Quaternion.Euler(0, 0, settings.LaserPivotRotation);
    }


    [Button]
    void SpawnLasers()
    {
        showLasersRoutine = StartCoroutine(ShowLasersRoutine());


        int sideDronesHit = 0;
        foreach (Transform laser in lasers)
        {
            //raycast all 
            RaycastHit[] hits = Physics.RaycastAll(laser.position, laser.forward, laserDistance);


            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out SideDrone sideDrone))
                {
                    // to not hit other drones' side drones
                    if (sideDrones.Contains(sideDrone))
                    {
                        sideDronesHit++;
                        sideDrone.GetHitByLaser(laser.forward, settings);
                    }
                }
            }
        }

        if (sideDronesHit > 0 && sideDronesHit != sideDrones.Count)
        {
            Debug.LogError("Side drones hit is not 0 or equal to side drones count. Probably an error because laser should hit all or none");
        }

        if (sideDronesHit > 0)
        {
            enemyBase.RaiseDestroyedByCorrectWeaponEvent();
            StartCoroutine(BurnCoreRoutine());
        }
        else
        {
            enemyBase.RaiseShotByMistakeEvent();
        }
    }

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

            if (settings.BackAndForthSpeed > 0.01f)
            {
            MoveSideDrones();
                
            }
            else
            {
                
            RotateSideDrones();
            }
        }
    }

    void MoveSideDrones()
    {
        float sineWave = Mathf.Sin(totalTimePassed * settings.BackAndForthSpeed);


        foreach (SideDrone sideDrone in sideDrones)
        {
            float newZ = sineWave * settings.BackAndForthDistance;
            sideDrone.transform.localPosition = new Vector3(sideDrone.StartPositionLocal.x, sideDrone.StartPositionLocal.y, newZ);
        }
    }

    void RotateSideDrones()
    {
        foreach (SideDrone sideDrone in sideDrones)
        {
            sideDrone.transform.RotateAround(pivot.position, pivot.transform.up, settings.SideDronesRotationSpeed * Time.deltaTime);
            sideDrone.transform.LookAt(playerPosition.Position);
        }
    }
}