using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class StatsDisplay : MonoBehaviour
{
    [SerializeField] StatsTracker statsTracker;
    [SerializeField] ScoreController scoreController;

    // could just have settings in the enemy stats display already, including the SO for correct weapon


    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI shotsFiredText;
    [SerializeField] TextMeshProUGUI accuracyText;
    
    
    [SerializeField] List<EnemyStatsDisplay> enemyStatsDisplays;


    Dictionary<EnemyStatsDisplay, StatsPerEnemy> groupedStatsPerEnemyDisplay;

    // need to group the stats here and inject them, separate out so maybe not group them i

    public void ShowStatsLastWave(StatsPerWave stats)
    {
        SetInfoTexts(stats);
        GroupEnemyStats(stats);
        
        for (int i = 0; i < enemyStatsDisplays.Count; i++)
        {
            if (groupedStatsPerEnemyDisplay.ContainsKey(enemyStatsDisplays[i]))
            {
                enemyStatsDisplays[i].gameObject.SetActive(true);
                enemyStatsDisplays[i].InjectStats(groupedStatsPerEnemyDisplay[enemyStatsDisplays[i]]);
            }
            else
            {
                enemyStatsDisplays[i].gameObject.SetActive(false);
            }
        }
    }

    void SetInfoTexts(StatsPerWave stats)
    {
        waveText.text = "Wave " + stats.WaveIndex.ToString() + " Complete!";
        scoreText.text = "Score: " + scoreController.Score.ToString();
        shotsFiredText.text = "Shots: " + stats.ShotsFired.ToString();
        accuracyText.text = "Accuracy: " + (stats.Accuracy * 100f).ToString("F0") + "%";
    }


    void GroupEnemyStats(StatsPerWave statsPerWave)
    {
        groupedStatsPerEnemyDisplay = new Dictionary<EnemyStatsDisplay, StatsPerEnemy>();


        for (int i = 0; i < statsPerWave.StatsPerEnemies.Count; i++)
        {
            EnemyStatsDisplay statsDisplay = enemyStatsDisplays.Find(x => x.enemiesGroupedInStat.Contains(statsPerWave.StatsPerEnemies[i].EnemySettings));

            if (statsDisplay == null)
            {
                Debug.Log("ERROR: Stats display not found for enemy " + statsPerWave.StatsPerEnemies[i].EnemySettings.name);
                // later account for creating one, but not needed right now
            }
            else
            {
                if(groupedStatsPerEnemyDisplay.ContainsKey(statsDisplay))
                {
                    groupedStatsPerEnemyDisplay[statsDisplay].DestroyedCorrectly += statsPerWave.StatsPerEnemies[i].DestroyedCorrectly;
                    groupedStatsPerEnemyDisplay[statsDisplay].DestroyedByMistake += statsPerWave.StatsPerEnemies[i].DestroyedByMistake;
                }
                else
                {
                    groupedStatsPerEnemyDisplay.Add(statsDisplay, statsPerWave.StatsPerEnemies[i]);
                }
            }
        }
    }
}