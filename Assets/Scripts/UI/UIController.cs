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

    [SerializeField] GameOverPanel gameOverPanel;
    // [SerializeField] StatsDisplay statsDisplay;

    [SerializeField] List<GameObject> panelsToHideOnStart;

    // public static event Action OnEnableUnlimitedHealth = delegate { };


    void OnEnable()
    {
        GameManager.OnWaveGameFailed += ShowStatsOnWaveGameFailed;

        GameManager.OnTimeTrialSuccess += ShowStatsOnTimeTrialSuccess;
        GameManager.OnTimeTrialFailed += ShowStatsOnTimeTrialFailed;
    }

    void OnDisable()
    {
        GameManager.OnWaveGameFailed -= ShowStatsOnWaveGameFailed;

        GameManager.OnTimeTrialSuccess -= ShowStatsOnTimeTrialSuccess;
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
        EnableMeshCollider(activate);
    }


    [Button]
    public void StartNewWaveGame()
    {
        ToggleMenuPanel(false);
        ToggleIngamePanel(true);
        gameOverPanel.gameObject.SetActive(false);

        timeLeft.gameObject.SetActive(false);

        GameManager.StartNewWaveGame();
    }


    [Button]
    public void StartNewTimeTrialGame()
    {
        ToggleMenuPanel(false);
        ToggleIngamePanel(true);
        gameOverPanel.gameObject.SetActive(false);
        timeLeft.gameObject.SetActive(true);


        GameManager.StartNewTimeTrialGame();
    }


    [Button]
    void ToggleIngamePanel(bool activate)
    {
        waveIngamePanel.SetActive(activate);
    }


    void ShowStatsOnWaveGameFailed()
    {
        // show the panel, route click through the menu stats button, cleanest
        ToggleMenuPanel(true);
        gameOverPanel.ShowWaveGameFailed();
    }

    void ShowStatsOnTimeTrialSuccess()
    {
        ToggleMenuPanel(true);
        gameOverPanel.ShowTimeTrialSuccess();
    }

    void ShowStatsOnTimeTrialFailed()
    {
        ToggleMenuPanel(true);
        gameOverPanel.ShowTimeTrialFailed();
    }

    // void ToggleStatsPanel(bool show)
    // {
    //     EnableMeshCollider(show);
    //     waveIngamePanel.SetActive(!show);
    // }


    // [Button]
    // public void StartNextWaveBtn()
    // {
    //     EnableMeshCollider(false);
    //     waveIngamePanel.gameObject.SetActive(true);
    //     GameManager.StartWave();
    // }


    public void EnableMeshCollider(bool activate)
    {
        meshCollider.enabled = activate;
    }
}