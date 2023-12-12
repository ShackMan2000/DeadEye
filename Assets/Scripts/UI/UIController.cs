using System;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;


public class UIController : MonoBehaviour
{
    [SerializeField] HealthDisplay healthDisplay;

    [SerializeField] WaveController waveController;

    [SerializeField] StatsTracker statsTracker;

    [SerializeField] MeshCollider meshCollider;

    [SerializeField] GameObject menuPanel;
 
    
    // do later, needs to react to wave completed or failed
    //[SerializeField] Button startNextWaveBtn;
    
    [SerializeField] GameObject waveIngamePanel;
    [SerializeField] StatsDisplay statsDisplay;
    
    public static event Action<bool> OnEnterGameMode = delegate { };
    public static event Action OnEnableUnlimitedHealth = delegate {  }; 
    
    

    void OnEnable()
    {
        waveController.OnWaveFinished += ShowStatsAfterWavePanel;
    }
    
    void OnDisable()
    {
        waveController.OnWaveFinished -= ShowStatsAfterWavePanel;
    }


    void Start()
    {
        menuPanel.gameObject.SetActive(true);
        waveIngamePanel.SetActive(false);
        //startNextWaveBtn.gameObject.SetActive(false);
        statsDisplay.gameObject.SetActive(false);
    }

    
    
    
    [Button]
    public void ToggleMenuPanel(bool activate)
    {
        menuPanel.gameObject.SetActive(activate);
        EnableMeshCollider(activate);
    }
    

    [Button]
    public void StartNewWaveGame()
    {
        waveController.StartNewWaveGame();
        
        ToggleMenuPanel(false);
        ToggleActiveWavePanel(true);
        ToggleNextWavePanel(false);
        OnEnterGameMode(true);
    }
    
    [Button]
    void ToggleActiveWavePanel(bool activate)
    {
        waveIngamePanel.SetActive(activate);
        healthDisplay.gameObject.SetActive(activate);
    }
    
    
    
    [Button]
    void ShowStatsAfterWavePanel()
    {
        ToggleActiveWavePanel(false);
        ToggleNextWavePanel(true);
    }


    void ToggleNextWavePanel(bool show)
    {
        if (show)
        {
            statsDisplay.ShowStatsLastWave(statsTracker.StatsForCurrentWave);
        }
        statsDisplay.gameObject.SetActive(show);
        
        //startNextWaveBtn.gameObject.SetActive(show);
        EnableMeshCollider(show);
        waveIngamePanel.SetActive(!show);
    }
    
    
    [Button]
    public void StartNextWaveBtn()
    {
        waveController.StartNextWave();
       // startNextWaveBtn.gameObject.SetActive(false);
        EnableMeshCollider(false);
        waveIngamePanel.gameObject.SetActive(true);
        OnEnterGameMode(true);
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