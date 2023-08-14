using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float Score = 0f;
    public TMP_Text scoreText;
    public Text highScoreText; // Renamed to avoid confusion with the method name
    public float highScoreNum;

    private void Awake()
    {
        scoreText = GameObject.FindWithTag("SCORE").GetComponent<TMP_Text>();
        highScoreNum = PlayerPrefs.GetFloat("HighScore", 0f); // Load high score from player prefs
        highScoreText.text = "High Score: " + highScoreNum.ToString();
    }

    public void Score10()
    {
        Score += 10f;
        UpdateScoreText();
    }

    public void Score30()
    {
        Score += 30f;
        UpdateScoreText();
    }

    public void MinusScore()
    {
        Score -= 5f;
        UpdateScoreText();
    }

    public void score(float newScore)
    {
        Score += newScore;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + Score.ToString();
        if (Score > highScoreNum)
        {
            highScoreNum = Score;
            highScoreText.text = "High Score: " + highScoreNum.ToString();
            PlayerPrefs.SetFloat("HighScore", highScoreNum); // Save new high score to player prefs
        }
    }
}