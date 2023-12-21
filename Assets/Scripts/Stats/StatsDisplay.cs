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


    public Stats currentStatsDisplaying;
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


    [SerializeField] Button NextWaveButton;
    [SerializeField] Button BackToMenuButtonLarge;
    [SerializeField] Button BackToMenuButtonSmall;

    [FormerlySerializedAs("enemyStatsDisplays")] [SerializeField]
    List<EnemySingleStatsDisplay> enemySingleStatsDisplays;

    [SerializeField] List<EnemyMultiStatsDisplay> enemyMultiStatsDisplays;

    Dictionary<EnemySingleStatsDisplay, StatsPerSingleEnemy> groupedStatsPerEnemyDisplay;


    [Button]
    public void ShowStatsCurrentWave()
    {
        ShowStats(statsTracker.statsThisRound);

        SetGeneralInfoTexts(statsTracker.statsThisRound, true);

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
        var stats = statsTracker.GetStatsForAllWavesCombined();

        ShowStats(stats);

        SetGeneralInfoTexts(stats, true);

        showStatsCurrentWaveButton.image.color = unselectedButtonColor;
        showStatsAllWavesButton.image.color = selectedButtonColor;

        RectTransform rectTransform = showStatsCurrentWaveButton.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, unselectedButtonHeight);

        RectTransform rectTransform2 = showStatsAllWavesButton.GetComponent<RectTransform>();
        rectTransform2.sizeDelta = new Vector2(rectTransform2.sizeDelta.x, unselectedButtonHeight * selectedButtonHeightMulti);
    }

    [Button]
    public void ShowStatsTimeTrial()
    {
        Debug.Log("ShowStatsTimeTrial");
        ShowStats(statsTracker.statsThisRound);
        
        SetGeneralInfoTexts(statsTracker.statsThisRound, false);
    }


    void ShowStats(Stats stats)
    {
        currentStatsDisplaying = stats;

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



    void SetGeneralInfoTexts(Stats stats, bool isWaveMode)
    {
        if (isWaveMode)
        {
            if (GameManager.GameOver)
            {
                waveText.text = "Game Over";

                NextWaveButton.gameObject.SetActive(false);
                BackToMenuButtonSmall.gameObject.SetActive(false);
                BackToMenuButtonLarge.gameObject.SetActive(true);
            }
            else
            {
                waveText.text = "Wave " + (stats.WaveIndex + 1).ToString() + " Complete!";

                NextWaveButton.gameObject.SetActive(true);
                BackToMenuButtonSmall.gameObject.SetActive(true);
                BackToMenuButtonLarge.gameObject.SetActive(false);
            }
        }
        else
        {
            waveText.text = "Time Trial Complete!";
            
            NextWaveButton.gameObject.SetActive(false);
            BackToMenuButtonSmall.gameObject.SetActive(false);
            BackToMenuButtonLarge.gameObject.SetActive(true);
        }
        
        ToggleButtonsForWaveStats(isWaveMode);

        scoreText.text = "Score: " + stats.Score.ToString();
        shotsFiredText.text = "Shots: " + stats.ShotsFired.ToString();
        accuracyText.text = "Accuracy: " + (stats.Accuracy * 100f).ToString("F0") + "%";
    }


    void ToggleButtonsForWaveStats(bool show)
    {
        showStatsCurrentWaveButton.gameObject.SetActive(show);
        showStatsAllWavesButton.gameObject.SetActive(show);
    }


    void GroupSingleEnemyStats(Stats stats)
    {
        groupedStatsPerEnemyDisplay = new Dictionary<EnemySingleStatsDisplay, StatsPerSingleEnemy>();

        for (int i = 0; i < stats.StatsPerSingleEnemies.Count; i++)
        {
            EnemySingleStatsDisplay singleStatsDisplay = enemySingleStatsDisplays.Find(x => x.enemiesGroupedInStat.Contains(stats.StatsPerSingleEnemies[i].EnemySettings));

            if (singleStatsDisplay == null)
            {
                Debug.Log("ERROR: Stats display not found for enemy " + stats.StatsPerSingleEnemies[i].EnemySettings.name);
                // later account for creating one, but not needed right now
            }
            else
            {
                if (groupedStatsPerEnemyDisplay.ContainsKey(singleStatsDisplay))
                {
                    groupedStatsPerEnemyDisplay[singleStatsDisplay].DestroyedCorrectly += stats.StatsPerSingleEnemies[i].DestroyedCorrectly;
                    groupedStatsPerEnemyDisplay[singleStatsDisplay].DestroyedByMistake += stats.StatsPerSingleEnemies[i].DestroyedByMistake;
                }
                else
                {
                    groupedStatsPerEnemyDisplay.Add(singleStatsDisplay, stats.StatsPerSingleEnemies[i]);
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