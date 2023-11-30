using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField] EnemySettings settings;
    // keep all movement in here so the child can rotate towards the player

    [SerializeField] CheckPointsList checkPoints;

    bool useRandomPath;

    bool isMoving;

    [SerializeField] float distanceToReachTarget = 0.1f;

    [SerializeField] float zOffset;

    int currentTargetIndex;
    Vector3 currentTarget;


    public void InjectCheckPoints(CheckPointsList checkPointsList, float zOffset)
    {
        checkPoints = checkPointsList;
        this.zOffset = zOffset;
        isMoving = true;

        currentTargetIndex = -1;
        PickNextTarget();
    }

    void PickNextTarget()
    {
        if (checkPoints == null || checkPoints.CheckPoints.Count == 0)
        {
            Debug.LogError("No checkpoints found, disabled movement");
            isMoving = false;
            return;
        }

        if (useRandomPath)
        {
            currentTargetIndex = Random.Range(0, checkPoints.CheckPoints.Count);
        }
        else
        {
            currentTargetIndex++;
            if (currentTargetIndex >= checkPoints.CheckPoints.Count)
            {
                currentTargetIndex = 0;
            }

            currentTarget = checkPoints.CheckPoints[currentTargetIndex];
        }
    }


    void Update()
    {
        if (isMoving)
        {
            Move();
        }
    }
    

    // for now could also make a method that if it hasn't reached a checkpoint in x seconds, pick the next one. 
    // or even if distance to checkpoint is not decreasing enough since the last second, it's stuck and should pick the next one
    
    void Move()
    {
        float movementSpeed = settings.MovementSpeed * Time.deltaTime;
        float rotationSpeed = settings.RotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(currentTarget - transform.position), rotationSpeed);
        transform.position += transform.forward * movementSpeed;
        
        if (Vector3.Distance(transform.position, currentTarget) < distanceToReachTarget)
        {
            PickNextTarget();
        }
    }
    
    
    
    [Button]
    void TestStartMoving(bool randomPath)
    {
        useRandomPath = randomPath;
        isMoving = true;
        PickNextTarget();
    }
}