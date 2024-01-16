using System;
using UnityEngine;


public class ScoreController : MonoBehaviour
{
    public float Score;
    public int KillStreak;

    [SerializeField] ScoreSettings scoreSettings;


    [SerializeField] PlayerHealth playerHealth;

    [SerializeField] ScoreMulti currentMulti;

    public static event Action<float> OnScoreChanged = delegate { };
    public static event Action<int> OnKillStreakChanged = delegate { };

    public static event Action<float, Vector3> OnPointsForKillAwarded = delegate { };


    void OnEnable()
    {
        EnemyBase.OnAnySingleEnemyDestroyedCorrectly += OnSingleEnemyDestroyed;
        MultiDrone.OnMultiDroneShot += CheckToResetScoreMulti;
        MultiDrone.OnMultiDroneDestroyed += AwardMultiDronePoints;

        GameManager.OnStartingNewWaveGame += ResetScore;
        GameManager.OnStartingNewTimeTrialGame += ResetScore;

        playerHealth.OnPlayerHitByBullet += ResetKillStreakAndMulti;
    }

    void OnDisable()
    {
        EnemyBase.OnAnySingleEnemyDestroyedCorrectly -= OnSingleEnemyDestroyed;
        MultiDrone.OnMultiDroneShot -= CheckToResetScoreMulti;
        MultiDrone.OnMultiDroneDestroyed -= AwardMultiDronePoints;

        GameManager.OnStartingNewWaveGame -= ResetScore;
        GameManager.OnStartingNewTimeTrialGame -= ResetScore;

        playerHealth.OnPlayerHitByBullet -= ResetKillStreakAndMulti;
    }


    void ResetScore()
    {
        Score = 0;
        KillStreak = 0;
        currentMulti = scoreSettings.ScoreMultipliers[0];

        OnScoreChanged(Score);
        OnKillStreakChanged(KillStreak);
    }


    void ResetKillStreakAndMulti()
    {
        KillStreak = 0;
        currentMulti = scoreSettings.ScoreMultipliers[0];
        OnKillStreakChanged(KillStreak);
    }


    void CheckToResetScoreMulti(MultiDroneHitInfo hitInfo)
    {
        if (!hitInfo.IsCorrectRange)
        {
            KillStreak = 0;
            UpdateScoreMulti();
            OnKillStreakChanged(KillStreak);
        }
    }


    void AwardMultiDronePoints(MultiDroneHitInfo hitInfo)
    {
        if (hitInfo.IsCorrectRange)
        {
            float pointsForKill = hitInfo.Settings.pointsForKill * currentMulti.ScoreMultiplier;
            Score += pointsForKill;
            OnPointsForKillAwarded(pointsForKill, hitInfo.Position);
            
            KillStreak++;
            UpdateScoreMulti();
            OnScoreChanged(Score);
        }
    }


    void OnSingleEnemyDestroyed(EnemySettings enemySettings, bool correctWeapon, Vector3 position)
    {
        if (correctWeapon)
        {
            float pointsForKill = enemySettings.pointsForKill * currentMulti.ScoreMultiplier;
            Score += pointsForKill;
            OnPointsForKillAwarded(pointsForKill, position);
            KillStreak++;
        }
        else
        {
            KillStreak = 0;
        }

        UpdateScoreMulti();
        OnScoreChanged(Score);
        OnKillStreakChanged(KillStreak);
        // getting a bit messy but easiest for now to get position too
    }


    void UpdateScoreMulti()
    {
        ScoreMulti nextMulti = scoreSettings.GetScoreMultiByKillStreak(KillStreak);

        if (nextMulti != currentMulti)
        {
            currentMulti = nextMulti;
        }
    }
}