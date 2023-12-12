using System;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;


public class UIController : MonoBehaviour
{
    [SerializeField] HealthDisplay healthDisplay;

    [SerializeField] WaveController waveController;


    [SerializeField] MeshCollider meshCollider;

    [SerializeField] Button newWaveGameBtn;
    [SerializeField] Button startNextWaveBtn;
    
    [FormerlySerializedAs("scoreDisplay")] [SerializeField] ScoreDisplay ingameScoreDisplay;
    [SerializeField] StatsDisplay statsDisplay;
    
    public static event Action<bool> OnEnterGameMode = delegate { };
    public static event Action OnEnableUnlimitedHealth = delegate {  }; 
    
    

    void OnEnable()
    {
        waveController.OnWaveFinished += ShowNextWavePanel;
    }
    
    void OnDisable()
    {
        waveController.OnWaveFinished -= ShowNextWavePanel;
    }


    void Start()
    {
        newWaveGameBtn.gameObject.SetActive(true);
        ingameScoreDisplay.gameObject.SetActive(false);
    }

    
    
    
    [Button]
    public void ToggleMenuPanel(bool activate)
    {
        newWaveGameBtn.gameObject.SetActive(activate);
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
        ingameScoreDisplay.gameObject.SetActive(activate);
        healthDisplay.gameObject.SetActive(activate);
    }
    
    
    
    void ShowNextWavePanel() => ToggleNextWavePanel(true);
    
    void ToggleNextWavePanel(bool show)
    {
        statsDisplay.gameObject.SetActive(show);
        startNextWaveBtn.gameObject.SetActive(show);
        EnableMeshCollider(show);
        ingameScoreDisplay.gameObject.SetActive(!show);
    }
    
    
    [Button]
    public void StartNextWaveBtn()
    {
        waveController.StartNextWave();
        startNextWaveBtn.gameObject.SetActive(false);
        EnableMeshCollider(false);
        ingameScoreDisplay.gameObject.SetActive(true);
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