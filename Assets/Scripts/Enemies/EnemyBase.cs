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

    public event Action OnShootAtPlayer = delegate { };

    public static event Action<EnemySettings, bool> OnAnyEnemyDestroyedCorrectly = delegate { };
    public static event Action<EnemySettings> OnMultiDroneShotAtWrongTime = delegate { };

    // this is getting messy. idea was that the pool can put it back in the correct list, needs to know which prefab this was created from.
    public static event Action<EnemyBase, EnemyBase> OnAnyEnemyDestroyedPrefabType = delegate { };

    public event Action OnEnemyDestroyed = delegate { };
    public EnemyBase Prefab { get; set; }

    public bool IsInitialized = false;

    public bool CanShoot => Settings.CanShoot;

    void OnEnable()
    {
        shotReceiver.OnShot += OnShot;
        shotReceiver.ShootingBlocked = false;
    }

    void OnDisable()
    {
        shotReceiver.OnShot -= OnShot;
    }

    void OnShot(WeaponType weaponType)
    {
        bool correctWeapon = Settings.CorrectWeaponsToGetShot.Contains(weaponType);
        
        if (GetsDestroyedByGunshot)
        {
            OnAnyEnemyDestroyedCorrectly(Settings, correctWeapon);
            GetDestroyed(correctWeapon);
        }


        // for the multidrone
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
        OnMultiDroneShotAtWrongTime(Settings);
    }

    
    // not 100% clean because this is really the multidrone being destroyed, regardless of the weapon
    public void RaiseDestroyedByCorrectWeaponEvent(bool isWeaponCorrect)
    {
        OnAnyEnemyDestroyedCorrectly(Settings, isWeaponCorrect);
    }


    public void GetDestroyed(bool correctWeapon = false)
    {
        shotReceiver.ShootingBlocked = true;
        OnSpawnExplosion(transform.position);

        OnAnyEnemyDestroyedPrefabType(this, Prefab);

        IsInitialized = false;
        gameObject.SetActive(false);
        OnEnemyDestroyed?.Invoke();
    }


    // do something later, like move into town/portal/train etc
    public void DisappearWhenPlayerGotKilled()
    {
        OnAnyEnemyDestroyedPrefabType(this, Prefab);
        gameObject.SetActive(false);
    }

    // this is getting messy, should all be in one method that initializes. 
    public void SetLingerPoint(Vector3 checkPoint)
    {
        movement.SetLingerPoint(checkPoint);
    }

    public void ShootAtPlayer()
    {
        OnShootAtPlayer?.Invoke();
    }
}