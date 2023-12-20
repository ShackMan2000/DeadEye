using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour
{
    [SerializeField] WaveSettings settings;

    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField] Transform spawnMarker;

    [SerializeField] Train train;

    [SerializeField] int currentWaveIndex;

    [ShowInInspector] Dictionary<SpawnSettings, int> enemiesToSpawnCurrentWave;

    [SerializeField] List<CheckPointsList> checkPointsForPaths;
    [SerializeField] List<CheckPointsList> checkPointsForLinger;


    float timeTillNextSpawn;

    float spawnIntervalCurrentWave;

    bool isSpawning;

    bool waveFailed;

    public event Action<int> OnWaveStarted = delegate { };


    void OnEnable()
    {
        GameManager.OnStartingNewWaveGame += StartNewWaveGame;
        GameManager.OnStartingNextWave += InitializeWave;
        GameManager.OnWaveFailed += OnWaveFailed;

        EnemySpawner.OnActiveEnemiesCountChanged += CheckIfWaveCompleted;
    }

    void OnDisable()
    {
        GameManager.OnStartingNewWaveGame -= StartNewWaveGame;
        GameManager.OnStartingNextWave -= InitializeWave;
        GameManager.OnWaveFailed -= OnWaveFailed;
        
        EnemySpawner.OnActiveEnemiesCountChanged -= CheckIfWaveCompleted;
    }


    void StartNewWaveGame()
    {
        enemySpawner.SetUpCheckPointsLists(checkPointsForPaths, checkPointsForLinger);

        currentWaveIndex = -1;
        InitializeWave();
    }


    public void InitializeWave()
    {
        currentWaveIndex++;

        spawnIntervalCurrentWave = settings.EnemySpawnIntervalBase - settings.EnemySpawnIntervalDecreasePerLevel * currentWaveIndex;

        foreach (var checkPointsList in checkPointsForLinger)
        {
            checkPointsList.ResetFreeIndexes();
        }

        spawnIntervalCurrentWave = settings.EnemySpawnIntervalBase - settings.EnemySpawnIntervalDecreasePerLevel * currentWaveIndex;

        CreateEnemiesToSpawnForCurrentWave();
        StartCoroutine(InitializeWaveRoutine());

        OnWaveStarted?.Invoke(currentWaveIndex);
    }

    void CreateEnemiesToSpawnForCurrentWave()
    {
        enemiesToSpawnCurrentWave = new Dictionary<SpawnSettings, int>();

        foreach (SpawnSettings spawnSettings in settings.AllEnemiesOptions)
        {
            if (spawnSettings.MinimumWaveLevel <= currentWaveIndex)
            {
                enemiesToSpawnCurrentWave.Add(spawnSettings, spawnSettings.SpawnAmountBase + spawnSettings.SpawnAmountIncreasePerLevel * currentWaveIndex);
            }
        }
    }


    IEnumerator InitializeWaveRoutine()
    {
        train.SpawnTrain();

        yield return new WaitForSeconds(train.MovementDuration);

        isSpawning = true;
    }


    void Update()
    {
        if (!isSpawning) return;

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

        enemySpawner.SpawnEnemy(selectedSetting.EnemySettings, spawnMarker.position);

        if (enemiesToSpawnCurrentWave.Count == 0)
        {
            isSpawning = false;
        }
    }


    
    void CheckIfWaveCompleted(int activeEnemiesCount)
    {

        if (activeEnemiesCount == 0 && !isSpawning)
        {
            GameManager.WaveCompleted();
        }
    }


    void OnWaveFailed()
    {
        isSpawning = false;

      enemySpawner.MakeAllEnemiesInactive();
    }
}