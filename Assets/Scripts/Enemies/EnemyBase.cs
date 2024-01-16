using System;
using FluffyUnderware.Curvy;
using UnityEngine;



[SelectionBase]
public class EnemyBase : MonoBehaviour
{

    public EnemySettings Settings;

    [SerializeField] EnemyMovement movement;

    [SerializeField] ShotReceiver shotReceiver;

    public bool GetsDestroyedByGunshot;

    public event Action OnInitialized = delegate { };
    public event Action OnShotByAnyWeapon = delegate { };
    public static event Action<EnemySettings,Vector3> OnSpawnExplosion = delegate { };

    public static event Action<EnemySettings, bool, Vector3> OnAnySingleEnemyDestroyedCorrectly = delegate { };

    public static event Action<EnemyBase, EnemyBase> OnAnyEnemyDestroyedPrefabType = delegate { };

    public event Action OnEnemyDestroyed = delegate { };
    public EnemyBase Prefab { get; set; }

    public bool IsInitialized = false;

   // public bool CanShoot => Settings.CanShoot;

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
            OnSpawnExplosion(Settings, transform.position);
            OnAnySingleEnemyDestroyedCorrectly(Settings, correctWeapon, transform.position);

            GetDestroyedByPlayer();
        }

        

        OnShotByAnyWeapon();
    }

    public void Initialize(EnemySettings settingsOption, CurvySpline spline, bool removeOnPathEnd)
    {
        Settings = settingsOption;
        gameObject.SetActive(true);
        
        movement.Initialize(Settings, spline, removeOnPathEnd);
        IsInitialized = true;
        
        OnInitialized?.Invoke();
    }
    
    
    
    // public void Initialize(EnemySettings settingsOption, CheckPointsList checkPointsList)
    // {
    //     Settings = settingsOption;
    //     gameObject.SetActive(true);
    //     
    //     movement.Initialize(Settings, checkPointsList);
    //     IsInitialized = true;
    //     
    //     OnInitialized?.Invoke();
    // }

    
    public void SpawnExplosion()
    {
        OnSpawnExplosion(Settings, transform.position);
    }

  
    public void GetDestroyedByPlayer()
    {
        shotReceiver.ShootingBlocked = true;

        movement.OnDestroyed();
        IsInitialized = false;
        gameObject.SetActive(false);
        OnEnemyDestroyed?.Invoke();
        
        //might be important to fire last, because that is the one that will end the wave. Could also play it safe and delay the end a tiny bit
        OnAnyEnemyDestroyedPrefabType(this, Prefab);
    }

    
    // end of game (wave over, time trial over, player killed) or enemy reached second gate
    public void DeactivateViaManager()
    {
        OnAnyEnemyDestroyedPrefabType(this, Prefab);
        gameObject.SetActive(false);
    }



   

}