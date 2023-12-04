using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ScoreDisplay : MonoBehaviour
{

    [SerializeField] ScoreController scoreController;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI killStreakText;

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
    
    
    void OnScoreChanged(int score)
    {
        scoreText.text = score.ToString();
    }
    
    
    void OnKillStreakChanged(int killStreak)
    {
        killStreakText.text = killStreak.ToString();
    }
}