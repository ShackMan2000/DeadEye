using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class ScoreDisplay : MonoBehaviour
{

    [SerializeField] ScoreController scoreController;
    
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI currentMultiText;
    [FormerlySerializedAs("killStreakText")] [SerializeField] TextMeshProUGUI nextMultiText;

    [SerializeField] ScoreSettings scoreSettings;
    
    [SerializeField] Image progressBarMain;
    [SerializeField] Image progressBarHighlight;

    
    ScoreMulti currentMulti;
    // don't set killstreak text, only update progress bar
    // need to know how many are needed for the next in total, and how many are 
    
    void OnEnable()
    {
        scoreController.OnScoreChanged += OnScoreChanged;
        scoreController.OnKillStreakChanged += OnKillStreakChanged;
    }
    
    void OnDisable()
    {
       scoreController.OnScoreChanged -= OnScoreChanged;
        scoreController.OnKillStreakChanged -= OnKillStreakChanged;
    }
    
    
    void OnScoreChanged(float newScore)
    {
        scoreText.text = newScore.ToString();
    }
    
    
    void OnKillStreakChanged(int killStreak)
    {
        ScoreMulti updatedMulti = scoreSettings.GetScoreMultiByKillStreak(killStreak);
        //
        // if (updatedMulti != currentMulti)
        // {
            currentMulti = updatedMulti;
        //} 
            UpdateMultiDisplay();
    }

    
    
    void UpdateMultiDisplay()
    {
        float progressToNextMulti;
        
       currentMultiText.text = "x" + currentMulti.ScoreMultiplier.ToString();
       currentMultiText.color = currentMulti.Color;
        

        if (scoreSettings.IsHighestMulti(currentMulti))
        {
            progressToNextMulti = 1;
            nextMultiText.text = "MAX";
        }
        else
        {
            int killStreakForNextMulti = scoreSettings.GetKillStreakForNextMulti(currentMulti);
            int additionalStreakNeeded = killStreakForNextMulti - currentMulti.KillStreakNeeded;
            int killStreaksDoneTowardsNext = scoreController.KillStreak - currentMulti.KillStreakNeeded;
            

            progressToNextMulti = (float) killStreaksDoneTowardsNext / additionalStreakNeeded;
            nextMultiText.text = "x" + scoreSettings.ScoreMultipliers[scoreSettings.ScoreMultipliers.IndexOf(currentMulti) + 1].ScoreMultiplier.ToString();
        }
           
        
        progressBarMain.fillAmount = progressToNextMulti;
        progressBarHighlight.fillAmount = progressToNextMulti;
        
    }



    
    
    
}