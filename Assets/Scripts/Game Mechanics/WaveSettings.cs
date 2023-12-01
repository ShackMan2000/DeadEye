 
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[InlineEditor]
[CreateAssetMenu(menuName = "WaveSettings")]
public class WaveSettings : ScriptableObject
{

    public int EnemyCountBase;
    public int EnemyCountIncreasePerLevel;
    public int EnemyCountMax;

    public float EnemySpawnIntervalBase;
    public float EnemySpawnIntervalDecreasePerLevel;
    public float EnemySpawnIntervalMin;
    [Range(0f, 0.9f)] [InfoBox("0.1 means anything between 10% lower or 10% higher")]
    public float EnemySpawnIntervalVarianceRelative;
    

    public List<PrefabsWithSettingsOptions> AllEnemiesWithSettingsOptions;
    // maybe a helper list that sorts them by wave level


    public float ShotIntervalBase;
    public float ShotIntervalDecreasePerLevel;
    public float ShotIntervalMin;
    [Range(0f, 0.9f)] [InfoBox("0.1 means anything between 10% lower or 10% higher")]
    public float ShotIntervalVarianceRelative;

    
    // could also add a float for increasing the speed of all enemies with each wave
    
  


    // public bool IsEnemyAvailable(EnemySettings enemySettings, int level)
    // {
    //     
    //     foreach (var enemy in AllEnemies)
    //     {
    //         if (enemy.EnemySettings == enemySettings)
    //         {
    //             return enemy.Level <= level;
    //         }
    //     }
    //
    //     Debug.LogError("Enemy not found in WaveSettings");
    //     return false;
    //     
    // }
}

[System.Serializable]
public class PrefabsWithSettingsOptions
{
    public EnemyBase EnemyPrefab;
    public List<EnemySettings> SettingsOptions;
}
