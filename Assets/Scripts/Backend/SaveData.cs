using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;


[InlineEditor]
[Serializable]
public class SaveData : ScriptableObject
{
    public event Action OnSaveDataLoaded = delegate { };
    
    public List<StatsSummary> StatsForWaveGames =   new List<StatsSummary>();
    public List<StatsSummary> StatsForTimeTrialGames = new List<StatsSummary>();
   
  
     
     
     
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
public class StatsSummary
{
    public int index;

   public List<AccuracyPerEnemy> AccuracyPerEnemy = new List<AccuracyPerEnemy>();
}


[Serializable]
public class AccuracyPerEnemy
{
    public string GUID;
    public float Accuracy;
}



