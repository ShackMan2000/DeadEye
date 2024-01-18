using System;
using System.Collections.Generic;
using UnityEngine;


public class PauseMode : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    // when the A button on either quest controller is pressed and game is not paused, pause it.

    [SerializeField] UIController uiController;
    
    
    void Update()
    {
        if (!GameManager.ShootingModeActive || GameManager.IsPaused) return;

        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            GameManager.PauseGame();
            pausePanel.SetActive(true);
            uiController.EnableMeshCollider(true);
        }
if (Input.GetKeyDown(KeyCode.Space))
        
        {
            GameManager.PauseGame();
            pausePanel.SetActive(true);
        }
        
    }


    public void ResumeGame()
    {
        GameManager.ResumeGame();
        pausePanel.SetActive(false);
        uiController.EnableMeshCollider(false);
    }
    
    public void QuitGame()
    {
        GameManager.QuitGameThroughPauseMenu();
        pausePanel.SetActive(false);
        uiController.ToggleMenuPanel(true);
    }
}