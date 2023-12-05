using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[InlineEditor]
[CreateAssetMenu(fileName = "ScoreSettings", menuName = "GameMechanics/ScoreSettings")]
public class ScoreSettings : ScriptableObject
{
    
    public List<ScoreMulti> ScoreMultipliers = new List<ScoreMulti>();

    

    public ScoreMulti GetScoreMultiByKillStreak(int killStreak)
    {
        ScoreMulti currentMulti = ScoreMultipliers[0];
        
        for(int i = 0; i < ScoreMultipliers.Count; i++)
        {
            if (killStreak >= ScoreMultipliers[i].KillStreakNeeded)
            {
                currentMulti = ScoreMultipliers[i];
            }
            else
            {
                break;
            }
        }
        
        return currentMulti;
    }
    
    
    public bool IsHighestMulti(ScoreMulti currentMulti)
    {
        int index = ScoreMultipliers.IndexOf(currentMulti);
        return index == ScoreMultipliers.Count - 1;
    }



    public int GetKillStreakForNextMulti(ScoreMulti currentMulti)
    {
        if(IsHighestMulti(currentMulti))
        {
            return -1;
        }
        
        int index = ScoreMultipliers.IndexOf(currentMulti);
     
        return ScoreMultipliers[index + 1].KillStreakNeeded;
    }
    
    


}
    [System.Serializable]
    public class ScoreMulti
    {
        public int ScoreMultiplier;
        public int KillStreakNeeded;
        public Color Color;
    }
