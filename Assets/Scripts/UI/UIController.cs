using System;
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

    public static event Action OnEnableUnlimitedHealth = delegate { };


    void OnEnable()
    {
        GameManager.OnWaveCompleted += ShowStatsOnWaveCompleted;
        GameManager.OnWaveFailed += ShowStatsOnWaveFailed;
        
        GameManager.OnTimeTrialFinished += ShowStatsOnTimeTrialFinished;
    }

    void OnDisable()
    {
        GameManager.OnWaveCompleted -= ShowStatsOnWaveCompleted;
        GameManager.OnWaveFailed -= ShowStatsOnWaveFailed;
        
        GameManager.OnTimeTrialFinished -= ShowStatsOnTimeTrialFinished;
    }


    void Start()
    {
        menuPanel.gameObject.SetActive(true);
        waveIngamePanel.SetActive(false);
        statsDisplay.gameObject.SetActive(false);
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

    void ShowStatsOnTimeTrialFinished()
    {
        ToggleIngamePanel(false);
        ToggleStatsPanel(true);
        statsDisplay.ShowStatsTimeTrial();
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
        GameManager.StartNextWave();
    }


    void EnableMeshCollider(bool activate)
    {
        meshCollider.enabled = activate;
    }


    [Button]
    public void EnableUnlimitedHealth()
    {
        OnEnableUnlimitedHealth?.Invoke();
    }
}