using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class EnemiesShootingManager : MonoBehaviour
{

    // [SerializeField] WaveController waveController;
    //
    // [SerializeField] WaveSettings settings;
    //
    // float shootingIntervallCurrentWave;
    // float timeTillNextShot;
    //
    // bool isWaveActive;
    //
    // EnemyBase lastEnemyThatShot;
    //
    // void OnEnable()
    // {
    //     waveController.OnWaveStarted += OnWaveStarted;
    //     //waveController.OnWaveCompleted += OnWaveCompleted;
    //     GameManager.OnExitGameMode += OnWaveFinished;
    // }
    //
    // void OnDisable()
    // {
    //     waveController.OnWaveStarted -= OnWaveStarted;
    //     //waveController.OnWaveCompleted -= OnWaveCompleted;
    //     GameManager.OnExitGameMode -= OnWaveFinished;
    // }
    //
    // void OnWaveStarted(int waveIndex)
    // {
    //     shootingIntervallCurrentWave = settings.ShotIntervalBase - (settings.ShotIntervalDecreasePerLevel * waveIndex);
    //     shootingIntervallCurrentWave = Mathf.Clamp(shootingIntervallCurrentWave, settings.ShotIntervalMin, shootingIntervallCurrentWave);
    //     
    //     SetTimerForNextShot();
    //     
    //     isWaveActive = true;
    // }
    // // needs to know which enemies are active
    // // should probably save the last enemy that has shot to not have the same one shoot twice
    //
    //
    // void SetTimerForNextShot()
    // {
    //     float variance = UnityEngine.Random.Range(1f - settings.ShotIntervalVarianceRelative, 1f + settings.ShotIntervalVarianceRelative);
    //     timeTillNextShot = shootingIntervallCurrentWave * variance;
    // }
    //
    //
    // void Update()
    // {
    //     if (!isWaveActive)
    //     {
    //         return;
    //     }
    //
    //     timeTillNextShot -= Time.deltaTime;
    //     if (timeTillNextShot <= 0)
    //     {
    //         SetTimerForNextShot();
    //         Shoot();
    //     }
    // }
    //
    //
    // // use enemy spawner, that way works for time trial and 
    // void Shoot()
    // {
    //     List<EnemyBase> shootingEnemies = new List<EnemyBase>();
    //     foreach (var enemyList in waveController.activeEnemies)
    //     {
    //         foreach (var enemy in enemyList.Value)
    //         {
    //             if (enemy.CanShoot)
    //             {
    //                 shootingEnemies.Add(enemy);
    //             }
    //         }
    //     }
    //     
    //     if(shootingEnemies.Count > 1 && lastEnemyThatShot != null && shootingEnemies.Contains(lastEnemyThatShot))
    //     {
    //         shootingEnemies.Remove(lastEnemyThatShot);
    //     }
    //     
    //     if(shootingEnemies.Count == 0)
    //     {
    //         return;
    //     }
    //     
    //     lastEnemyThatShot = shootingEnemies[UnityEngine.Random.Range(0, shootingEnemies.Count)];
    //     lastEnemyThatShot.ShootAtPlayer();
    //     
    //     // first get list of all enemies that can shoot.
    //     // if it is 0, return...
    //     // if it is more than one, remove the one that shot last (if it's on there), and pick a random one
    //     
    //     
    // }
    //
    //
    // void OnWaveFinished()
    // {
    //     isWaveActive = false;
    // }

}