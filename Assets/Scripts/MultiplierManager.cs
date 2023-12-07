using System;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierManager : MonoBehaviour
{
    public Text scoreText;
    public Text multiplierText;

    public event Action<bool> OnSuccessfulHit; // true for successful, false for unsuccessful
    public event Action<int> OnMultiplierActivated; // the activated multiplier

    public uint score = 0;
    private int consecutiveHits = 0;
    public int multiplier = 1;
    public uint points; 

    private void Start()
    {
        scoreText = GameObject.FindWithTag("SCORE").GetComponent<Text>();

        UpdateScoreText();
        UpdateMultiplierText();
    }

    public void RegisterHit(bool isSuccessful)
    {
        if (isSuccessful)
        {
            consecutiveHits++;
            if (consecutiveHits == 5)
            {
                multiplier++;
                consecutiveHits = 0;
                OnMultiplierActivated?.Invoke(multiplier);
                UpdateMultiplierText();
            }
            score += (uint)(multiplier * 10);
            OnSuccessfulHit?.Invoke(true);
        }
        else
        {
            consecutiveHits = 0;
            OnSuccessfulHit?.Invoke(false);
            
        }

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    private void UpdateMultiplierText()
    {
        multiplierText.text = multiplier > 1 ? multiplier + "X Multiplier" : "";
    }
}
