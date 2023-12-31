using System;
using System.Collections.Generic;
using System.Linq;
using FluffyUnderware.Curvy;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class TimeTrialManager : MonoBehaviour
{
    [SerializeField] SelectableOption timeOptions;

    [SerializeField] WaveSettings waveSettings;

    [SerializeField] List<CurvySpline> splines;

    [SerializeField] PlayerHealth playerHealth;

    float timeTillNextSpawn;
    public float StartTimeInSeconds => timeOptions.SelectedValue * 60;

    public int MaxActiveActiveEnemies => waveSettings.MaxActiveEnemies;

    float timeLeft;

    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField] TextMeshProUGUI timeLeftText;

    bool gameIsRunning;


    [ShowInInspector]
    List<EnemySettings> enemiesToSpawnCurrentBatch;


    void OnEnable()
    {
        GameManager.OnStartingNewTimeTrialGame += OnStartingNewTimeTrialGame;
        playerHealth.OnHealthReduced += OnPlayerHealthReduced;
    }

    void OnDisable()
    {
        GameManager.OnStartingNewTimeTrialGame -= OnStartingNewTimeTrialGame;
        playerHealth.OnHealthReduced -= OnPlayerHealthReduced;
    }


    void OnStartingNewTimeTrialGame()
    {
        timeLeft = StartTimeInSeconds;
        gameIsRunning = true;
        timeLeftText.gameObject.SetActive(true);

        playerHealth.ResetHealth();

        enemySpawner.SetUpCurvyPaths(splines, null, null);
    }


    void SpawnRandomEnemy()
    {
        if (enemySpawner.GetActiveEnemiesCount() >= MaxActiveActiveEnemies)
        {
            return;
        }


        // create new batch
        if (enemiesToSpawnCurrentBatch == null || enemiesToSpawnCurrentBatch.Count == 0)
        {
            enemiesToSpawnCurrentBatch = new List<EnemySettings>();

            foreach (var option in waveSettings.AllEnemiesOptions)
            {
                for (int i = 0; i < option.SpawnAmountBase; i++)
                {
                    enemiesToSpawnCurrentBatch.Add(option.EnemySettings);
                }
            }
        }

        if (enemiesToSpawnCurrentBatch.Count == 0)
        {
            Debug.LogError("No enemies to spawn");
            return;
        }


        int randomIndex = Random.Range(0, enemiesToSpawnCurrentBatch.Count);

        EnemySettings selectedSetting = enemiesToSpawnCurrentBatch[randomIndex];

        enemiesToSpawnCurrentBatch.RemoveAt(randomIndex);


        if (selectedSetting.MovementType == EnemyMovementType.Linger)
        {
            Debug.LogError("Linger enemies not supported in time trial");
            return;
        }

        enemySpawner.SpawnEnemy(selectedSetting, true);
    }


    void Update()
    {
        if (gameIsRunning)
        {
            timeLeft -= Time.deltaTime;


            timeLeftText.text = timeLeft.ToString("F1") + "s";

            if (timeLeft <= 0)
            {
                timeLeftText.text = "0.0s";
                gameIsRunning = false;
                enemySpawner.MakeAllEnemiesInactive();

                GameManager.TimeTrialCompleted();
            }

            timeTillNextSpawn -= Time.deltaTime;

            if (timeTillNextSpawn <= 0)
            {
                SpawnRandomEnemy();
                timeTillNextSpawn = Random.Range(waveSettings.SpawnIntervalMin, waveSettings.SpawnIntervalMax);
            }
        }
    }


    void OnPlayerHealthReduced()
    {
        if (gameIsRunning == false)
        {
            Debug.LogError("Player health reduced while game is not running");
            return;
        }

        if (playerHealth.CurrentHealth <= 0 && playerHealth.MaxHealth != 0)
        {
            gameIsRunning = false;
            GameManager.TimeTrialFailed();
        }
    }
}