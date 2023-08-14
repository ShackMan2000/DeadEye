using UnityEngine;
using UnityEngine.UI;
using System;

public class UpdateTimeText : MonoBehaviour
{
    public Text timeText;

    private void Start()
    {
        // Make sure a Text component is assigned to the script in the Inspector
        if (timeText == null)
        {
            Debug.LogError("Time Text component is not assigned!");
            return;
        }

        // Call the UpdateTime function immediately and then every second
        UpdateTime();
        InvokeRepeating("UpdateTime", 1f, 1f);
    }

    private void UpdateTime()
    {
        // Get the current system time
        DateTime currentTime = DateTime.Now;

        // Convert the time to a string format
        string formattedTime = currentTime.ToString("HH:mm:ss");

        // Update the Text component with the formatted time
        timeText.text = "Current Time: " + formattedTime;
    }
}