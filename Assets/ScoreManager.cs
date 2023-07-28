using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float Score = 0f;
    public TMP_Text scoreText;
    
    private void Awake()
    {
        scoreText = GameObject.FindWithTag("SCORE").GetComponent<TMP_Text>();
    }

    public void Score10()
    {
        Score = Score + 10f;
        scoreText.text = ("Score: " + Score);
    }
    public void Score30()
    {
        Score = Score + 30f;
        scoreText.text = ("Score: " + Score);

    }
     public void MinusScore()
    {
        Score = Score - 5f;
        scoreText.text = ("Score: " + Score);

    }

    public void score(float newScore)

    {
        Score += newScore;
        scoreText.text = ("Score: " + Score.ToString());
        
    }
    
    

}
