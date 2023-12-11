using System;
using UnityEngine;


public class ScoreController : MonoBehaviour
{
    public float Score;
    public int KillStreak;

    [SerializeField] ScoreSettings scoreSettings;


    [SerializeField] ScoreMulti currentMulti;

    public event Action<float> OnScoreChanged = delegate { };
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
        EnemyBase.OnMultiDroneShotAtWrongTime += OnMultiDroneShotAtWrongTime;
    }

    void OnDisable()
    {
        EnemyBase.OnAnyEnemyDestroyedCorrectly -= OnAnyEnemyDestroyedCorrectly;
        EnemyBase.OnMultiDroneShotAtWrongTime -= OnMultiDroneShotAtWrongTime;
    }

    void OnAnyEnemyDestroyedCorrectly(EnemySettings enemySettings, bool correctWeapon)
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

    void OnMultiDroneShotAtWrongTime(EnemySettings enemySettings)
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