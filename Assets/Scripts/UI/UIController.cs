using System;
using System.Collections.Generic;
using Backend;
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
    [SerializeField] GameObject tutorialInstructionsPanel;

    [SerializeField] GameObject timeLeft;

    [SerializeField] GameOverPanel gameOverPanel;
    // [SerializeField] StatsDisplay statsDisplay;

    [SerializeField] Tutorial tutorial;

    [SerializeField] WaveController waveController;
    [SerializeField] TimeTrialManager timeTrialManager;
    
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

        if(SaveManager.Instance.GetSaveData().TutorialCompleted)
        {
            ToggleMenuPanel(true);
        }
        else
        {
            StartTutorial();
        }
        
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

        waveController.StartNewWaveGame();
    }


    public void StartTutorial()
    {
        ToggleMenuPanel(false);
        ToggleIngamePanel(false);
        tutorialInstructionsPanel.SetActive(true);
        gameOverPanel.gameObject.SetActive(false);

        tutorial.StartTutorial();
    }
    
    public void FinishedTutorial()
    {
        tutorialInstructionsPanel.SetActive(false);
        ToggleMenuPanel(true);
    }
    

    [Button]
    public void StartNewTimeTrialGame()
    {
        ToggleMenuPanel(false);
        ToggleIngamePanel(true);
        gameOverPanel.gameObject.SetActive(false);
        timeLeft.gameObject.SetActive(true);
        
        timeTrialManager.StartNewTimeTrialGame();


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




    public void EnableMeshCollider(bool activate)
    {
        meshCollider.enabled = activate;
    }


}