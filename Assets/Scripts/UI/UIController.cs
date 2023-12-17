using System;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;


public class UIController : MonoBehaviour
{
    [SerializeField] HealthDisplay healthDisplay;

    [SerializeField] StatsTracker statsTracker;

    [SerializeField] MeshCollider meshCollider;

    [SerializeField] GameObject menuPanel;

    [SerializeField] GameObject waveIngamePanel;

    [SerializeField] StatsDisplay statsDisplay;

    public static event Action OnEnableUnlimitedHealth = delegate { };


    void OnEnable()
    {
        GameManager.OnWaveCompleted += ShowStatsOnWaveCompleted;
        GameManager.OnWaveFailed += ShowStatsOnWaveFailed;
    }

    void OnDisable()
    {
        GameManager.OnWaveCompleted -= ShowStatsOnWaveCompleted;
        GameManager.OnWaveFailed -= ShowStatsOnWaveFailed;
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
        EnableMeshCollider(activate);
    }


    [Button]
    public void StartNewWaveGame()
    {
        ToggleMenuPanel(false);
        ToggleActiveWavePanel(true);
        ToggleStatsPanel(false);

        GameManager.StartNewWaveGame();
    }

    [Button]
    void ToggleActiveWavePanel(bool activate)
    {
        waveIngamePanel.SetActive(activate);
        healthDisplay.gameObject.SetActive(activate);
    }


    [Button]
    void ShowStatsOnWaveCompleted()
    {
        ToggleActiveWavePanel(false);
        ToggleStatsPanel(true);
        statsDisplay.ShowStatsCurrentWave();
    }

    void ShowStatsOnWaveFailed()
    {
        ToggleActiveWavePanel(false);
        ToggleStatsPanel(true);
        statsDisplay.ShowStatsAllWaves();
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