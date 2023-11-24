using System;
using System.Collections.Generic;
using UnityEngine;


public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlashParticleSystem;


    void OnEnable()
    {
        Shooter.OnShotFired += SpawnMuzzleFlash;
    }

    void OnDisable()
    {
        Shooter.OnShotFired -= SpawnMuzzleFlash;
    }

    void SpawnMuzzleFlash()
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