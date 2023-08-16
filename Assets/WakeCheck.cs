using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeCheck : MonoBehaviour
{
    private const string wakeKey = "HasAwoken"; // The key for PlayerPrefs

    public bool hasAwoken = false;

    private void Awake()
    {
        // Check if the object has awoken before
        if (PlayerPrefs.HasKey(wakeKey))
        {
            hasAwoken = true;
        }

        // If it hasn't awoken before, set the PlayerPrefs and disable the object
        if (!hasAwoken)
        {
            PlayerPrefs.SetString(wakeKey, "I've woken");
            PlayerPrefs.Save();
            hasAwoken = true;
            gameObject.SetActive(false);
        }
    }
}
