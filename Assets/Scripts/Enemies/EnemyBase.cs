using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class EnemyBase : MonoBehaviour
{

    // could do the settings directly in here, a list of options
    // would need a bool first so wave controller can check if there is any option avaialble for this wave

    [ShowInInspector] EnemySettings settings;
    
    [SerializeField] EnemyMovement movement;

    [SerializeField] ShotReceiver shotReceiver;

    public bool DestroyedWithOneShot;
    
    public event Action<bool> OnShotCorrectly = delegate { };
    public static event Action<bool> OnAnyEnemyShotCorrectly  = delegate { };
    public static event Action<Vector3> OnSpawnExplosion = delegate { };

    void OnEnable()
    {
        shotReceiver.OnShotByCorrectWeapon += OnShotByCorrectWeapon;
        shotReceiver.ShootingBlocked = false;
    }
    
    void OnDisable()
    {
        shotReceiver.OnShotByCorrectWeapon -= OnShotByCorrectWeapon;
    }

    void OnShotByCorrectWeapon(bool correctWeapon)
    {
        if (DestroyedWithOneShot)
        {
            GetDestroyed(correctWeapon);
            shotReceiver.ShootingBlocked = true;
        }
        
        OnShotCorrectly(correctWeapon);
        OnAnyEnemyShotCorrectly(correctWeapon);
    }

    public void Initialize(EnemySettings settingsOption, CheckPointsList checkPointsList)
    {
        settings = settingsOption;
        movement.Initialize(settings, checkPointsList);
    }
    
    
    void GetDestroyed(bool correctWeapon)
    {
        OnSpawnExplosion(transform.position);
        gameObject.SetActive(false);
    }
}


