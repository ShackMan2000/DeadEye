using UnityEngine;
using UnityEngine.Serialization;


public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlashParticleSystem;


    [SerializeField] ParticleSystem.MainModule mainModule;


    [FormerlySerializedAs("shooter")] [SerializeField] Gun gun;

    void Awake()
    {
        mainModule = muzzleFlashParticleSystem.main;
        mainModule.startColor = gun.SelectedWeaponType.Color;
    }

    void OnEnable()
    {
        gun.OnShotFired += SpawnMuzzleFlash;
    }

    void OnDisable()
    {
        gun.OnShotFired -= SpawnMuzzleFlash;
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