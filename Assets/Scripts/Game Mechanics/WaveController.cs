using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluffyUnderware.Curvy;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour
{
    public WaveSettings settings;

    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField] Transform spawnMarker;

    [SerializeField] Train train;

    public int currentWaveIndex;

    [ShowInInspector] Dictionary<SpawnSettings, int> enemiesToSpawnCurrentWave;

    [SerializeField] List<CurvySpline> splinesEntryForMainLoops;

    [SerializeField] List<CurvySpline> splinesLingerEasy;

    [SerializeField] List<CurvySpline> splinesLingerHard;

    [SerializeField] PlayerHealth playerHealth;


    [SerializeField] EnemySelector enemySelector;

    public float WarmupTime => settings.WarmupTime;

    float timeTillNextSpawn;

    float spawnIntervalCurrentWave;

    bool isSpawning;

    bool waveGameIsRunning;
    
    
    public static  event Action OnWaveCompleted = delegate { };


    void OnEnable()
    {
        playerHealth.OnHealthReduced += OnPlayerHealthReduced;
        EnemySpawner.OnActiveEnemiesCountChanged += CheckIfWaveCompleted;
        GameManager.OnGameFinished += FinishWaveGame;
    }

    void OnDisable()
    {
        playerHealth.OnHealthReduced -= OnPlayerHealthReduced;
        EnemySpawner.OnActiveEnemiesCountChanged -= CheckIfWaveCompleted;
        GameManager.OnGameFinished -= FinishWaveGame;
    }


    void Start()
    {
        List<EnemySettings> enemyOptions = new List<EnemySettings>();
        foreach (SpawnSettings spawnSettings in settings.AllEnemiesOptions)
        {
            enemyOptions.Add(spawnSettings.EnemySettings);
        }

        enemySelector.InjectAllOptions(enemyOptions);
        enemySelector.SetAllEnemiesSelected();

        gameObject.SetActive(false);
    }

    public void StartNewWaveGame()
    {
        gameObject.SetActive(true);

        GameManager.StartNewWaveGame();

        playerHealth.ResetHealth();

        enemySpawner.SetUpCurvyPaths(splinesEntryForMainLoops, splinesLingerEasy, splinesLingerHard);

        currentWaveIndex = -1;

        waveGameIsRunning = true;
        StartCoroutine(InitializeWaveRoutine());
    }


    void CreateEnemiesToSpawnForCurrentWave()
    {
        enemiesToSpawnCurrentWave = new Dictionary<SpawnSettings, int>();

        bool ignoreSelection = enemySelector.SelectedEnemies.Count == 0;

        foreach (SpawnSettings spawnSettings in settings.AllEnemiesOptions)
        {
            if (spawnSettings.MinimumWaveLevel <= currentWaveIndex)
            {
                if (enemySelector.SelectedEnemies.Contains(spawnSettings.EnemySettings) || ignoreSelection)
                {
                    int spawnAmount = spawnSettings.GetSpawnAmountForWaveLevel(currentWaveIndex);
                    enemiesToSpawnCurrentWave.Add(spawnSettings, spawnAmount);
                }
            }
        }


        if (enemiesToSpawnCurrentWave.Count == 0)
        {
            foreach (SpawnSettings spawnSettings in settings.AllEnemiesOptions)
            {
                if (spawnSettings.EnemySettings == enemySelector.SelectedEnemies[0])
                {
                    int spawnAmount = spawnSettings.GetSpawnAmountForWaveLevel(currentWaveIndex);
                    enemiesToSpawnCurrentWave.Add(spawnSettings, spawnAmount);
                }
            }
        }
    }


    IEnumerator InitializeWaveRoutine(float delay = 0f)
    {
        currentWaveIndex++;

        spawnIntervalCurrentWave = settings.EnemySpawnIntervalBase - settings.EnemySpawnIntervalDecreasePerLevel * currentWaveIndex;
        // need a min!

        enemySpawner.FreeAllSplines();

        CreateEnemiesToSpawnForCurrentWave();

        if (delay > 0.1f)
        {
            yield return new WaitForSeconds(delay);
        }

        train.MoveTrainIntoScene();

        yield return new WaitForSeconds(WarmupTime);

        isSpawning = true;
    }


    void Update()
    {
        if (!waveGameIsRunning)
        {
            return;
        }

        if (!isSpawning) return;

        if (GameManager.IsPaused) return;

        timeTillNextSpawn -= Time.deltaTime;

        if (timeTillNextSpawn <= 0)
        {
            SpawnRandomEnemy();
            timeTillNextSpawn = spawnIntervalCurrentWave;
        }
    }


    void SpawnRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemiesToSpawnCurrentWave.Count);

        SpawnSettings selectedSetting = enemiesToSpawnCurrentWave.ElementAt(randomIndex).Key;

        enemiesToSpawnCurrentWave[selectedSetting]--;
        if (enemiesToSpawnCurrentWave[selectedSetting] <= 0)
        {
            enemiesToSpawnCurrentWave.Remove(selectedSetting);
        }

        enemySpawner.SpawnEnemy(selectedSetting.EnemySettings, false);

        if (enemiesToSpawnCurrentWave.Count == 0)
        {
            isSpawning = false;
        }
    }


    void OnPlayerHealthReduced()
    {
        // just in case bullet hits the same time
        if (!waveGameIsRunning)
        {
            return;
        }

        if (playerHealth.CurrentHealth <= 0 && playerHealth.MaxHealth != 0)
        {
            GameManager.WaveFailed();
            FinishWaveGame();
        }
    }

    
    // this way can also be called through quit in pause menu and it won't count as a failed game
    void FinishWaveGame()
    {
        waveGameIsRunning = false;
        isSpawning = false;
        gameObject.SetActive(false);
        
        StopAllCoroutines();
        train.MoveTrainOutOfScene();
    }


    void CheckIfWaveCompleted(int activeEnemiesCount)
    {
        if (!waveGameIsRunning)
        {
            return;
        }

        if (activeEnemiesCount == 0 && !isSpawning)
        {
            OnWaveCompleted?.Invoke();
            train.MoveTrainOutOfScene();
            StartCoroutine(InitializeWaveRoutine(3f));
        }
    }


  
}