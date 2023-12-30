using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static bool ShootingModeActive;
    public static bool GameOver;


    [SerializeField] PlayerHealth playerHealth;

    // right now this is only used by the guns/controller toggle
    public static event Action OnEnterShootingMode = delegate { };
    public static event Action OnExitShootingMode = delegate { };

    public static event Action OnStartingNewWaveGame = delegate { };
    public static event Action OnStartingWave = delegate { };

    public static event Action OnStartingNewTimeTrialGame = delegate { };
    public static event Action OnTimeTrialFinished = delegate { };

    public static event Action OnWaveCompleted = delegate { };
    public static event Action OnWaveFailed = delegate { };


    void OnEnable()
    {
        playerHealth.OnHealthReduced += OnPlayerHealthReduced;
        OnStartingNewWaveGame += ResetHealth;
        OnStartingNewTimeTrialGame += ResetHealth;
    }


    void OnDisable()
    {
        playerHealth.OnHealthReduced -= OnPlayerHealthReduced;
        OnStartingNewWaveGame -= ResetHealth;
        OnStartingNewTimeTrialGame -= ResetHealth;
    }

    
    void ResetHealth()
    {
        playerHealth.ResetHealth();
    }
    
    void OnPlayerHealthReduced(int health)
    {
        if (health <= 0)
        {
            WaveFailed();
        }
    }


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


    public static void StartNewTimeTrialGame()
    {
        GameOver = false;
        EnterShootingMode();
        
        OnStartingNewTimeTrialGame?.Invoke();
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

    public static void FinishTimeTrialGame()
    {
        Debug.Log("FinishTimeTrialGame");
        ExitShootingGameMode();
        OnTimeTrialFinished?.Invoke();
    }

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
}


