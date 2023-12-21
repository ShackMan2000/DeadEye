using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameModeActive;
    public static bool GameOver;

    
    // right now this is only used by the guns/controller toggle
    public static event Action OnEnterGameMode = delegate { }; 
    public static event Action OnExitGameMode = delegate { };
    
    public static event Action OnStartingNewWaveGame = delegate { };
    public static event Action OnStartingNextWave = delegate { };
    
    public static event Action OnStartingNewTimeTrialGame = delegate { };
    public static event Action OnTimeTrialFinished = delegate { };
    
    public static event Action OnWaveCompleted = delegate { };
    public static event Action OnWaveFailed = delegate { };


    
    
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

    
    public static void StartNewTimeTrialGame()
    {
        GameOver = false;
        EnterGameMode();
        OnStartingNewTimeTrialGame?.Invoke();
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
    
    public static void FinishTimeTrialGame()
    {
        Debug.Log("FinishTimeTrialGame");
        ExitGameMode();
        OnTimeTrialFinished?.Invoke();
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