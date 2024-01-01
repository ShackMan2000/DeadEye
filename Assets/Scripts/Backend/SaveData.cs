using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;


[Serializable]
public class SaveData
{
    public event Action OnSaveDataLoaded = delegate { };
    
    public List<StatsSummaryPerGame> StatsForWaveGames =   new List<StatsSummaryPerGame>();
    public List<StatsSummaryPerGame> StatsForTimeTrialGames = new List<StatsSummaryPerGame>();
   
  
    public bool TutorialCompleted;

    public string LastSaveTimeStampString;
    // public float TotalPlayTime;


    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
        OnSaveDataLoaded?.Invoke();
    }

    public void Clear()
    {
        Debug.Log("Clearing Save Data");

        TutorialCompleted = false;
    }
}








[Serializable]
public class StatsSummaryPerGame
{
    public float Score;
    public float Accuracy;

    public List<AccuracyPerEnemy> AccuracyPerEnemy = new List<AccuracyPerEnemy>();
}



[Serializable]
public class AccuracyPerEnemy
{
    public string GUID;
    
    // use GUID to be safe, but when the data is loaded set enemy settings
    public EnemySettings EnemySettings;
    public float Accuracy;
}