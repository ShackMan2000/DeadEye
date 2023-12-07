using System.Collections.Generic;
using UnityEngine;

public class ControllerGunsToggle : MonoBehaviour
{
    [SerializeField] List<GameObject> guns;
    [SerializeField] List<GameObject> controllers;

    
    // might be better to route all things related to game mode through game controller, which right now does kinda nothing...
    // re evaluate when adding the arcaade mode
    [SerializeField] WaveController waveController;

    void OnEnable()
    {
        UIController.OnEnterGameMode += ToggleGuns;
        waveController.OnWaveFinished += ShowControllers;
    }
    
    void OnDisable()
    {
        UIController.OnEnterGameMode -= ToggleGuns;
        waveController.OnWaveFinished -= ShowControllers;
    }

    void ShowControllers()
    {
        ToggleGuns(false);
    }


    void Start()
    {
        ToggleGuns(false);
    }


    public void ToggleGuns(bool activateGuns)
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(activateGuns);
        }

        foreach (GameObject controller in controllers)
        {
            controller.SetActive(!activateGuns);
        }
    }
}