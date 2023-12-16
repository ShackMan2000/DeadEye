using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class StatsDisplay : MonoBehaviour
{
    [SerializeField] StatsTracker statsTracker;
    [SerializeField] ScoreController scoreController;


    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI shotsFiredText;
    [SerializeField] TextMeshProUGUI accuracyText;


    [FormerlySerializedAs("enemyStatsDisplays")] [SerializeField]
    List<EnemySingleStatsDisplay> enemySingleStatsDisplays;

    [SerializeField] List<EnemyMultiStatsDisplay> enemyMultiStatsDisplays;


    Dictionary<EnemySingleStatsDisplay, StatsPerSingleEnemy> groupedStatsPerEnemyDisplay;

    
    public void ShowStatsLastWave()
    {
        StatsPerWave stats = statsTracker.StatsForCurrentWave;
        
        SetGeneralInfoTexts(stats);
        
        GroupSingleEnemyStats(stats);

        for (int i = 0; i < enemySingleStatsDisplays.Count; i++)
        {
            if (groupedStatsPerEnemyDisplay.ContainsKey(enemySingleStatsDisplays[i]))
            {
                enemySingleStatsDisplays[i].gameObject.SetActive(true);
                enemySingleStatsDisplays[i].InjectStats(groupedStatsPerEnemyDisplay[enemySingleStatsDisplays[i]]);
            }
            else
            {
                enemySingleStatsDisplays[i].gameObject.SetActive(false);
            }
        }
        
        
        
        for (int i = 0; i < enemyMultiStatsDisplays.Count; i++)
        {
            StatsMultiDrone statsMultiDrone = stats.StatsMultiDrones.Find(x => x.EnemySettings == enemyMultiStatsDisplays[i].EnemySettings);
            
            if (statsMultiDrone != null)
            {
                enemyMultiStatsDisplays[i].gameObject.SetActive(true);
                enemyMultiStatsDisplays[i].InjectStats(statsMultiDrone);
            }
            else
            {
                enemyMultiStatsDisplays[i].gameObject.SetActive(false);
            }
        }
    }

    
    public void ShowStatsAllWaves()
    {
        Debug.Log("ShowStatsAllWaves");
    }
    
    
    void SetGeneralInfoTexts(StatsPerWave stats)
    {
        waveText.text = "Wave " + stats.WaveIndex.ToString() + " Complete!";
        scoreText.text = "Score: " + scoreController.Score.ToString();
        shotsFiredText.text = "Shots: " + stats.ShotsFired.ToString();
        accuracyText.text = "Accuracy: " + (stats.Accuracy * 100f).ToString("F0") + "%";
    }


    void GroupSingleEnemyStats(StatsPerWave statsPerWave)
    {
        groupedStatsPerEnemyDisplay = new Dictionary<EnemySingleStatsDisplay, StatsPerSingleEnemy>();

        for (int i = 0; i < statsPerWave.StatsPerSingleEnemies.Count; i++)
        {
            EnemySingleStatsDisplay singleStatsDisplay = enemySingleStatsDisplays.Find(x => x.enemiesGroupedInStat.Contains(statsPerWave.StatsPerSingleEnemies[i].EnemySettings));

            if (singleStatsDisplay == null)
            {
                Debug.Log("ERROR: Stats display not found for enemy " + statsPerWave.StatsPerSingleEnemies[i].EnemySettings.name);
                // later account for creating one, but not needed right now
            }
            else
            {
                if (groupedStatsPerEnemyDisplay.ContainsKey(singleStatsDisplay))
                {
                    groupedStatsPerEnemyDisplay[singleStatsDisplay].DestroyedCorrectly += statsPerWave.StatsPerSingleEnemies[i].DestroyedCorrectly;
                    groupedStatsPerEnemyDisplay[singleStatsDisplay].DestroyedByMistake += statsPerWave.StatsPerSingleEnemies[i].DestroyedByMistake;
                }
                else
                {
                    groupedStatsPerEnemyDisplay.Add(singleStatsDisplay, statsPerWave.StatsPerSingleEnemies[i]);
                }
            }
        }
    }

  
}