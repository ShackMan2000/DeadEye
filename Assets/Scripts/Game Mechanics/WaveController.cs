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

    [SerializeField] int currentWaveIndex;

    [ShowInInspector] Dictionary<SpawnSettings, int> enemiesToSpawnCurrentWave;

    [SerializeField] List<CurvySpline> splinesEntryForMainLoops;

    [SerializeField] List<CurvySpline> splinesLingerEasy;

    [SerializeField] List<CurvySpline> splinesLingerHard;


    float timeTillNextSpawn;

    float spawnIntervalCurrentWave;

    bool isSpawning;

    bool waveFailed;



    void OnEnable()
    {
        GameManager.OnStartingNewWaveGame += StartNewWaveGame;
        GameManager.OnStartingWave += InitializeWave;
        GameManager.OnWaveFailed += OnWaveFailed;

        EnemySpawner.OnActiveEnemiesCountChanged += CheckIfWaveCompleted;
    }

    void OnDisable()
    {
        GameManager.OnStartingNewWaveGame -= StartNewWaveGame;
        GameManager.OnStartingWave -= InitializeWave;
        GameManager.OnWaveFailed -= OnWaveFailed;

        EnemySpawner.OnActiveEnemiesCountChanged -= CheckIfWaveCompleted;
    }


    void StartNewWaveGame()
    {
        enemySpawner.SetUpCurvyPaths(splinesEntryForMainLoops, splinesLingerEasy, splinesLingerHard);

        currentWaveIndex = -1;
    }


    public void InitializeWave()
    {
        currentWaveIndex++;

        spawnIntervalCurrentWave = settings.EnemySpawnIntervalBase - settings.EnemySpawnIntervalDecreasePerLevel * currentWaveIndex;

        enemySpawner.FreeAllSplines();
        
        // foreach (var checkPointsList in checkPointsForLinger)
        // {
        //     checkPointsList.ResetFreeIndexes();
        // }

        spawnIntervalCurrentWave = settings.EnemySpawnIntervalBase - settings.EnemySpawnIntervalDecreasePerLevel * currentWaveIndex;

        CreateEnemiesToSpawnForCurrentWave();
        StartCoroutine(InitializeWaveRoutine());

        //OnWaveStarted?.Invoke(currentWaveIndex);
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

        enemySpawner.SpawnEnemy(selectedSetting.EnemySettings, false);

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