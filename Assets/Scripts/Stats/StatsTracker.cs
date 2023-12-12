using System;
using System.Collections.Generic;
using UnityEngine;


public class StatsTracker : MonoBehaviour
{

    [SerializeField] WaveController waveController;

    public StatsPerWave StatsForCurrentWave;

    void OnEnable()
    {
        waveController.OnWaveStarted += OnWaveStarted;
        EnemyBase.OnAnyEnemyDestroyedCorrectly += OnAnyEnemyDestroyedCorrectly;
        EnemyBase.OnMultiDroneShotAtWrongTime += OnMultiDroneShotAtWrongTime;
        Shooter.ShotHitEnemy += OnShotFired;
    }

    void OnDisable()
    {
        waveController.OnWaveStarted -= OnWaveStarted;
        EnemyBase.OnAnyEnemyDestroyedCorrectly -= OnAnyEnemyDestroyedCorrectly;
        EnemyBase.OnMultiDroneShotAtWrongTime -= OnMultiDroneShotAtWrongTime;
        Shooter.ShotHitEnemy -= OnShotFired;
    }

    void OnWaveStarted(int waveIndex)
    {
        StatsForCurrentWave = new StatsPerWave();
        StatsForCurrentWave.WaveIndex = waveIndex;
    }


    void OnAnyEnemyDestroyedCorrectly(EnemySettings enemySettings, bool correctWeapon)
    {
        // first check if already tracking the enemy settings
        // if not, add it to the list
        // if yes, add to the correct counter

        int index = StatsForCurrentWave.StatsPerEnemies.FindIndex(x => x.EnemySettings == enemySettings);

        if (index == -1)
        {
            StatsPerEnemy statsPerEnemy = new StatsPerEnemy();
            statsPerEnemy.EnemySettings = enemySettings;

            StatsForCurrentWave.StatsPerEnemies.Add(statsPerEnemy);
            index = StatsForCurrentWave.StatsPerEnemies.Count - 1;
        }

        if (correctWeapon)
        {
            StatsForCurrentWave.StatsPerEnemies[index].DestroyedCorrectly++;
        }
        else
        {
            StatsForCurrentWave.StatsPerEnemies[index].DestroyedByMistake++;
        }
    }


    void OnMultiDroneShotAtWrongTime(EnemySettings enemySettings)
    {
        int index = StatsForCurrentWave.StatsPerEnemies.FindIndex(x => x.EnemySettings == enemySettings);

        StatsForCurrentWave.StatsPerEnemies[index].DestroyedByMistake++;
    }


    void OnShotFired(bool hitEnemy)
    {
        if (StatsForCurrentWave == null)
        {
            return;
            
        }
        StatsForCurrentWave.ShotsFired++;

        if (hitEnemy)
        {
            StatsForCurrentWave.ShotsHit++;
        }
    }
}


// stats per game, which also includes the day. Could also simply use an int for number of games so they can be ordered.
// might actually be better than dates...

[System.Serializable]
public class StatsPerWave
{
    public int WaveIndex;
    public float ShotsFired;
    public float ShotsHit;

    public List<StatsPerEnemy> StatsPerEnemies = new List<StatsPerEnemy>();
    public float Accuracy
    {
        get
        {
            if (ShotsFired == 0)
                return 0;
            
            return ShotsHit / ShotsFired;
        }
    }
}


[Serializable]
public class StatsPerEnemy
{
    public EnemySettings EnemySettings;
    public float DestroyedCorrectly;
    public float DestroyedByMistake;

    // for multidrone need to know how close the shot was. Actuall figure that out first. 
    // need some kind of system that adjusts which accuracy is correct and not.
    // for back and forth, accuracy is -1 to 1 (1 being side drones closest)
    // for rotation should be first split into -90 to 90. 
    public float ShotCorrectWeaponPercent
    {
        get
        {
            if (DestroyedCorrectly + DestroyedByMistake == 0)
                return 0;
            
            return DestroyedCorrectly / (DestroyedCorrectly + DestroyedByMistake);
        }
    }
}