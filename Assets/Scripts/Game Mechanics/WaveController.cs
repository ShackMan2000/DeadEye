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

    [SerializeField] List<CheckPointsList> checkPointsForPaths;
    [SerializeField] List<CheckPointsList> checkPointsForLinger;

    [SerializeField] CurvySpline splinesLingerEasyAvailable;
    [SerializeField]    CurvySpline splinesLingerHardReserved;
    
    [SerializeField]    CurvySpline splinesLingerHardAvailable;
    [SerializeField]    CurvySpline splinesLingerEasyReserved;

    // allright, need a list of paths...

    float timeTillNextSpawn;

    float spawnIntervalCurrentWave;

    bool isSpawning;

    bool waveFailed;

    
    // for path based enemies (balls and shooter) have a list of entry paths, each will lead to a main loop. Have like 3 or 4 and that's good enough. 
    // make one where they will be opposite, that would be cool for the left and right enemies.
    // also name the paths, like swoosh at player, high in the sky, midrange
    
    
    // for the linger ones, need to reserve points as before (could delay spawning if not enough points, but have like 20 just in case)
    // question is then first how to get the entry path from the linger path... and how to make sure it will pick that path when it reaches the connection
    // doesn't matter for now... 
    // could just have a lot of paths... SIMPLEST
    
    // okay do that for now, when stress test worked can do the branching and all that...
    
    

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
        enemySpawner.SetUpCheckPointsLists(checkPointsForPaths, checkPointsForLinger);

        currentWaveIndex = -1;
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