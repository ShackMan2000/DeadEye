using System;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBullet : MonoBehaviour
{

    public float Speed;

    bool isMoving;
    Vector3 direction;
    
    public static event Action OnAnyEnemyBulletHitPlayer = delegate { };

    public void Initialize(Vector3 position, Vector3 playerPositionPosition)
    {
        isMoving = true;
        transform.position = position;
        direction = (playerPositionPosition - position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += direction * (Speed * Time.deltaTime);
        }
    }
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnAnyEnemyBulletHitPlayer?.Invoke();
            Destroy(gameObject);
        }
    }
}