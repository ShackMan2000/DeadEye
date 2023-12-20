 
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[InlineEditor]
[CreateAssetMenu]
public class TimeTrialSettings : ScriptableObject
{
    public List<float> TimeOptions;

    public int selectedTimeIndex;
    
    public float SelectedTime => TimeOptions[selectedTimeIndex];
    
    public int MaxActiveEnemies;
    
    
    // keep this super low, use max enemies for spawning.
    public float SpawnInterval;
    
    
    // max enemies on screen, spawn rate
    // types of enemies. path is kinda difficult? actually not that much, could also polish it by choosing the closest one.
    // might suck a bit with the path and such...
    
}
