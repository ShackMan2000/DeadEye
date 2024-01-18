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

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip chargingSound;
    [SerializeField] AudioClip blastSound;


    // first figure out how long that charging sound should play
    // which means have to initialize the shooting quite early
    // when it's time to shoot, change the sound clip to blast. That's all, the swooshing sound is in the bullet

    EnemySettings settings => enemyBase.Settings;

    static float globalTimeLastShot;

    [SerializeField] PlayerPosition playerPosition;
    static readonly int Glow = Shader.PropertyToID("_Glow");

    void OnEnable()
    {
        enemyBase.OnInitialized += InitializeShooting;
        GameManager.OnGamePaused += PauseSound;
        GameManager.OnGameResumed += ResumeSound;

        foreach (Renderer r in gunRenderer)
        {
            r.material.SetFloat(Glow, 0f);
        }
        
                audioSource.Stop();
    }

    void OnDisable()
    {
        enemyBase.OnInitialized -= InitializeShooting;
        GameManager.OnGamePaused -= PauseSound;
        GameManager.OnGameResumed -= ResumeSound;
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

        audioSource.clip = chargingSound;
        audioSource.Play();


        foreach (Renderer r in gunRenderer)
        {
            r.material.SetFloat(Glow, 0.8f);
        }

        yield return new WaitForSeconds(warningTime);
        yield return new WaitUntil(() => !GameManager.IsPaused);

        if (enemyBase.Settings.BulletPrefab != null)
        {
            EnemyBullet bullet = Instantiate(enemyBase.Settings.BulletPrefab);
            bullet.Initialize(bulletSpawnPoint.position, playerPosition.Position);
        }
        
        audioSource.clip = blastSound;
        audioSource.Play();


        foreach (Renderer r in gunRenderer)
        {
            r.material.SetFloat(Glow, 0f);
        }

        if (gameObject.activeSelf)
        {
            StartCoroutine(ShootAtPlayerCoroutine());
        }
    }


    void PauseSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        
    }
    
    void ResumeSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }
}