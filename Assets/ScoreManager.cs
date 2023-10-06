using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public uint Score = 0;
    public Text scoreText;
    public Text highScoreText; // Renamed to avoid confusion with the method name
    public float highScoreNum;
    public Text PauseScoreText;

    private void Awake()
    {
        scoreText = GameObject.FindWithTag("SCORE").GetComponent<Text>();
        highScoreNum = PlayerPrefs.GetFloat("HighScore", 0f); // Load high score from player prefs
        highScoreText.text = "High Score: " + highScoreNum.ToString();
    }

    public void Score10()
    {
        Score += 10;
        UpdateScoreText();
    }

    public void Score30()
    {
        Score += 30;
        UpdateScoreText();
    }

    public void MinusScore()
    {
        Score -= 5;
        UpdateScoreText();
    }

    public void score(uint newScore)
    {
        Score += newScore;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + Score.ToString();
        PauseScoreText.text = Score.ToString();
        if (Score > highScoreNum)
        {
            highScoreNum = Score;
            highScoreText.text = "High Score: " + highScoreNum.ToString();
            PlayerPrefs.SetFloat("HighScore", highScoreNum); // Save new high score to player prefs
        }

    }

    public void ScoreReset()
    {
        Score = 0;
        UpdateScoreText();
    }
}