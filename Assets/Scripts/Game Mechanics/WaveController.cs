using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour
{
    // keeps track of each wave, listens to wave game mode to start and to reset wave back to 0

    // spawns all enemies and also controls how often they shoot, so it's not too many at the same time


    [SerializeField] WaveSettings settings;

    [SerializeField] Transform spawnMarker;

    [SerializeField] Train train;

    [SerializeField] int currentWaveIndex;

    [SerializeField] List<EnemyBase> activeEnemies;

    List<PrefabsWithSettingsOptions> enemyOptionsForCurrentWave;

    [SerializeField] List<CheckPointsList> checkPointsLists;
    
    // for spawning tripplets etc. could disable the interval but add it to the max so the next one won't spawn too fast

    [SerializeField] GameObject testEnemy;

    float timeTillNextSpawn;
    int enemiesToSpawnCurrentWave;

    bool isSpawning;


    // need to get a list of prefabs that have settings that are available for this wave
    // nah, all prefabs are available. Actually should rather get the settings for this wave. 
    // so each wave simply has one setting for the prefab. Just order the list based on that...

    [Button]
    void InitializeWave()
    {
        enemiesToSpawnCurrentWave = settings.EnemyCountBase + settings.EnemyCountIncreasePerLevel * currentWaveIndex;
        StartCoroutine(InitializeWaveRoutine());
    }


    IEnumerator InitializeWaveRoutine()
    {
        CreateEnemyOptionsForCurrentWave();


        train.SpawnTrain();

        yield return new WaitForSeconds(train.MovementDuration);

        isSpawning  = true;
        
        activeEnemies = new List<EnemyBase>();

        
      

    }

    void CreateEnemyOptionsForCurrentWave()
    {
        enemyOptionsForCurrentWave = new List<PrefabsWithSettingsOptions>();


        foreach (var enemy in settings.AllEnemiesWithSettingsOptions)
        {
            if (enemy.SettingsOptions.Count == 0)
            {
                Debug.LogError("Enemy has no settings options");
                continue;
            }

            PrefabsWithSettingsOptions newEnemyOption = new PrefabsWithSettingsOptions();
            newEnemyOption.SettingsOptions = new List<EnemySettings>();
            newEnemyOption.EnemyPrefab = enemy.EnemyPrefab;

            EnemySettings highestAvailableSettings = enemy.SettingsOptions[0];

            foreach (var settingsOption in enemy.SettingsOptions)
            {
                if (settingsOption.minWaveLevel <= currentWaveIndex)
                {
                    if (settingsOption.minWaveLevel > highestAvailableSettings.minWaveLevel)
                    {
                        highestAvailableSettings = settingsOption;
                    }
                }
            }

            newEnemyOption.SettingsOptions.Add(highestAvailableSettings);

            enemyOptionsForCurrentWave.Add(newEnemyOption);
        }
    }


    void Update()
    {
        if(!isSpawning) return;
        
        timeTillNextSpawn -= Time.deltaTime;
        
        // could trigger a couroutine that spawns the train or the group that moves to positions
        if (timeTillNextSpawn <= 0)
        {
            SpawnEnemy();
            //adjust later
            timeTillNextSpawn = settings.EnemySpawnIntervalBase;
        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyOptionsForCurrentWave.Count);
        EnemyBase newEnemy = Instantiate(enemyOptionsForCurrentWave[randomIndex].EnemyPrefab);    
        newEnemy.transform.SetParent(transform);
        newEnemy.transform.position = spawnMarker.position;
        activeEnemies.Add(newEnemy);
        
        
        newEnemy.Initialize(enemyOptionsForCurrentWave[randomIndex].SettingsOptions[0], checkPointsLists[0]);
      
        
            
        enemiesToSpawnCurrentWave--;
        if (enemiesToSpawnCurrentWave <= 0)
        {
            isSpawning = false;
        }
    }
}