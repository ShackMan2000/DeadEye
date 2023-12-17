using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] StatsTracker statsTracker;
    [SerializeField] ScoreController scoreController;


    public WaveStats currentStatsDisplaying;
    [SerializeField] Button showStatsCurrentWaveButton;
    [SerializeField] Button showStatsAllWavesButton;
    [SerializeField] Color selectedButtonColor;
    [SerializeField] Color unselectedButtonColor;
    [SerializeField] float unselectedButtonHeight;
    [SerializeField] float selectedButtonHeightMulti;


    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI shotsFiredText;
    [SerializeField] TextMeshProUGUI accuracyText;


    [FormerlySerializedAs("enemyStatsDisplays")] [SerializeField]
    List<EnemySingleStatsDisplay> enemySingleStatsDisplays;

    [SerializeField] List<EnemyMultiStatsDisplay> enemyMultiStatsDisplays;

    Dictionary<EnemySingleStatsDisplay, StatsPerSingleEnemy> groupedStatsPerEnemyDisplay;


    
    [Button]
    public void ShowStatsCurrentWave()
    {
        ShowStats(statsTracker.StatsForCurrentWave);

        showStatsCurrentWaveButton.image.color = selectedButtonColor;
        showStatsAllWavesButton.image.color = unselectedButtonColor;

        RectTransform rectTransform = showStatsCurrentWaveButton.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, unselectedButtonHeight * selectedButtonHeightMulti);

        RectTransform rectTransform2 = showStatsAllWavesButton.GetComponent<RectTransform>();
        rectTransform2.sizeDelta = new Vector2(rectTransform2.sizeDelta.x, unselectedButtonHeight);
    }

    
    [Button]
    public void ShowStatsAllWaves()
    {
        
       ShowStats(statsTracker.GetStatsForAllWavesCombined());

        showStatsCurrentWaveButton.image.color = unselectedButtonColor;
        showStatsAllWavesButton.image.color = selectedButtonColor;

        RectTransform rectTransform = showStatsCurrentWaveButton.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, unselectedButtonHeight);

        RectTransform rectTransform2 = showStatsAllWavesButton.GetComponent<RectTransform>();
        rectTransform2.sizeDelta = new Vector2(rectTransform2.sizeDelta.x, unselectedButtonHeight * selectedButtonHeightMulti);
    }


    void ShowStats(WaveStats waveStats)
    {
        currentStatsDisplaying = waveStats;
        
        SetGeneralInfoTexts(waveStats);

        GroupSingleEnemyStats(waveStats);

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
            StatsMultiDrone statsMultiDrone = waveStats.StatsMultiDrones.Find(x => x.EnemySettings == enemyMultiStatsDisplays[i].EnemySettings);

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


    void SetGeneralInfoTexts(WaveStats waveStats)
    {
        waveText.text = "Wave " + (waveStats.WaveIndex + 1).ToString() + " Complete!";
        scoreText.text = "Score: " + scoreController.Score.ToString();
        shotsFiredText.text = "Shots: " + waveStats.ShotsFired.ToString();
        accuracyText.text = "Accuracy: " + (waveStats.Accuracy * 100f).ToString("F0") + "%";
    }


    void GroupSingleEnemyStats(WaveStats waveStats)
    {
        groupedStatsPerEnemyDisplay = new Dictionary<EnemySingleStatsDisplay, StatsPerSingleEnemy>();

        for (int i = 0; i < waveStats.StatsPerSingleEnemies.Count; i++)
        {
            EnemySingleStatsDisplay singleStatsDisplay = enemySingleStatsDisplays.Find(x => x.enemiesGroupedInStat.Contains(waveStats.StatsPerSingleEnemies[i].EnemySettings));

            if (singleStatsDisplay == null)
            {
                Debug.Log("ERROR: Stats display not found for enemy " + waveStats.StatsPerSingleEnemies[i].EnemySettings.name);
                // later account for creating one, but not needed right now
            }
            else
            {
                if (groupedStatsPerEnemyDisplay.ContainsKey(singleStatsDisplay))
                {
                    groupedStatsPerEnemyDisplay[singleStatsDisplay].DestroyedCorrectly += waveStats.StatsPerSingleEnemies[i].DestroyedCorrectly;
                    groupedStatsPerEnemyDisplay[singleStatsDisplay].DestroyedByMistake += waveStats.StatsPerSingleEnemies[i].DestroyedByMistake;
                }
                else
                {
                    groupedStatsPerEnemyDisplay.Add(singleStatsDisplay, waveStats.StatsPerSingleEnemies[i]);
                }
            }
        }
    }
    
    
    [Button]
    void SetUnselectedButtonHeight()
    {
        RectTransform rectTransform = showStatsCurrentWaveButton.GetComponent<RectTransform>();
        unselectedButtonHeight = rectTransform.sizeDelta.y;
    }
}