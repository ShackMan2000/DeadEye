using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{

    public int MaxHealth = 3;

    public float MinIntervalBetweenEnemyShots = 1f;
    
    
    public List<EnemySettings> AllEnemySettings;

    
    
    public EnemySettings GetEnemySettings(string guid)
    {
        return AllEnemySettings.Find(enemy => enemy.GUID == guid);
    }


    void OnValidate()
    {
        List<string> guids = new List<string>();
        foreach (var enemySettings in AllEnemySettings)
        {
            if (guids.Contains(enemySettings.GUID))
            {
                Debug.LogError("Duplicate GUID: " + enemySettings.GUID);
            }
            else
            {
                guids.Add(enemySettings.GUID);
            }
        }
    }
}