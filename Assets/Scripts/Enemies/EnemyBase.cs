using System;
using UnityEngine;


public class EnemyBase : MonoBehaviour
{
    // could do the settings directly in here, a list of options
    // would need a bool first so wave controller can check if there is any option avaialble for this wave

    public EnemySettings Settings;

    [SerializeField] EnemyMovement movement;

    [SerializeField] ShotReceiver shotReceiver;

    public bool GetsDestroyedByGunshot;

    
    public event Action OnInitialized = delegate { };
    public event Action OnShotByAnyWeapon = delegate { };
    public static event Action<Vector3> OnSpawnExplosion = delegate { };

    public static event Action<EnemySettings> OnAnyEnemyDestroyedCorrectly = delegate { };
    public static event Action<EnemySettings> OnAnyEnemyShotByMistake = delegate { };
    
    public static event Action<EnemyBase,EnemyBase> OnAnyEnemyDestroyedPrefabType = delegate { };
    public EnemyBase Prefab { get; set; }

    public bool IsInitialized = false;
    
    

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
                OnAnyEnemyDestroyedCorrectly(Settings);
            }
            else
            {
                OnAnyEnemyShotByMistake(Settings);
            }


            GetDestroyed(correctWeapon);
        }

        // the issue is that the drone here has nothing to do with getting shot by the correct weapon, it's all about the timing.
        // and the drone 
        OnShotByAnyWeapon();
    }

    public void Initialize(EnemySettings settingsOption, CheckPointsList checkPointsList)
    {
        Settings = settingsOption;
        movement.Initialize(Settings, checkPointsList);
        IsInitialized = true;
        OnInitialized?.Invoke();
    }

    public void RaiseShotByMistakeEvent()
    {
        OnAnyEnemyShotByMistake(Settings);
    }

    public void RaiseDestroyedByCorrectWeaponEvent()
    {
        OnAnyEnemyDestroyedCorrectly(Settings);
    }


    public void GetDestroyed(bool correctWeapon = false)
    {
        shotReceiver.ShootingBlocked = true;
        OnSpawnExplosion(transform.position);
        
        OnAnyEnemyDestroyedPrefabType(this,Prefab);
        
        IsInitialized = false;
        gameObject.SetActive(false);
    }

    // this is getting messy, should all be in one method that initializes. 
    public void SetLingerPoint(Vector3 checkPoint)
    {
        movement.SetLingerPoint(checkPoint);
    }
}