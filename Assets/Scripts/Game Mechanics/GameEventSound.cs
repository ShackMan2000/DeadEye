using System;
using System.Collections.Generic;
using UnityEngine;


public class GameEventSound : MonoBehaviour
{

    
    [SerializeField] AudioSource successAudioSource;
    [SerializeField] AudioSource failAudioSource;


    void OnEnable()
    {
        GameManager.OnWaveGameFailed += PlayFailSound;
        GameManager.OnTimeTrialFailed += PlayFailSound;
        GameManager.OnTimeTrialSuccess += PlaySuccessSound;
        WaveController.OnWaveCompleted += PlaySuccessSound;
    }
    
    void OnDisable()
    {
        GameManager.OnWaveGameFailed -= PlayFailSound;
        GameManager.OnTimeTrialFailed -= PlayFailSound;
        GameManager.OnTimeTrialSuccess -= PlaySuccessSound;
        WaveController.OnWaveCompleted -= PlaySuccessSound;
    }
    
    void PlaySuccessSound()
    {
        successAudioSource.Play();
    }
    
    void PlayFailSound()
    {
        failAudioSource.Play();
    }
}