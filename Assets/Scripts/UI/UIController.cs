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

    
    // separate that out more and more. E.g. main menu, wave menu etc...
    public void ShowMenuPanel()
    {
        newWaveGameBtn.gameObject.SetActive(true);
    }
    

    [Button]
    public void StartNewWaveGame()
    {
        waveController.StartNewWaveGame();
        newWaveGameBtn.gameObject.SetActive(false);
        EnableMeshCollider(false);
        scoreDisplay.gameObject.SetActive(true);
        OnEnterGameMode(true);
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
    
    
    
}