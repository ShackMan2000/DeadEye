 
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[InlineEditor]
[CreateAssetMenu(menuName = "WaveSettings")]
public class WaveSettings : ScriptableObject
{

    public float EnemySpawnIntervalBase;
    public float EnemySpawnIntervalDecreasePerLevel;
    public float EnemySpawnIntervalMin;
    [Range(0f, 0.9f)] [InfoBox("0.1 means anything between 10% lower or 10% higher")]
    public float EnemySpawnIntervalVarianceRelative;

    public List<SpawnSettings> AllEnemiesOptions;

    public float ShotIntervalBase;
    public float ShotIntervalDecreasePerLevel;
    public float ShotIntervalMin;
    [Range(0f, 0.9f)] [InfoBox("0.1 means anything between 10% lower or 10% higher")]
    public float ShotIntervalVarianceRelative;

}

[System.Serializable]
public class SpawnSettings
{
    public EnemySettings EnemySettings;

    public EnemyBase EnemyPrefabNeutral;
    public EnemyBase EnemyPrefabLeft;
    public EnemyBase EnemyPrefabRight;
    
    public int MinimumWaveLevel;
    
    public int SpawnAmountBase;
    public int SpawnAmountIncreasePerLevel;

    public int SpawnCountLeftCurrentWave;
}
