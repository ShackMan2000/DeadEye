using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class EnemyBullet : MonoBehaviour
{

    [SerializeField] PlayerHealth selectableOption;
    
    public float Speed;

    bool isMoving;
    Vector3 direction;
    
    public static event Action OnAnyEnemyBulletHitPlayer = delegate { };

    
    // could also tell it directly about the player. Thing is there is no player script yet...
    // could be the position updater...

    void OnEnable()
    {
        GameManager.OnExitShootingMode += DestroyBullet;
    }
    
    void OnDisable()
    {
        GameManager.OnExitShootingMode -= DestroyBullet;
    }


    public void Initialize(Vector3 position, Vector3 playerPositionPosition)
    {
        isMoving = true;
        transform.position = position;
        direction = (playerPositionPosition - position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void Update()
    {
        if (!GameManager.IsPaused && isMoving)
        {
            transform.position += direction * (Speed * Time.deltaTime);
        }
    }
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selectableOption.ReduceHealth();
            Destroy(gameObject);
        }
    }
    
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}