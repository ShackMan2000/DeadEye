using System;
using System.Collections.Generic;
using Backend;
using UnityEngine;
using UnityEngine.Serialization;


public class StatsTracker : MonoBehaviour
{
    [SerializeField] WaveController waveController;

    // public Stats statsThisRound;

    public StatsSummaryPerGame statsSummaryThisRound;

    public static event Action OnSavedStats = delegate { };

    public static event Action OnNewHighScore = delegate { };
    // public List<Stats> StatsForEachWave;


    void OnEnable()
    {
        GameManager.OnStartingNewWaveGame += CreateNewStats;
        // GameManager.OnWaveGameFinished += SaveStatsWaveGame;
        // as per request, quitting a wave game in the pause menu does not save stats, so failing is the only way right now
        GameManager.OnWaveGameFailed += SaveStatsWaveGame;

        GameManager.OnStartingNewTimeTrialGame += CreateNewStats;
        GameManager.OnTimeTrialSuccess += SaveStatsTimeTrialGame;
        GameManager.OnTimeTrialFailed += SaveStatsTimeTrialGame;

        EnemyBase.OnAnySingleEnemyDestroyedCorrectly += OnAnySingleEnemyDestroyedCorrectly;
        Gun.ShotHitEnemy += OnShotFired;
        MultiDrone.OnMultiDroneShot += OnMultiDroneShot;
        ScoreController.OnScoreChanged += UpdateScore;
    }


    void OnDisable()
    {
        GameManager.OnStartingNewWaveGame -= CreateNewStats;
        GameManager.OnWaveGameFailed -= SaveStatsWaveGame;

        GameManager.OnStartingNewTimeTrialGame -= CreateNewStats;
        GameManager.OnTimeTrialFailed -= SaveStatsTimeTrialGame;
        GameManager.OnTimeTrialSuccess -= SaveStatsTimeTrialGame;

        EnemyBase.OnAnySingleEnemyDestroyedCorrectly -= OnAnySingleEnemyDestroyedCorrectly;
        Gun.ShotHitEnemy -= OnShotFired;
        MultiDrone.OnMultiDroneShot -= OnMultiDroneShot;
        ScoreController.OnScoreChanged -= UpdateScore;
    }


    void CreateNewStats()
    {
        statsSummaryThisRound = new StatsSummaryPerGame();
    }


    void OnAnySingleEnemyDestroyedCorrectly(EnemySettings enemySettings, bool correctWeapon, Vector3 positionToIgnore)
    {
        // int index = statsThisRound.StatsPerSingleEnemies.FindIndex(x => x.EnemySettings == enemySettings);
        //
        // if (index == -1)
        // {
        //     StatsPerSingleEnemy statsPerSingleEnemy = new StatsPerSingleEnemy();
        //     statsPerSingleEnemy.EnemySettings = enemySettings;
        //
        //     statsThisRound.StatsPerSingleEnemies.Add(statsPerSingleEnemy);
        //     index = statsThisRound.StatsPerSingleEnemies.Count - 1;
        // }

        statsSummaryThisRound.redBlueHitsCount++;

        if (correctWeapon)
        {
            statsSummaryThisRound.redBlueHitsCountCorrectWeapon++;
        }
    }

    void UpdateScore(float score)
    {
        if (statsSummaryThisRound == null)
        {
            return;
        }

        statsSummaryThisRound.Score = score;
    }

    void OnShotFired(bool hitEnemy)
    {
        if (statsSummaryThisRound == null)
        {
            return;
        }

        statsSummaryThisRound.ShotsFired++;

        if (hitEnemy)
        {
            statsSummaryThisRound.ShotsHitAnyEnemy++;
        }
    }


    void OnMultiDroneShot(MultiDroneHitInfo hitInfo)
    {
        statsSummaryThisRound.multiDroneHitsCount++;
        // one will be 0...
        //   statsSummaryThisRound.multiDroneHitsAccuracySums += MathF.Abs(hitInfo.RotationRelative);
        statsSummaryThisRound.multiDroneHitsAccuracySums += 1f - hitInfo.OffsetOnShotRelative;

        // int index = statsThisRound.StatsMultiDrones.FindIndex(x => x.EnemySettings == hitInfo.Settings);
        //
        // if (index == -1)
        // {
        //     StatsMultiDrone statsMultiDrone = new StatsMultiDrone();
        //     statsMultiDrone.EnemySettings = hitInfo.Settings;
        //     statsMultiDrone.rangeForEachShot = new List<float>();
        //
        //     statsThisRound.StatsMultiDrones.Add(statsMultiDrone);
        //
        //     index = statsThisRound.StatsMultiDrones.Count - 1;
        // }
        //
        // statsThisRound.StatsMultiDrones[index].rangeForEachShot.Add(hitInfo.OffsetOnShotRelative);
        // statsThisRound.StatsMultiDrones[index].rotationsRelativeWhenShot.Add(hitInfo.RotationRelative);
        //
        //
        // // adjust it so the entire rotation is represented as -1 to 1
        // if (hitInfo.Settings.SideDronesMovementType == SideDronesMovementType.RotateAround)
        // {
        //     int shotIndex = statsThisRound.StatsMultiDrones[index].rangeForEachShot.Count - 1;
        //
        //     float shot = statsThisRound.StatsMultiDrones[index].rangeForEachShot[shotIndex];
        //     float rotation = statsThisRound.StatsMultiDrones[index].rotationsRelativeWhenShot[shotIndex];
        //
        //     if (shot >= 0f && rotation >= 0.25f)
        //     {
        //         statsThisRound.StatsMultiDrones[index].rangeForEachShot[shotIndex] = -shot;
        //     }
        //     else if (shot < 0f && rotation <= 0.75)
        //     {
        //         statsThisRound.StatsMultiDrones[index].rangeForEachShot[shotIndex] = -shot;
        //     }
        // }
    }


