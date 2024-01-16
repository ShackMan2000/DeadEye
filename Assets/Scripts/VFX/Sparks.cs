using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class Sparks : MonoBehaviour
{

    SparksSpawner spawner;
    
    // particle system that emits one time sparks
    
    [SerializeField] ParticleSystem sparksParticleSystem;
    public WeaponType WeaponType;


    public void Initialize(SparksSpawner sparksSpawner, WeaponType wt)
    {
        spawner = sparksSpawner;
        WeaponType = wt;
    }
    
    
    public void PlaySparks()
    {
        gameObject.SetActive(true);
        StartCoroutine(PlayAndDisableSparksCoroutine());
    }
    
    IEnumerator PlayAndDisableSparksCoroutine()
    {
        sparksParticleSystem.Emit(30);
        yield return new WaitForSeconds(1f);
        spawner.ReturnToPool(this);
            gameObject.SetActive(false);
    }
    


}