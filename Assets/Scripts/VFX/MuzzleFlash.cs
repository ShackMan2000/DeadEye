using UnityEngine;


public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlashParticleSystem;


    [SerializeField] ParticleSystem.MainModule mainModule;


    [SerializeField] Shooter shooter;

    void Awake()
    {
        mainModule = muzzleFlashParticleSystem.main;
        mainModule.startColor = shooter.SelectedWeaponType.Color;
    }

    void OnEnable()
    {
        shooter.OnShotFired += SpawnMuzzleFlash;
    }

    void OnDisable()
    {
        shooter.OnShotFired -= SpawnMuzzleFlash;
    }

    void SpawnMuzzleFlash(WeaponType weaponType)
    {
        if (muzzleFlashParticleSystem != null)
        {
            muzzleFlashParticleSystem.Play();
        }
        else
        {
            Debug.LogError("Muzzle flash particle system not found");
        }
    }
}