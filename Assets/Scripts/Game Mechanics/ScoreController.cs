using System;
using System.Collections.Generic;
using UnityEngine;


public class ScoreController : MonoBehaviour
{


    public int Score;
    public int KillStreak;
    
    public event Action<int> OnScoreChanged = delegate { };
    public event Action<int> OnKillStreakChanged = delegate { };

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
        OnScoreChanged(Score);
        OnKillStreakChanged(KillStreak);
    }

    void OnAnyEnemyShotByMistake(EnemySettings enemySettings)
    {
        KillStreak = 0;
        OnKillStreakChanged(KillStreak);
    }
    
}