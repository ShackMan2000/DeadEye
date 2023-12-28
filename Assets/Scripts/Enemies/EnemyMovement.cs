using System;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;
using Sirenix.OdinInspector;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    EnemySettings settings;

    [SerializeField] EnemyBase enemyBase;

    [SerializeField] PlayerPosition playerPosition;

    [SerializeField] SplineController splineController;

    [SerializeField] Transform pivot;
    

    public static  event Action<CurvySpline> OnSplineFreedUp = delegate { };


    [SerializeField] CurvySpline spline;
    
    bool isMoving;

    int reservedLingerPointIndex;

    [SerializeField] float distanceToReachTarget = 0.1f;

    int currentTargetIndex;

    Vector3 currentTargetPosition;


    void OnEnable()
    {
        splineController.OnEndReached.AddListener(OnEndReached);
    }


    void OnDisable()
    {
        splineController.OnEndReached.RemoveListener(OnEndReached);
    }

    // public void Initialize(EnemySettings s, CheckPointsList checkPointsList)
    // {
    //     settings = s;
    //
    //     checkPoints = checkPointsList;
    //     isMoving = true;
    //
    //     currentTargetIndex = -1;
    //
    //     if (settings.MovementType == EnemyMovementType.FixedPath)
    //     {
    //         PickNextTarget();
    //     }
    // }

    public void Initialize(EnemySettings enemySettings, CurvySpline s, bool isLooping)
    {
        settings = enemySettings;
        spline = s;

        splineController.Spline = s;
        
        splineController.AbsolutePosition = 0;
        splineController.Speed = settings.MovementSpeed;
        splineController.Clamping = isLooping ? CurvyClamping.Loop : CurvyClamping.Clamp;

        splineController.Refresh();
        isMoving = true;
    }

    // void PickNextTarget()
    // {
    //     if (checkPoints == null || checkPoints.CheckPoints.Count == 0)
    //     {
    //         Debug.LogError("No checkpoints found, disabled movement");
    //         isMoving = false;
    //         return;
    //     }
    //
    //
    //     currentTargetIndex++;
    //     if (currentTargetIndex >= checkPoints.CheckPoints.Count)
    //     {
    //         currentTargetIndex = 0;
    //     }
    //
    //     currentTargetPosition = checkPoints.CheckPoints[currentTargetIndex];
    // }


    void LateUpdate()
    {
        
        // use reached end event instead
        if (settings.RotateTowardsPlayer && splineController.RelativePosition < 0.99f)
        {
            RotatePivotTowardsPlayer();
        }
    }


    // for now could also make a method that if it hasn't reached a checkpoint in x seconds, pick the next one. 
    // or even if distance to checkpoint is not decreasing enough since the last second, it's stuck and should pick the next one

    // void Move()
    // {
    //     float movementSpeed = settings.MovementSpeed * Time.deltaTime;
    //     float rotationSpeed = settings.RotationSpeed * Time.deltaTime;
    //
    //     transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(currentTargetPosition - transform.position), rotationSpeed);
    //     transform.position += transform.forward * movementSpeed;
    //
    //     if (Vector3.Distance(transform.position, currentTargetPosition) < distanceToReachTarget)
    //     {
    //         if (settings.MovementType == EnemyMovementType.Linger)
    //         {
    //             isMoving = false;
    //         }
    //         else
    //         {
    //             PickNextTarget();
    //         }
    //     }
    // }

    void RotatePivotTowardsPlayer()
    {
        pivot.rotation = Quaternion.LookRotation(playerPosition.Position - pivot.position);
    }

    
    void OnEndReached(CurvySplineMoveEventArgs arg0)
    {
        if(splineController.Clamping == CurvyClamping.Clamp)
        {
            enemyBase.DeactivateViaManager();
        }
    }


    public void OnDestroyed()
    {
        if (settings.MovementType == EnemyMovementType.Linger)
        {
            OnSplineFreedUp?.Invoke(splineController.Spline);
        }
    }
}


public enum EnemyMovementType
{
    FixedPath,
    Linger
}