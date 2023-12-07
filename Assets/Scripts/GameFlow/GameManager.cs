using System;
using System.Security.AccessControl;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [ShowInInspector]
    public static GameMode SelectedMode;

    public static event Action OnGameStarted = delegate { };
    
    public void SelectGameMode(GameMode mode)
    {
        SelectedMode = mode;
    }

    
    // user should first see a button that starts a wave game
    // which triggers to start the next wave. 
    // Just have UI controller check the wave controller if it is above 1, in that case show next wave or quit game button
    
    
    // bigger question is how to make sure that all is hooked up to the prefab... or better have the UI controller above all that.
    
    
    [Button]
    public void StartNewWaveGame()
    {
        OnGameStarted();
    }
  
    // a wave based mode with lives.
    // a time based mode without lives. Could just spawn every time the player has shot an enemy, and maybe make some disappear? Maybe they go back into the gate



}


public enum GameMode
{
    Waves,
    TimeTrial
}