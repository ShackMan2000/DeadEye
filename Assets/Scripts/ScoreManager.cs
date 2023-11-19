using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameNative.Leaderboard;

public class ScoreManager : MonoBehaviour
{
    public Leaderboard _leaderboard;
    public uint Score = 0;
    public Text scoreText;
   // public Text highScoreText; // Renamed to avoid confusion with the method name
    public uint highScoreNum;
    public Text PauseScoreText;
    public EntryUI entryUI;
    public string Rank = "rank";

    private void Start()
    {
        
      //  _leaderboard = GameObject.FindWithTag("LEADERBOARD").GetComponent<Leaderboard>();
      //  scoreText = GameObject.FindWithTag("SCORE").GetComponent<Text>();
       // highScoreNum = (uint)PlayerPrefs.GetInt("HighScore", 0); // Load high score from player prefs
       // highScoreText.text = "High Score: " + highScoreNum.ToString();
       
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

    public void GameOber()

    {
        UpdateScoreText();
        _leaderboard.AddEntry(Score);
        
    }

    public void UpdateScoreText()
    {
       // scoreText.text =  Score.ToString();
        //PauseScoreText.text = Score.ToString();
      //  if (Score > highScoreNum)
      //  {
     //       highScoreNum = Score;
            //highScoreText.text = "High Score: " + highScoreNum.ToString();
      //      PlayerPrefs.SetInt("HighScore", (int)highScoreNum); // Save new high score to player prefs
     //   }

    }

    public void ScoreReset()
    {
        Score = 0;
        UpdateScoreText();
    }
}