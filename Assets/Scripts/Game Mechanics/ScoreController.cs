using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class ScoreController : MonoBehaviour
{


    public int Score;
    public int KillStreak;
        
    [SerializeField] ScoreSettings scoreSettings;
    
   
    [SerializeField] ScoreMulti currentMulti;
    
    public event Action<int> OnScoreChanged = delegate { };
    public event Action<int> OnKillStreakChanged = delegate { };


    // make this as reaction to on wave started
    void Start()
    {
        Score = 0;
        KillStreak = 0;
        OnScoreChanged(Score);
        OnKillStreakChanged(KillStreak);
        currentMulti = scoreSettings.ScoreMultipliers[0];
        
        OnKillStreakChanged?.Invoke(KillStreak);
    }

    void OnEnable()
    {
        EnemyBase.OnAnyEnemyDestroyedCorrectly += OnAnyEnemyDestroyedCorrectly;
        EnemyBase.OnAnyEnemyShotByMistake += OnAnyEnemyShotByMistake;
    }

    void OnDisable()
    {
        EnemyBase.OnAnyEnemyDestroyedCorrectly -= OnAnyEnemyDestroyedCorrectly;
        EnemyBase.OnAnyEnemyShotByMistake -= OnAnyEnemyShotByMistake;
    }

    void OnAnyEnemyDestroyedCorrectly(EnemySettings enemySettings)
    {
        KillStreak++;
        Score++;
        
        UpdateScoreMulti();
        OnScoreChanged(Score);
        OnKillStreakChanged(KillStreak);
    }

    void OnAnyEnemyShotByMistake(EnemySettings enemySettings)
    {
        KillStreak = 0;
        UpdateScoreMulti();
        
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