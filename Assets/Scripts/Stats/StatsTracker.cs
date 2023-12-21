using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class StatsTracker : MonoBehaviour
{
    [SerializeField] WaveController waveController;

    public Stats statsThisRound;

    public List<Stats> StatsForEachWave;


    void OnEnable()
    {
        waveController.OnWaveStarted += OnWaveStarted;

        GameManager.OnStartingNewWaveGame += CreateNewStatsForAllWaves;

        EnemyBase.OnAnySingleEnemyDestroyedCorrectly += OnAnySingleEnemyDestroyedCorrectly;
        Shooter.ShotHitEnemy += OnShotFired;
        MultiDrone.OnMultiDroneShot += OnMultiDroneShot;
        ScoreController.OnScoreChanged += UpdateScore;
    }


    void OnDisable()
    {
        waveController.OnWaveStarted -= OnWaveStarted;

        EnemyBase.OnAnySingleEnemyDestroyedCorrectly -= OnAnySingleEnemyDestroyedCorrectly;
        Shooter.ShotHitEnemy -= OnShotFired;
        MultiDrone.OnMultiDroneShot -= OnMultiDroneShot;
        ScoreController.OnScoreChanged -= UpdateScore;
    }

    void CreateNewStatsForAllWaves()
    {
        StatsForEachWave = new List<Stats>();
    }

    void OnWaveStarted(int waveIndex)
    {
        statsThisRound = new Stats();
        statsThisRound.WaveIndex = waveIndex;
        StatsForEachWave.Add(statsThisRound);
    }


    void OnAnySingleEnemyDestroyedCorrectly(EnemySettings enemySettings, bool correctWeapon)
    {
        int index = statsThisRound.StatsPerSingleEnemies.FindIndex(x => x.EnemySettings == enemySettings);

        if (index == -1)
        {
            StatsPerSingleEnemy statsPerSingleEnemy = new StatsPerSingleEnemy();
            statsPerSingleEnemy.EnemySettings = enemySettings;

            statsThisRound.StatsPerSingleEnemies.Add(statsPerSingleEnemy);
            index = statsThisRound.StatsPerSingleEnemies.Count - 1;
        }

        if (correctWeapon)
        {
            statsThisRound.StatsPerSingleEnemies[index].DestroyedCorrectly++;
        }
        else
        {
            statsThisRound.StatsPerSingleEnemies[index].DestroyedByMistake++;
        }
    }

    void UpdateScore(float score)
    {
        if (statsThisRound == null)
        {
            return;
        }

        statsThisRound.Score = score;
    }

    void OnShotFired(bool hitEnemy)
    {
        if (statsThisRound == null)
        {
            return;
        }

        statsThisRound.ShotsFired++;

        if (hitEnemy)
        {
            statsThisRound.ShotsHit++;
        }
    }


    void OnMultiDroneShot(MultiDroneHitInfo hitInfo)
    {
        int index = statsThisRound.StatsMultiDrones.FindIndex(x => x.EnemySettings == hitInfo.Settings);

        if (index == -1)
        {
            StatsMultiDrone statsMultiDrone = new StatsMultiDrone();
            statsMultiDrone.EnemySettings = hitInfo.Settings;
            statsMultiDrone.rangeForEachShot = new List<float>();

            statsThisRound.StatsMultiDrones.Add(statsMultiDrone);

            index = statsThisRound.StatsMultiDrones.Count - 1;
        }

        statsThisRound.StatsMultiDrones[index].rangeForEachShot.Add(hitInfo.OffsetOnShotRelative);
        statsThisRound.StatsMultiDrones[index].rotationsRelativeWhenShot.Add(hitInfo.RotationRelative);


        // adjust it so the entire rotation is represented as -1 to 1
        if (hitInfo.Settings.SideDronesMovementType == SideDronesMovementType.RotateAround)
        {
            int shotIndex = statsThisRound.StatsMultiDrones[index].rangeForEachShot.Count - 1;

            float shot = statsThisRound.StatsMultiDrones[index].rangeForEachShot[shotIndex];
            float rotation = statsThisRound.StatsMultiDrones[index].rotationsRelativeWhenShot[shotIndex];

            if (shot >= 0f && rotation >= 0.25f)
            {
                statsThisRound.StatsMultiDrones[index].rangeForEachShot[shotIndex] = -shot;
            }
            else if (shot < 0f && rotation <= 0.75)
            {
                statsThisRound.StatsMultiDrones[index].rangeForEachShot[shotIndex] = -shot;
            }

        }
    }


    public Stats GetStatsForAllWavesCombined()
    {
        Stats statsCombined = new Stats();

        statsCombined.WaveIndex = StatsForEachWave.Count - 1;

        foreach (var waveStats in StatsForEachWave)
        {
            statsCombined.ShotsFired += waveStats.ShotsFired;
            statsCombined.ShotsHit += waveStats.ShotsHit;
            statsCombined.Score += waveStats.Score;

            foreach (var statsPerSingleEnemy in waveStats.StatsPerSingleEnemies)
            {
                int index = statsCombined.StatsPerSingleEnemies.FindIndex(x => x.EnemySettings == statsPerSingleEnemy.EnemySettings);

                if (index == -1)
                {
                    StatsPerSingleEnemy statsPerSingleEnemyCombined = new StatsPerSingleEnemy();
                    statsPerSingleEnemyCombined.EnemySettings = statsPerSingleEnemy.EnemySettings;

                    statsCombined.StatsPerSingleEnemies.Add(statsPerSingleEnemyCombined);
                    index = statsCombined.StatsPerSingleEnemies.Count - 1;
                }

                statsCombined.StatsPerSingleEnemies[index].DestroyedCorrectly += statsPerSingleEnemy.DestroyedCorrectly;
                statsCombined.StatsPerSingleEnemies[index].DestroyedByMistake += statsPerSingleEnemy.DestroyedByMistake;
            }

            foreach (var statsMultiDrone in waveStats.StatsMultiDrones)
            {
                int index = statsCombined.StatsMultiDrones.FindIndex(x => x.EnemySettings == statsMultiDrone.EnemySettings);

                if (index == -1)
                {
                    StatsMultiDrone statsMultiDroneCombined = new StatsMultiDrone();
                    statsMultiDroneCombined.EnemySettings = statsMultiDrone.EnemySettings;
                    statsMultiDroneCombined.rangeForEachShot = new List<float>();

                    statsCombined.StatsMultiDrones.Add(statsMultiDroneCombined);

                    index = statsCombined.StatsMultiDrones.Count - 1;
                }

                statsCombined.StatsMultiDrones[index].rangeForEachShot.AddRange(statsMultiDrone.rangeForEachShot);
                statsCombined.StatsMultiDrones[index].rotationsRelativeWhenShot.AddRange(statsMultiDrone.rotationsRelativeWhenShot);
            }
        }


        return statsCombined;
    }
}


[System.Serializable]
public class Stats
{
    public int WaveIndex;

    public float Score;

    public float ShotsFired;
    public float ShotsHit;

    public List<StatsPerSingleEnemy> StatsPerSingleEnemies = new List<StatsPerSingleEnemy>();
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
    
    public enum GameMode
    {
        Waves,
        TimeTrial
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
    public List<float> rangeForEachShot = new List<float>();
    public List<float> rotationsRelativeWhenShot = new List<float>();
}