    // public Stats GetStatsForAllWavesCombined()
    // {
    //     Stats statsCombined = new Stats();
    //
    //   //  statsCombined.GameIndex = StatsForEachWave.Count - 1;
    //
    //     foreach (var waveStats in StatsForEachWave)
    //     {
    //         statsCombined.ShotsFired += waveStats.ShotsFired;
    //         statsCombined.ShotsHit += waveStats.ShotsHit;
    //         statsCombined.Score += waveStats.Score;
    //
    //         foreach (var statsPerSingleEnemy in waveStats.StatsPerSingleEnemies)
    //         {
    //             int index = statsCombined.StatsPerSingleEnemies.FindIndex(x => x.EnemySettings == statsPerSingleEnemy.EnemySettings);
    //
    //             if (index == -1)
    //             {
    //                 StatsPerSingleEnemy statsPerSingleEnemyCombined = new StatsPerSingleEnemy();
    //                 statsPerSingleEnemyCombined.EnemySettings = statsPerSingleEnemy.EnemySettings;
    //
    //                 statsCombined.StatsPerSingleEnemies.Add(statsPerSingleEnemyCombined);
    //                 index = statsCombined.StatsPerSingleEnemies.Count - 1;
    //             }
    //
    //             statsCombined.StatsPerSingleEnemies[index].DestroyedCorrectly += statsPerSingleEnemy.DestroyedCorrectly;
    //             statsCombined.StatsPerSingleEnemies[index].DestroyedByMistake += statsPerSingleEnemy.DestroyedByMistake;
    //         }
    //
    //         foreach (var statsMultiDrone in waveStats.StatsMultiDrones)
    //         {
    //             int index = statsCombined.StatsMultiDrones.FindIndex(x => x.EnemySettings == statsMultiDrone.EnemySettings);
    //
    //             if (index == -1)
    //             {
    //                 StatsMultiDrone statsMultiDroneCombined = new StatsMultiDrone();
    //                 statsMultiDroneCombined.EnemySettings = statsMultiDrone.EnemySettings;
    //                 statsMultiDroneCombined.rangeForEachShot = new List<float>();
    //
    //                 statsCombined.StatsMultiDrones.Add(statsMultiDroneCombined);
    //
    //                 index = statsCombined.StatsMultiDrones.Count - 1;
    //             }
    //
    //             statsCombined.StatsMultiDrones[index].rangeForEachShot.AddRange(statsMultiDrone.rangeForEachShot);
    //             statsCombined.StatsMultiDrones[index].rotationsRelativeWhenShot.AddRange(statsMultiDrone.rotationsRelativeWhenShot);
    //         }
    //     }
    //
    //
    //     return statsCombined;
    // }


    void SaveStatsWaveGame()
    {
        SaveStatsSummary(true);
    }

    void SaveStatsTimeTrialGame()
    {
        SaveStatsSummary(false);
    }


    void SaveStatsSummary(bool isWaveGame)
    {
        // StatsSummaryPerGame statsSummaryPerGame = new StatsSummaryPerGame();
        // statsSummaryPerGame.AccuracyPerEnemy = new List<AccuracyPerEnemy>();
        //
        // Stats stats = statsThisRound;
        //
        // statsSummaryPerGame.Score = stats.Score;
        // statsSummaryPerGame.AccuracyAllShots = stats.Accuracy;
        //
        // foreach (var statsPerSingleEnemy in stats.StatsPerSingleEnemies)
        // {
        //     AccuracyPerEnemy accuracyPerEnemy = new AccuracyPerEnemy();
        //
        //     accuracyPerEnemy.GUID = statsPerSingleEnemy.EnemySettings.GUID;
        //     accuracyPerEnemy.Accuracy = statsPerSingleEnemy.ShotCorrectWeaponPercent;
        //
        //     statsSummaryPerGame.AccuracyPerEnemy.Add(accuracyPerEnemy);
        // }
        //
        // foreach (var statsMultiDrone in stats.StatsMultiDrones)
        // {
        //     AccuracyPerEnemy accuracyPerEnemy = new AccuracyPerEnemy();
        //
        //     accuracyPerEnemy.GUID = statsMultiDrone.EnemySettings.GUID;
        //     accuracyPerEnemy.Accuracy = statsMultiDrone.rangeForEachShot.Count / (float)statsMultiDrone.rotationsRelativeWhenShot.Count;
        //
        //     statsSummaryPerGame.AccuracyPerEnemy.Add(accuracyPerEnemy);
        // }


        if (isWaveGame)
        {
            SaveManager.Instance.GetSaveData().StatsForWaveGames.Add(statsSummaryThisRound);
            if (statsSummaryThisRound.Score > SaveManager.Instance.GetSaveData().HighScoreWaves)
            {
                SaveManager.Instance.GetSaveData().HighScoreWaves = statsSummaryThisRound.Score;
                OnNewHighScore?.Invoke();
            }
        }
        else
        {
            SaveManager.Instance.GetSaveData().StatsForTimeTrialGames.Add(statsSummaryThisRound);

            if (statsSummaryThisRound.Score > SaveManager.Instance.GetSaveData().HighScoreTimeTrial)
            {
                SaveManager.Instance.GetSaveData().HighScoreTimeTrial = statsSummaryThisRound.Score;
                OnNewHighScore?.Invoke();
            }
        }

        SaveManager.Instance.WriteSaveData();

        OnSavedStats?.Invoke();
    }
}


[System.Serializable]
public class Stats
{
    //  public int GameIndex;

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