using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;


public class GunOverheat : MonoBehaviour
{
    
    [FormerlySerializedAs("shooter")] [SerializeField] Gun gun;

    [SerializeField] float overheatPerShot = 0.1f;
    [SerializeField] float overheatReductionPerSecond = 0.15f;
    [SerializeField] float overheatLockedTime = 3f;
    
    [SerializeField] float overheatEffectTime = 0.2f;

    [SerializeField] Renderer overheatRenderer;
    
    [ShowInInspector, ReadOnly] float currentOverheat = 0f;
    static readonly int OverheatAmount = Shader.PropertyToID("_OverheatAmount");

    
    [SerializeField] ParticleSystem smokeSystem;

    [SerializeField] float maxSmokeEmission;
    ParticleSystem.EmissionModule smokeEmission;
    
    
    void Awake()
    {
        smokeEmission = smokeSystem.emission;
        maxSmokeEmission = smokeEmission.rateOverTime.constant;
        smokeEmission.rateOverTime = 0f;

    }

    void OnEnable()
    {
        gun.OnShotFired += IncreaseOverheat;
        
        gun.IsLockedByOverheat = false;
        currentOverheat = 0f;
    }
        
    void OnDisable()
    {
        gun.OnShotFired -= IncreaseOverheat;
    }
    
    
    
    [Button]
    void IncreaseOverheat(WeaponType weaponType)
    {
        currentOverheat += overheatPerShot;
        
        if (currentOverheat >= 1f && !gun.IsLockedByOverheat)
        {
            ToggleOverheat(true);
        }
    }

    void ToggleOverheat(bool activate)
    {
        if (activate)
        {
            currentOverheat += overheatReductionPerSecond * overheatLockedTime;
            gun.IsLockedByOverheat = true;
            smokeEmission.rateOverTime = maxSmokeEmission;
            smokeSystem.Play();
        }
        else
        {
            gun.IsLockedByOverheat = false;
            smokeEmission.rateOverTime = 0f;
            smokeSystem.Stop();
        }
    }
 

    void Update()
    {
        if (Mathf.Approximately(currentOverheat, 0f))
        {
            return;
        }
        
        currentOverheat -= overheatReductionPerSecond * Time.deltaTime;
        
        
        
        if (currentOverheat < 1f && gun.IsLockedByOverheat)
        {
            ToggleOverheat(false);
        }
        
        if (currentOverheat < 0f)
        {
            currentOverheat = 0f;
        }
        
        if (currentOverheat > 0f)
        {
            overheatRenderer.material.SetFloat(OverheatAmount, Mathf.Clamp01(currentOverheat));
        }
        else
        {
            overheatRenderer.material.SetFloat(OverheatAmount, 0f);
        }
    }

}