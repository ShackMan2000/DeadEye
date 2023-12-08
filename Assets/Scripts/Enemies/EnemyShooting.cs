using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class EnemyShooting : MonoBehaviour
{
    [SerializeField] EnemyBase enemyBase;
    [SerializeField] List<Renderer> gunRenderer;
    
    [SerializeField] Transform bulletSpawnPoint;


    [SerializeField] PlayerPosition playerPosition;
    static readonly int Glow = Shader.PropertyToID("_Glow");

    void OnEnable()
    {
        enemyBase.OnShootAtPlayer += ShootAtPlayer;

        foreach (Renderer r in gunRenderer)
        {
            r.material.SetFloat(Glow, 0f);
        }
    }
    
    void OnDisable()
    {
        enemyBase.OnShootAtPlayer -= ShootAtPlayer;
    }
    
    
    [Button]
    void ShootAtPlayer()
    {
        StartCoroutine(ShootAtPlayerCoroutine());
    }
    
    // shoot at player coroutine
    
    IEnumerator ShootAtPlayerCoroutine()
    {
        float warningTime = enemyBase.Settings.ShootWarningTime;
            
        foreach (Renderer r in gunRenderer)
        {
            r.material.SetFloat(Glow, 1f);
        }        
        yield return new WaitForSeconds(warningTime);

        if (enemyBase.Settings.BulletPrefab != null)
        {
            EnemyBullet bullet = Instantiate(enemyBase.Settings.BulletPrefab);
            bullet.Initialize(bulletSpawnPoint.position, playerPosition.Position);
        }
        
        
        foreach (Renderer r in gunRenderer)
        {
            r.material.SetFloat(Glow, 0f);
        }
        Debug.Log("Shoot at player routine finished");
    }
    
    
}