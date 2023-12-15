using Sirenix.OdinInspector;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    EnemySettings settings;

    [SerializeField] CheckPointsList checkPoints;


    [SerializeField] PlayerPosition playerPosition;


    [SerializeField] Transform pivot;


    bool isMoving;

    int reservedLingerPointIndex;

    [SerializeField] float distanceToReachTarget = 0.1f;

    int currentTargetIndex;

    Vector3 currentTargetPosition;


    public void Initialize(EnemySettings s, CheckPointsList checkPointsList)
    {
        settings = s;

        checkPoints = checkPointsList;
        isMoving = true;

        currentTargetIndex = -1;

        if (settings.MovementType == EnemyMovementType.FixedPath)
        {
            PickNextTarget();
        }
    }

    void PickNextTarget()
    {
        if (checkPoints == null || checkPoints.CheckPoints.Count == 0)
        {
            Debug.LogError("No checkpoints found, disabled movement");
            isMoving = false;
            return;
        }


        currentTargetIndex++;
        if (currentTargetIndex >= checkPoints.CheckPoints.Count)
        {
            currentTargetIndex = 0;
        }

        currentTargetPosition = checkPoints.CheckPoints[currentTargetIndex];
    }


    void Update()
    {
        if (isMoving)
        {
            Move();
        }

        if (settings.RotateTowardsPlayer)
        {
            RotatePivotTowardsPlayer();
        }
    }


    // for now could also make a method that if it hasn't reached a checkpoint in x seconds, pick the next one. 
    // or even if distance to checkpoint is not decreasing enough since the last second, it's stuck and should pick the next one

    void Move()
    {
        float movementSpeed = settings.MovementSpeed * Time.deltaTime;
        float rotationSpeed = settings.RotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(currentTargetPosition - transform.position), rotationSpeed);
        transform.position += transform.forward * movementSpeed;

        if (Vector3.Distance(transform.position, currentTargetPosition) < distanceToReachTarget)
        {
            if (settings.MovementType == EnemyMovementType.Linger)
            {
                isMoving = false;
            }
            else
            {
                PickNextTarget();
            }
        }
    }

    void RotatePivotTowardsPlayer()
    {
        pivot.rotation = Quaternion.LookRotation(playerPosition.Position - pivot.position);
    }


    [Button]
    void TestStartMoving()
    {
        isMoving = true;
        PickNextTarget();
    }


    public void SetLingerPoint(CheckPointsList cpl)
    {
        checkPoints = cpl;
        reservedLingerPointIndex = checkPoints.GetFreeIndex();
        currentTargetPosition = checkPoints.CheckPoints[reservedLingerPointIndex];
    }

    public void OnDestroyed()
    {
        if (settings.MovementType == EnemyMovementType.Linger)
        {
            checkPoints.FreeUpIndex(reservedLingerPointIndex);
        }
    }
}


public enum EnemyMovementType
{
    FixedPath,
    Linger
}