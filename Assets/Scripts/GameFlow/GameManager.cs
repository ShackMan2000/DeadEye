using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public static class GameManager
{
    public static bool ShootingModeActive;
    public static bool GameOver;


    // kinda starting to think the better solution would have been an abstract class with most of that stuff here
    // with wavecontroller and time trial controller inheriting.


    public static event Action OnEnterShootingMode = delegate { };
    public static event Action OnExitShootingMode = delegate { };

    public static event Action OnStartingNewWaveGame = delegate { };
    public static event Action OnStartingWave = delegate { };
    public static event Action OnWaveCompleted = delegate { };
    public static event Action OnWaveFailed = delegate { };

    public static event Action OnStartingNewTimeTrialGame = delegate { };
    public static event Action OnTimeTrialCompleted = delegate { };
    public static event Action OnTimeTrialFailed = delegate { };



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



    // need to inform that player got killed, and then either wave mode or time trial reacts to it
    // either they are listening to it, or save mode here.
    // since there are only 2 modes, they should do it

    // also need to take into account that player might exit the wave game through the quit button, in 

    public static void WaveCompleted()
    {
        if (ShootingModeActive)
        {
            ExitShootingGameMode();
            OnWaveCompleted?.Invoke();
        }
        else
        {
            Debug.Log("Trying to call Wave completed, but game mode is not active. Should never happen");
        }
    }


    public static void WaveFailed()
    {
        if (ShootingModeActive)
        {
            GameOver = true;
            ExitShootingGameMode();
            OnWaveFailed?.Invoke();
        }
        else
        {
            Debug.Log("Trying to call Wave failed, but game mode is not active. Should never happen");
        }
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
}
