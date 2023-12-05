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

    public bool GetsDestroyedByGunshot;

    public event Action OnShotByAnyWeapon = delegate { };
    public static event Action<Vector3> OnSpawnExplosion = delegate { };

    public static event Action<EnemySettings> OnAnyEnemyDestroyedCorrectly = delegate { };
    public static event Action<EnemySettings> OnAnyEnemyShotByMistake = delegate { };
    
    public static event Action<EnemyBase,EnemyBase> OnAnyEnemyDestroyedPrefabType = delegate { };
    public EnemyBase Prefab { get; set; }

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
        if (GetsDestroyedByGunshot)
        {
            if (correctWeapon)
            {
                OnAnyEnemyDestroyedCorrectly(settings);
            }
            else
            {
                OnAnyEnemyShotByMistake(settings);
            }


            GetDestroyed(correctWeapon);
        }

        // the issue is that the drone here has nothing to do with getting shot by the correct weapon, it's all about the timing.
        // and the drone 
        OnShotByAnyWeapon();
    }

    public void Initialize(EnemySettings settingsOption, CheckPointsList checkPointsList)
    {
        settings = settingsOption;
        movement.Initialize(settings, checkPointsList);
    }

    public void RaiseShotByMistakeEvent()
    {
        OnAnyEnemyShotByMistake(settings);
    }

    public void RaiseDestroyedByCorrectWeaponEvent()
    {
        OnAnyEnemyDestroyedCorrectly(settings);
    }


    public void GetDestroyed(bool correctWeapon = false)
    {
        shotReceiver.ShootingBlocked = true;
        OnSpawnExplosion(transform.position);
        
        OnAnyEnemyDestroyedPrefabType(this,Prefab);
        
        gameObject.SetActive(false);
    }
}