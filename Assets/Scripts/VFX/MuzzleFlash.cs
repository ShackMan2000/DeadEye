using System;
using System.Collections.Generic;
using UnityEngine;


public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlashParticleSystem;
    // main particle system
    [SerializeField] ParticleSystem muzzleFlashSparksParticleSystem;

    [SerializeField] ParticleSystem.MainModule mainModule;


    void Awake()
    {
        mainModule = muzzleFlashParticleSystem.main;
    }

    void OnEnable()
    {
        Shooter.OnShotFired += SpawnMuzzleFlash;
    }

    void OnDisable()
    {
        Shooter.OnShotFired -= SpawnMuzzleFlash;
    }

    void SpawnMuzzleFlash(WeaponType weaponType)
    {
        if (muzzleFlashParticleSystem != null)
        {
            mainModule.startColor = weaponType.Color;
            muzzleFlashParticleSystem.Play();
        }
        else
        {
            Debug.LogError("Muzzle flash particle system not found");
        }
    }
}