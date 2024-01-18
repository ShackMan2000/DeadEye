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

    [SerializeField] float timeLeft;

    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField] TextMeshProUGUI timeLeftText;

    bool gameIsRunning;


    [ShowInInspector]
    List<EnemySettings> enemiesToSpawnCurrentBatch;


    [SerializeField] EnemySelector enemySelector;

    void OnEnable()
    {
        GameManager.OnGameFinished += FinishGame;
        playerHealth.OnHealthReduced += OnPlayerHealthReduced;
    }

    void OnDisable()
    {
        GameManager.OnGameFinished -= FinishGame;
        playerHealth.OnHealthReduced -= OnPlayerHealthReduced;
    }


    void Start()
    {
        List<EnemySettings> enemyOptions = new List<EnemySettings>();
        foreach (SpawnSettings spawnSettings in waveSettings.AllEnemiesOptions)
        {
            enemyOptions.Add(spawnSettings.EnemySettings);
        }

        enemySelector.InjectAllOptions(enemyOptions);
        enemySelector.SetAllEnemiesSelected();
        
        gameObject.SetActive(false);
    }


    public void StartNewTimeTrialGame()
    {
        gameObject.SetActive(true);
        
        GameManager.StartNewTimeTrialGame();
        
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
                if (enemySelector.SelectedEnemies.Contains(option.EnemySettings))
                {
                    for (int i = 0; i < option.SpawnAmountBase; i++)
                    {
                        enemiesToSpawnCurrentBatch.Add(option.EnemySettings);
                    }
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
        if(GameManager.IsPaused) return;
        
        if (gameIsRunning)
        {
            timeTillNextSpawn -= Time.deltaTime;

            if (timeTillNextSpawn <= 0)
            {
                SpawnRandomEnemy();
                timeTillNextSpawn = Random.Range(waveSettings.SpawnIntervalMin, waveSettings.SpawnIntervalMax);
            }
            
            
            
            timeLeft -= Time.deltaTime;

            timeLeftText.text = timeLeft.ToString("F1") + "s";

            if (timeLeft <= 0)
            {
                timeLeftText.text = "0.0s";
                gameIsRunning = false;
                enemySpawner.MakeAllEnemiesInactive();

                GameManager.TimeTrialSuccess();
                gameObject.SetActive(false);
            }

        }
    }


    void OnPlayerHealthReduced()
    {
        if (gameIsRunning == false)
        {
            //  Debug.LogError("Player health reduced while game is not running");
            return;
        }

        if (playerHealth.CurrentHealth <= 0 && playerHealth.MaxHealth != 0)
        {
            gameIsRunning = false;
            GameManager.TimeTrialFailed();
            gameObject.SetActive(false);
        }
    }
    
    
    void FinishGame()
    {
        gameIsRunning = false;
    }
}