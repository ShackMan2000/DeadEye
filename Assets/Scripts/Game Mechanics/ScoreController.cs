using System;
using UnityEngine;


public class ScoreController : MonoBehaviour
{
    public float Score;
    public int KillStreak;

    [SerializeField] ScoreSettings scoreSettings;


    [SerializeField] ScoreMulti currentMulti;

    public static event Action<float> OnScoreChanged = delegate { };
    public static event Action<int> OnKillStreakChanged = delegate { };

    
    
    
    void OnEnable()
    {
        EnemyBase.OnAnySingleEnemyDestroyedCorrectly += OnAnySingleEnemyDestroyedCorrectly;
        MultiDrone.OnMultiDroneShot += OnMultiDroneShot;
        
        GameManager.OnStartingNewWaveGame += ResetScore;
        GameManager.OnStartingNewTimeTrialGame += ResetScore;
    }

    void OnDisable()
    {
        EnemyBase.OnAnySingleEnemyDestroyedCorrectly -= OnAnySingleEnemyDestroyedCorrectly;
        MultiDrone.OnMultiDroneShot -= OnMultiDroneShot;
        
        GameManager.OnStartingNewWaveGame -= ResetScore;
        GameManager.OnStartingNewTimeTrialGame -= ResetScore;
    }

    
 

    
    void ResetScore()
    {
        Score = 0;
        KillStreak = 0;
        currentMulti = scoreSettings.ScoreMultipliers[0];
        
        OnScoreChanged(Score);
        OnKillStreakChanged(KillStreak);
    }
    
  

    void OnMultiDroneShot(MultiDroneHitInfo hitInfo)
    {
        if (hitInfo.IsCorrectRange)
        {
            KillStreak++;
            Score += hitInfo.Settings.pointsForKill;
            OnScoreChanged(Score);
        }
        else
        {
            KillStreak = 0;
        }

        UpdateScoreMulti();

        OnKillStreakChanged(KillStreak);
    }

    void OnAnySingleEnemyDestroyedCorrectly(EnemySettings enemySettings, bool correctWeapon)
    {
        if (correctWeapon)
        {
            KillStreak++;
            Score += enemySettings.pointsForKill;
        }
        else
        {
            KillStreak = 0;
        }

        UpdateScoreMulti();
        OnScoreChanged(Score);
        OnKillStreakChanged(KillStreak);
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