using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    [SerializeField] HealthDisplay healthDisplay;

    [SerializeField] WaveController     waveController;


    [SerializeField] MeshCollider      meshCollider;

    [SerializeField] Button newWaveGameBtn;
    [SerializeField] Button startNextWaveBtn;


    void Start()
    {
        newWaveGameBtn.gameObject.SetActive(true);
    }

    
    // separate that out more and more. E.g. main menu, wave menu etc...
    public void ShowMenuPanel()
    {
        newWaveGameBtn.gameObject.SetActive(true);
    }
    

    public void StartNewWaveGame()
    {
        waveController.StartNewWaveGame();
        newWaveGameBtn.gameObject.SetActive(false);
        EnableMeshCollider(false);
    }
    
    
    void ShowNextWavePanel()
    {
        startNextWaveBtn.gameObject.SetActive(true);
    }
    
    public void StartNextWaveBtn()
    {
        waveController.StartNextWave();
        startNextWaveBtn.gameObject.SetActive(false);
        EnableMeshCollider(false);
    }
    
    
    void EnableMeshCollider(bool activate)
    {
        meshCollider.enabled = activate;
    }
    
    
    
}