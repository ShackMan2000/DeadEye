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

    [SerializeField] List<Renderer> coreRenderers;


    [SerializeField] Transform laserPivot;
    [SerializeField] Transform pivot;

    //  public float HitRangeInUnits = 1f;

    [SerializeField] PlayerPosition playerPosition;

    [SerializeField] float sideDronesCurrentRangeRelative;

    [SerializeField] PopUp popUp;

    public bool alwaysShowAccuracyPopUp;


    static readonly int AlphaReveal = Shader.PropertyToID("_AlphaReveal");


    Coroutine showLasersRoutine;

    bool freezeSideDrones = false;
    float totalTimePassed = 0;

    static readonly int Burn = Shader.PropertyToID("_Burn");


    public float DebugPositionRelative;
    public float DebugRotationRelative;

    public static event Action<MultiDroneHitInfo> OnMultiDroneShot = delegate { };
    public static event Action<MultiDroneHitInfo> OnMultiDroneDestroyed = delegate { };


    void OnEnable()
    {
        enemyBase.OnShotByAnyWeapon += OnGettingShot;
        enemyBase.OnInitialized += SetSideDronesAndLaserPositions;
        if (enemyBase.IsInitialized)
        {
            SetSideDronesAndLaserPositions();
        }

        popUp.gameObject.SetActive(alwaysShowAccuracyPopUp);
    }

    void OnDisable()
    {
        enemyBase.OnShotByAnyWeapon -= OnGettingShot;
        enemyBase.OnInitialized -= SetSideDronesAndLaserPositions;
    }


    [Button]
    void SetSideDronesAndLaserPositions()
    {
        foreach (Renderer render in coreRenderers)
        {
            Material[] materials = render.materials;

            foreach (Material material in materials)
            {
                material.SetFloat(Burn, 0f);
            }

            render.materials = materials;
        }

        float placementDirection = 1f;

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

            if (sideDrone.transform.localRotation != Quaternion.identity)
            {
                sideDrone.transform.localRotation = Quaternion.identity;
            }
        }


        // rotate laster pivot on Z axis
        laserPivot.Rotate(Vector3.forward, settings.LaserPivotRotation, Space.Self);
        laserPivot.localRotation = Quaternion.Euler(0, 0, settings.LaserPivotRotation);
    }


    [ShowInInspector]
    public Vector3 SideDroneInLocalSpace => pivot.InverseTransformPoint(sideDrones[0].transform.position);

    public float SideDroneCurrentOffsetRelative => Mathf.Abs(SideDroneInLocalSpace.z / settings.MaxSideDroneOffsetInUnits());


    [Button]
    void OnGettingShot()
    {
        showLasersRoutine = StartCoroutine(ShowLasersRoutine());

        // sideDroneInLocalSpace = pivot.InverseTransformPoint(sideDrones[0].transform.position);


        // get hit range relative -1 to 1, need to get it all the way to stats display, could just assign this prefab

        MultiDroneHitInfo hitInfo = new MultiDroneHitInfo()
        {
            Settings = settings,
            OffsetOnShotRelative = SideDroneCurrentOffsetRelative,
            //RotationRelative = sideDronesCurrentRangeRelative,
            Position = transform.position
        };

        OnMultiDroneShot(hitInfo);

        if (Mathf.Abs(SideDroneInLocalSpace.z) <= settings.SideDronesHitRangeInUnits)
        {
            StartCoroutine(BurnCoreRoutine(hitInfo));
            sideDrones[0].GetHitByLaser(sideDrones[0].transform.position - lasers[0].transform.position, settings);
            sideDrones[1].GetHitByLaser(sideDrones[1].transform.position - lasers[1].transform.position, settings);
        }
    }


    IEnumerator BurnCoreRoutine(MultiDroneHitInfo multiDroneHitInfo)
    {
        enemyBase.SpawnExplosion();

        // the issue is that the drones get pushed away based on their direction towards the laser, which is never perfect Z
        // could push them away based on that though.
        // alternative is to set the text only once when getting shot. which is better for performance too, so let's do that
        // 
        enemyBase.movement.PauseMovement();

        //pause editor

        // so in the turorial mode the number doesn't get updated anymore
        alwaysShowAccuracyPopUp = false;

        float timePassed = 0;
        float burnTime = settings.LaserExpansionTime + settings.LaserStayTime;
        while (timePassed < burnTime)
        {
            timePassed += Time.deltaTime;
            float t = timePassed / burnTime;

            foreach (Renderer render in coreRenderers)
            {
                Material[] materials = render.materials;

                foreach (Material material in materials)
                {
                    material.SetFloat(Burn, t);
                }

                render.materials = materials;
            }

            yield return null;
        }

        if (showLasersRoutine != null)
        {
            StopCoroutine(showLasersRoutine);
        }

        enemyBase.GetDestroyedByPlayer();
        OnMultiDroneDestroyed(multiDroneHitInfo);
    }


    IEnumerator ShowLasersRoutine()
    {
        enemyBase.movement.PauseMovement();
        // separate, only show it once here, the other method will call that permanently
        ShowAccuracyPopUp();

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

        if (!alwaysShowAccuracyPopUp)
        {
            popUp.gameObject.SetActive(false);
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
        enemyBase.movement.ResumeMovement();
        coreShotReceiver.ShootingBlocked = false;
    }

    void Update()
    {
        if (GameManager.IsPaused) return;

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

            DebugPositionRelative = sideDronesCurrentRangeRelative;
        }

        if(alwaysShowAccuracyPopUp)
        {
            UpdatePopUp();
        }

        // convert this to some kind of permanent show. But do tutorial after list is done.
        // if (IsShowingAccuracyPopUp)
        // {
        //     UpdatePopUp();
        // }
    }


    public void ShowAccuracyPopUp()
    {
        if (popUp == null)
        {
            Debug.Log("no pop up assigned");
            return;
        }

        popUp.gameObject.SetActive(true);
        UpdatePopUp();
    }

    // actually don't have to separate text if it only shows while side drones stopped, which it should anyway. so just make it really fast
    // question is just how to show it on shot.... already have routine!
    // tutorial can override the hiding.

    void UpdatePopUp()
    {
        string accuracyText = Mathf.RoundToInt((1f - SideDroneCurrentOffsetRelative) * 100f).ToString() + "%";
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition.Position);
        // popUp.SetPosition(transform.position);
        popUp.SetTextAndScale(accuracyText, distanceToPlayer);

        popUp.transform.LookAt(playerPosition.Position);
        popUp.transform.Rotate(0, 180, 0);
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

    //  public bool blockRotation;

    void MoveSideDrones()
    {
        float offsetRaw = Mathf.PingPong(totalTimePassed * settings.BackAndForthSpeed, 1f);
        float offsetRemapped = settings.MovementCurve.Evaluate(offsetRaw);
        offsetRemapped = Mathf.Clamp(offsetRemapped, -1f, 1f);

        sideDronesCurrentRangeRelative = offsetRemapped;

        foreach (SideDrone sideDrone in sideDrones)
        {
            float newZ = offsetRemapped * settings.BackAndForthDistance;
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
        settings.SideDronesHitRangeInUnits = sideDrones[0].GetComponent<SphereCollider>().radius;
    }
}

public struct MultiDroneHitInfo
{
    public EnemySettings Settings;

    public float OffsetOnShotRelative;
    //  public float RotationRelative;


    public bool IsCorrectRange => Mathf.Abs(OffsetOnShotRelative) <= Settings.SideDroneOffsetForKillRelative();
    public Vector3 Position;
}