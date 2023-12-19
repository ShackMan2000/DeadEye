using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    // only use if there are too many events to keep track of
   // public static GameMode CurrentGameMode { get; private set; }


    public static bool GameModeActive;
    public static bool GameOver;

    public static event Action OnEnterGameMode = delegate { }; 
    public static event Action OnExitGameMode = delegate { };
    
    public static event Action OnStartingNewWaveGame = delegate { };
    public static event Action OnStartingNextWave = delegate { };
    
    public static event Action OnWaveCompleted = delegate { };
    public static event Action OnWaveFailed = delegate { };


    // the game mode might be overkill...
    
    // when game starts, UI needs to change. Actually that one needs to know which mode...
    // health counter needs to know game mode too. Though if enemies in time trial don't shoot, doesn't matter if it's in the scene or not
    // 
    // controllers turn into guns, and vice versa when game is over
    // should at least still all route through Game Manager
    
    
    
    // user should first see a button that starts a wave game
    // which triggers to start the next wave. 
    // Just have UI controller check the wave controller if it is above 1, in that case show next wave or quit game button
    
    
    // bigger question is how to make sure that all is hooked up to the prefab... or better have the UI controller above all that.
    
    
    [Button]
    public static void EnterGameMode()
    {
        GameModeActive = true;
        OnEnterGameMode?.Invoke();
    }


    [Button]
    public static void ExitGameMode()
    {
        GameModeActive = false;
        OnExitGameMode?.Invoke();
    }

    
    public static void StartNewWaveGame()
    {
        GameOver = false;
        EnterGameMode();
        OnStartingNewWaveGame?.Invoke();
    }

    public static void StartNextWave()
    {
        EnterGameMode();
        OnStartingNextWave?.Invoke();
    }
    

    public static void WaveCompleted()
    {
        if (GameModeActive)
        {
            ExitGameMode();
            OnWaveCompleted?.Invoke();
        }
        else
        {
            Debug.Log("Trying to call Wave completed, but game mode is not active. Should never happen");
        }
    }

    public static void WaveFailed()
    {
        if (GameModeActive)
        {   
            GameOver = true;
            ExitGameMode();
            OnWaveFailed?.Invoke();
        }
        else
        {
            Debug.Log("Trying to call Wave failed, but game mode is not active. Should never happen");
        }
    }
}


public enum GameMode
{
    Waves,
    TimeTrial
}