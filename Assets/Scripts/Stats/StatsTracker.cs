using System;
using System.Collections.Generic;
using Backend;
using UnityEngine;
using UnityEngine.Serialization;


public class StatsTracker : MonoBehaviour
{
    [SerializeField] WaveController waveController;

    public Stats statsThisRound;

    public List<Stats> StatsForEachWave;

    void OnEnable()
    {
        GameManager.OnStartingNewWaveGame += CreateNewStatsForAllWaves;
        GameManager.OnStartingWave += OnWaveStarted;

        EnemyBase.OnAnySingleEnemyDestroyedCorrectly += OnAnySingleEnemyDestroyedCorrectly;
        Shooter.ShotHitEnemy += OnShotFired;
        MultiDrone.OnMultiDroneShot += OnMultiDroneShot;
        ScoreController.OnScoreChanged += UpdateScore;
    }


    void OnDisable()
    {
        GameManager.OnStartingNewWaveGame -= CreateNewStatsForAllWaves;
        GameManager.OnStartingWave -= OnWaveStarted;

        EnemyBase.OnAnySingleEnemyDestroyedCorrectly -= OnAnySingleEnemyDestroyedCorrectly;
        Shooter.ShotHitEnemy -= OnShotFired;
        MultiDrone.OnMultiDroneShot -= OnMultiDroneShot;
        ScoreController.OnScoreChanged -= UpdateScore;
    }

    void CreateNewStatsForAllWaves()
    {
        StatsForEachWave = new List<Stats>();
    }

    void OnWaveStarted()
    {
        statsThisRound = new Stats();

        // first wave is 0
        statsThisRound.WaveIndex = StatsForEachWave.Count;
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


    public void SaveStatsSummary(bool isWaveGame)
    {
        StatsSummaryPerGame statsSummaryPerGame = new StatsSummaryPerGame();
        statsSummaryPerGame.AccuracyPerEnemy = new List<AccuracyPerEnemy>();

        Stats stats = GetStatsForAllWavesCombined();
        
        statsSummaryPerGame.Score = stats.Score;
        statsSummaryPerGame.Accuracy = stats.Accuracy;
        
        foreach (var statsPerSingleEnemy in stats.StatsPerSingleEnemies)
        {
            AccuracyPerEnemy accuracyPerEnemy = new AccuracyPerEnemy();
            
            accuracyPerEnemy.GUID = statsPerSingleEnemy.EnemySettings.GUID;
            accuracyPerEnemy.Accuracy = statsPerSingleEnemy.ShotCorrectWeaponPercent;

            statsSummaryPerGame.AccuracyPerEnemy.Add(accuracyPerEnemy);
        }
        
        foreach (var statsMultiDrone in stats.StatsMultiDrones)
        {
            AccuracyPerEnemy accuracyPerEnemy = new AccuracyPerEnemy();
            
            accuracyPerEnemy.GUID = statsMultiDrone.EnemySettings.GUID;
            accuracyPerEnemy.Accuracy = statsMultiDrone.rangeForEachShot.Count / (float)statsMultiDrone.rotationsRelativeWhenShot.Count;

            statsSummaryPerGame.AccuracyPerEnemy.Add(accuracyPerEnemy);
        }


        if (isWaveGame)
        {
            SaveManager.Instance.GetSaveData().StatsForWaveGames.Add(statsSummaryPerGame);
        }
        else
        {
            SaveManager.Instance.GetSaveData().StatsForTimeTrialGames.Add(statsSummaryPerGame);
        }
        
        SaveManager.Instance.WriteSaveData();
    }
}

// stats need to be saved every time
// a wave game fails
// a wave game is quit (inbetween waves) bc of that would be good to add some extra functionality for the button, not just switch ui
// a time trial succeeds
// a time trial fails


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