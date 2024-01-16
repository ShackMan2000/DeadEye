using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameOverPanel : MonoBehaviour
{
    [SerializeField] UIController uiController;

    [SerializeField] StatsPanel statsPanel;

    [SerializeField] TextMeshProUGUI headerText;

    // set text, activate panel
    // show main ui and show stats there
    // update the stats when stats tracker saves, might happen after this
    //basically just the stats panel, aka open it and open the right one depending on what was done

    // maybe flow should be that uicontroller opens the panel and tells it what mode finished
    // this one here clicks the stats button and if it was time trial, clicks that button too
    // just clickity clack who cares. easiest

    bool showingWaveStats;

    [SerializeField] SelectionButton showStatsPanelButton;

    void OnEnable()
    {
        StatsTracker.OnSavedStats += OnStatsUpdated;
    }

    void OnDisable()
    {
        StatsTracker.OnSavedStats -= OnStatsUpdated;
    }


    // might happen right after showing the panel...
    void OnStatsUpdated()
    {
        if(showingWaveStats)
            statsPanel.CreateGraphsForWaveGames();
        else
            statsPanel.CreateGraphsForTimeTrialGames();
    }


    public void ShowWaveGameFailed()
    {
        gameObject.SetActive(true);
        
        statsPanel.isShowingWaveStats = true;
        showStatsPanelButton.OnClick();
        
        showingWaveStats = true;
        
        headerText.text = "Wave Game Over";
        
    }

    public void ShowTimeTrialSuccess()
    {
        gameObject.SetActive(true);
        
        statsPanel.isShowingWaveStats = false;
        showStatsPanelButton.OnClick();
        
        showingWaveStats = false;
        headerText.text = "Time Trial Completed!";
    }

    public void ShowTimeTrialFailed()
    {
        gameObject.SetActive(true);
        
        statsPanel.isShowingWaveStats = false;
        showStatsPanelButton.OnClick();
        
        showingWaveStats = false;
        headerText.text = "Time Trial Failed";
    }
}