using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public static class GameManager
{
    public static bool ShootingModeActive;
    public static bool GameOver;

    public static bool IsPaused;


    public static event Action OnEnterShootingMode = delegate { };
    public static event Action OnExitShootingMode = delegate { };

    public static event Action OnStartingNewWaveGame = delegate { };

    public static event Action OnStartingWave = delegate { };
    //  public static event Action OnWaveCompleted = delegate { };

    public static event Action OnWaveGameFailed = delegate { };
    //public static event Action OnWaveGameFinished = delegate { };
    // 

    public static event Action OnStartingNewTimeTrialGame = delegate { };
    public static event Action OnTimeTrialCompleted = delegate { };
    public static event Action OnTimeTrialFailed = delegate { };

    public static event Action OnGamePaused = delegate { };
    public static event Action OnGameResumed = delegate { };

    public static event Action OnGameFinished = delegate { };


    [Button]
    public static void EnterShootingMode()
    {
        ShootingModeActive = true;
        OnEnterShootingMode?.Invoke();
    }

    [Button]
    public static void ExitShootingGameMode()
    {
        ShootingModeActive = false;
        OnExitShootingMode?.Invoke();
    }


    public static void StartNewWaveGame()
    {
        GameOver = false;
        OnStartingNewWaveGame?.Invoke();
        StartWave();
    }


    public static void StartWave()
    {
        EnterShootingMode();
        OnStartingWave?.Invoke();
    }


    // public static void WaveCompleted()
    // {
    //     if (ShootingModeActive)
    //     {
    //         ExitShootingGameMode();
    //         OnWaveCompleted?.Invoke();
    //     }
    //     else
    //     {
    //         Debug.Log("Trying to call Wave completed, but game mode is not active. Should never happen");
    //     }
    // }


    public static void WaveFailed()
    {
        if (ShootingModeActive)
        {
            GameOver = true;
            ExitShootingGameMode();
            // OnWaveGameFinished?.Invoke();
            OnWaveGameFailed?.Invoke();
            OnGameFinished?.Invoke();
        }
        else
        {
            Debug.Log("Trying to call Wave failed, but game mode is not active. Should never happen");
        }
    }


    //use that for panel, quite in pause menu
    public static void QuitGameThroughPauseMenu()
    {
        GameOver = true;
        IsPaused = false;
        OnGameFinished?.Invoke();
    }


    public static void StartNewTimeTrialGame()
    {
        GameOver = false;
        EnterShootingMode();

        OnStartingNewTimeTrialGame?.Invoke();
    }


    public static void TimeTrialCompleted()
    {
        GameOver = true;
        ExitShootingGameMode();
        OnTimeTrialCompleted?.Invoke();
    }

    public static void TimeTrialFailed()
    {
        GameOver = true;
        ExitShootingGameMode();
        OnTimeTrialFailed?.Invoke();
    }

    public static void PauseGame()
    {
        IsPaused = true;
        ExitShootingGameMode();
        OnGamePaused?.Invoke();
    }


    public static void ResumeGame()
    {
        IsPaused = false;
        EnterShootingMode();
        OnGameResumed?.Invoke();
    }
}