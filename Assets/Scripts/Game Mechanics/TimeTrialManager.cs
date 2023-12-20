using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TimeTrialManager : MonoBehaviour
{
    
    public List<float> TimeOptions;

    public int selectedTimeIndex;
    
    public float SelectedTime => TimeOptions[selectedTimeIndex];
    
    public int MaxActiveEnemies = 20;

    float timeLeft;

    [SerializeField] TextMeshProUGUI timeLeftText;
    
    bool gameIsRunning;

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
        timeLeft = SelectedTime;
        gameIsRunning = true;
        timeLeftText.gameObject.SetActive(true);
    }


    void Update()
    {
        if(gameIsRunning)
        {
            timeLeft -= Time.deltaTime;
            timeLeftText.text = timeLeft.ToString("F1") + "s";
            
            if (timeLeft <= 0)
            {
                gameIsRunning = false;
                GameManager.FinishTimeTrialGame();
            }
        }
        
    }
}