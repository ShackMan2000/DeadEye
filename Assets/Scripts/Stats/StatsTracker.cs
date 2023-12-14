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
        EnemyBase.OnAnySingleEnemyDestroyedCorrectly += OnAnySingleEnemyDestroyedCorrectly;
        Shooter.ShotHitEnemy += OnShotFired;
        MultiDrone.OnMultiDroneShot += OnMultiDroneShot;
    }

    void OnDisable()
    {
        waveController.OnWaveStarted -= OnWaveStarted;
        EnemyBase.OnAnySingleEnemyDestroyedCorrectly -= OnAnySingleEnemyDestroyedCorrectly;
        Shooter.ShotHitEnemy -= OnShotFired;
        MultiDrone.OnMultiDroneShot -= OnMultiDroneShot;
    }

    void OnWaveStarted(int waveIndex)
    {
        StatsForCurrentWave = new StatsPerWave();
        StatsForCurrentWave.WaveIndex = waveIndex;
    }


    void OnAnySingleEnemyDestroyedCorrectly(EnemySettings enemySettings, bool correctWeapon)
    {
        int index = StatsForCurrentWave.StatsPerEnemies.FindIndex(x => x.EnemySettings == enemySettings);

        if (index == -1)
        {
            StatsPerSingleEnemy statsPerSingleEnemy = new StatsPerSingleEnemy();
            statsPerSingleEnemy.EnemySettings = enemySettings;

            StatsForCurrentWave.StatsPerEnemies.Add(statsPerSingleEnemy);
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


    void OnMultiDroneShot(MultiDroneHitInfo hitInfo)
    {
        int index = StatsForCurrentWave.StatsMultiDrones.FindIndex(x => x.EnemySettings == hitInfo.Settings);

        if (index == -1)
        {
            StatsMultiDrone statsMultiDrone = new StatsMultiDrone();
            statsMultiDrone.EnemySettings = hitInfo.Settings;
            statsMultiDrone.RangeToDestroyRelative = hitInfo.OffsetMaxToDestroy;
            statsMultiDrone.rangeForEachShot = new List<float>();

            StatsForCurrentWave.StatsMultiDrones.Add(statsMultiDrone);

            index = StatsForCurrentWave.StatsMultiDrones.Count - 1;
        }

        StatsForCurrentWave.StatsMultiDrones[index].rangeForEachShot.Add(hitInfo.OffsetOnShot);
        StatsForCurrentWave.StatsMultiDrones[index].rotationsRelativeWhenShot.Add(hitInfo.RotationRelative);
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

    public List<StatsPerSingleEnemy> StatsPerEnemies = new List<StatsPerSingleEnemy>();
    public List<StatsMultiDrone> StatsMultiDrones = new List<StatsMultiDrone>();

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
public class StatsPerSingleEnemy
{
    public EnemySettings EnemySettings;
    public float DestroyedCorrectly;
    public float DestroyedByMistake;

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


[Serializable]
public class StatsMultiDrone
{
    public EnemySettings EnemySettings;
    public float RangeToDestroyRelative;
    public List<float> rangeForEachShot = new List<float>();
    public List<float> rotationsRelativeWhenShot = new List<float>();
}