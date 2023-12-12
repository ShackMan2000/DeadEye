using System.Collections.Generic;
using UnityEngine;


public class StatsDisplay : MonoBehaviour
{

    [SerializeField] StatsTracker statsTracker;

    // could just have settings in the enemy stats display already, including the SO for correct weapon
    
    
    [SerializeField] List<EnemyStatsDisplay> enemyStatsDisplays;
    
    
    
    public void ShowStatsForLastWave(StatsPerWave stats)
    {
        for (int i = 0; i < stats.StatsPerEnemies.Count; i++)
        {
            
            
            enemyStatsDisplays[i].InjectStats(stats.StatsPerEnemies[i]);
        }
    }
    
    // need to group the linger and shooting enemy. just do it manually for now, might have to be removed.

}


