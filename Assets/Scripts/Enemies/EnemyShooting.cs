using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyShooting : MonoBehaviour
{
    [SerializeField] EnemyBase enemyBase;
    [SerializeField] List<Renderer> gunRenderer;


    [SerializeField] Transform bulletSpawnPoint;

    [SerializeField] GameSettings gameSettings;

    EnemySettings settings => enemyBase.Settings;

    static float globalTimeLastShot;

    [SerializeField] PlayerPosition playerPosition;
    static readonly int Glow = Shader.PropertyToID("_Glow");

    void OnEnable()
    {
        enemyBase.OnInitialized += InitializeShooting;

        foreach (Renderer r in gunRenderer)
        {
            r.material.SetFloat(Glow, 0f);
        }
    }

    void OnDisable()
    {
        enemyBase.OnInitialized -= InitializeShooting;
    }

    void InitializeShooting()
    {
        StopAllCoroutines();
        StartCoroutine(ShootAtPlayerCoroutine());
    }


    IEnumerator ShootAtPlayerCoroutine()
    {
        float timeUntilShooting = Random.Range(settings.ShootingInterval.x, settings.ShootingInterval.y);

        yield return new WaitForSeconds(timeUntilShooting);

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
    }
}