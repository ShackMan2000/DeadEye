using System;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;


public class UIController : MonoBehaviour
{
    [SerializeField] HealthDisplay healthDisplay;

    [SerializeField] WaveController waveController;


    [SerializeField] MeshCollider meshCollider;

    [SerializeField] Button newWaveGameBtn;
    [SerializeField] Button startNextWaveBtn;
    
    [SerializeField] ScoreDisplay scoreDisplay;

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
        scoreDisplay.gameObject.SetActive(false);
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
        OnEnterGameMode(true);
    }
    
    [Button]
    void ToggleActiveWavePanel(bool activate)
    {
        scoreDisplay.gameObject.SetActive(activate);
        healthDisplay.gameObject.SetActive(activate);
    }
    
    
    void ShowNextWavePanel()
    {
        startNextWaveBtn.gameObject.SetActive(true);
        EnableMeshCollider(true);
        scoreDisplay.gameObject.SetActive(false);
    }
    
    
    [Button]
    public void StartNextWaveBtn()
    {
        waveController.StartNextWave();
        startNextWaveBtn.gameObject.SetActive(false);
        EnableMeshCollider(false);
        scoreDisplay.gameObject.SetActive(true);
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