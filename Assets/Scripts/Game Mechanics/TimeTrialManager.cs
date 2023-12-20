using System;
using System.Collections.Generic;
using UnityEngine;


public class TimeTrialManager : MonoBehaviour
{
        
    [SerializeField] TimeTrialSettings settings;
    
    
    // needs settings, at the very least the time
    // save later in player prefs or something


    void OnEnable()
    {
        GameManager.OnStartingNewTimeTrialGame += OnStartingNewTimeTrialGame;
        
    }
    
    void OnDisable()
    {
        GameManager.OnStartingNewTimeTrialGame -= OnStartingNewTimeTrialGame;
    }


    void OnStartingNewTimeTrialGame()
    {
        
    }
}