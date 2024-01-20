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


    public static event Action<CurvySpline> OnSplineFreedUp = delegate { };
    public static event Action OnEnemyLeftThroughGate = delegate { };


    [SerializeField] CurvySpline spline;

    int reservedLingerPointIndex;

    [SerializeField] float distanceToReachTarget = 0.1f;

    int currentTargetIndex;

    Vector3 currentTargetPosition;

    bool removeWhenReachingPathEnd;


    void OnEnable()
    {
        splineController.OnEndReached.AddListener(OnEndReached);
        GameManager.OnGamePaused += PauseMovement;
        GameManager.OnGameResumed += ResumeMovement;
    }


    void OnDisable()
    {
        splineController.OnEndReached.RemoveListener(OnEndReached);
        GameManager.OnGamePaused -= PauseMovement;
        GameManager.OnGameResumed -= ResumeMovement;
    }


    public void Initialize(EnemySettings enemySettings, CurvySpline s, bool removeOnPathEnd)
    {
        settings = enemySettings;
        spline = s;

        splineController.Spline = s;

        splineController.AbsolutePosition = 0;
        splineController.Speed = settings.MovementSpeed;
        removeWhenReachingPathEnd = removeOnPathEnd;

        splineController.Refresh();
    }


    void LateUpdate()
    {
        if (GameManager.IsPaused) return;

        // use reached end event instead
        if (settings.RotateTowardsPlayer && splineController.RelativePosition < 0.99f)
        {
            RotatePivotTowardsPlayer();
        }
    }


    void RotatePivotTowardsPlayer()
    {
        pivot.rotation = Quaternion.LookRotation(playerPosition.Position - pivot.position);
    }


    public void PauseMovement()
    {
        if (splineController != null)
        {
            splineController.Speed = 0;
        }
    }

    public void ResumeMovement()
    {
        if (splineController != null && settings != null && !GameManager.IsPaused)
        {
            splineController.Speed = settings.MovementSpeed;
        }
    }


    void OnEndReached(CurvySplineMoveEventArgs arg0)
        {
            if (removeWhenReachingPathEnd)
            {
                OnEnemyLeftThroughGate?.Invoke();
                enemyBase.DeactivateViaManager();
            }
        }


        public void OnDestroyed()
        {
            if (settings == null)
            {
                return;
            }
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