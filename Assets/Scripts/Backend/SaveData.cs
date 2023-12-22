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
    public List<AccuracyPerEnemy> AccuraciesPerWaveGame = new List<AccuracyPerEnemy>();
    public List<AccuracyPerEnemy> AccuraciesPerTimeTrialGame = new List<AccuracyPerEnemy>();

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



[System.Serializable]
public class AccuracyPerEnemy
{
    public string GUID;
    public List<AccuracyEntry> AccuracyEntries = new List<AccuracyEntry>();
}
// don't really have accuracy for left and right enemies. 
// only have total accuracy (which will use an enemy setting SO)
// could use if used correct weapon. Could also be smart and simplify and only show the depth drones for now...

// the idea is that this can be per game but also per wave
// this list will always be sorted by index by default, only thing is that some entries might be missing, at least for the ones per game
// so stats controller should be able to create one as well for all current waves.

[System.Serializable]
public class AccuracyEntry
{
    public int Index;
    public float Accuracy;
}