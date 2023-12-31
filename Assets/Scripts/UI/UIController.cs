using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;


public class UIController : MonoBehaviour
{
  //  [SerializeField] HealthDisplay healthDisplay;

    [SerializeField] StatsTracker statsTracker;

    [SerializeField] MeshCollider meshCollider;

    [SerializeField] GameObject menuPanel;

    [SerializeField] GameObject waveIngamePanel;

    [SerializeField] GameObject timeLeft;
    
    [SerializeField] StatsDisplay statsDisplay;
    
    [SerializeField] List<GameObject> panelsToHideOnStart;

   // public static event Action OnEnableUnlimitedHealth = delegate { };


    void OnEnable()
    {
        GameManager.OnWaveCompleted += ShowStatsOnWaveCompleted;
        GameManager.OnWaveFailed += ShowStatsOnWaveFailed;
        
        GameManager.OnTimeTrialCompleted += ShowStatsOnTimeTrialCompleted;
        GameManager.OnTimeTrialFailed += ShowStatsOnTimeTrialFailed;
    }

    void OnDisable()
    {
        GameManager.OnWaveCompleted -= ShowStatsOnWaveCompleted;
        GameManager.OnWaveFailed -= ShowStatsOnWaveFailed;
        
        GameManager.OnTimeTrialCompleted -= ShowStatsOnTimeTrialCompleted;
        GameManager.OnTimeTrialFailed -= ShowStatsOnTimeTrialFailed;
    }


    void Start()
    {
        foreach (GameObject panel in panelsToHideOnStart)
        {
            panel.SetActive(false);
        }
        
        menuPanel.gameObject.SetActive(true);
   
    }


    [Button]
    public void ToggleMenuPanel(bool activate)
    {
        menuPanel.gameObject.SetActive(activate);
        waveIngamePanel.SetActive(!activate);
        statsDisplay.gameObject.SetActive(!activate);
        EnableMeshCollider(activate);
    }


    [Button]
    public void StartNewWaveGame()
    {
        ToggleMenuPanel(false);
        ToggleIngamePanel(true);

        timeLeft.gameObject.SetActive(false);
        
        ToggleStatsPanel(false);

        GameManager.StartNewWaveGame();
    }
    
    
    [Button]
    public void StartNewTimeTrialGame()
    {
        ToggleMenuPanel(false);
        ToggleIngamePanel(true);
        ToggleStatsPanel(false);

        GameManager.StartNewTimeTrialGame();
    }
    
    

    [Button]
    void ToggleIngamePanel(bool activate)
    {
        waveIngamePanel.SetActive(activate);
    }


    [Button]
    void ShowStatsOnWaveCompleted()
    {
        ToggleIngamePanel(false);
        ToggleStatsPanel(true);
        statsDisplay.ShowStatsCurrentWave();
    }

    void ShowStatsOnWaveFailed()
    {
        ToggleIngamePanel(false);
        ToggleStatsPanel(true);
        statsDisplay.ShowStatsAllWaves();
    }

    void ShowStatsOnTimeTrialCompleted()
    {
        ToggleIngamePanel(false);
        ToggleStatsPanel(true);
        statsDisplay.ShowStatsTimeTrial(true);
    }
    
    void ShowStatsOnTimeTrialFailed()
    {
        ToggleIngamePanel(false);
        ToggleStatsPanel(true);
        statsDisplay.ShowStatsTimeTrial(false);
    }

    void ToggleStatsPanel(bool show)
    {
        statsDisplay.gameObject.SetActive(show);
        EnableMeshCollider(show);
        waveIngamePanel.SetActive(!show);
    }
    
    


    [Button]
    public void StartNextWaveBtn()
    {
        ToggleStatsPanel(false);
        EnableMeshCollider(false);
        waveIngamePanel.gameObject.SetActive(true);
        GameManager.StartWave();
    }


    void EnableMeshCollider(bool activate)
    {
        meshCollider.enabled = activate;
    }


  
}