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
        StartCoroutine(InitializeWaveRoutine());
    }


    IEnumerator InitializeWaveRoutine()
    {
        CreateEnemyOptionsForCurrentWave();


        train.SpawnTrain();

        yield return new WaitForSeconds(train.MovementDuration);

        isSpawning  = true;
        
        activeEnemies = new List<EnemyBase>();

        
        // could  do some extra stuff, like spawn a lot of balls and have them move to a position each, that would look cool.
        
        // have them move in trains, especially having a left and right train moving at the same time.

        // start simple, spawnCounter, spawn one after another
        // give each enemy a path and if random or follow the path
        // later use multiple paths
        // or give them a point and they just linger there.


        // next is calculate who is going to shoot


        // spawn enemies and add them to the list
        // add a listener to each enemy when they get destroyed


        // need some kind of movement for the enemies, could just use a rotation for now, something related to depth


        // depth enemies will have special movement, either rotating around each other on special axis or linear back and forth
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
        EnemyBase newEnemy = Instantiate(enemyOptionsForCurrentWave[Random.Range(0, enemyOptionsForCurrentWave.Count)].EnemyPrefab);    
        newEnemy.transform.SetParent(transform);
        newEnemy.transform.position = spawnMarker.position;
        activeEnemies.Add(newEnemy);
            
        enemiesToSpawnCurrentWave--;
        if (enemiesToSpawnCurrentWave <= 0)
        {
            isSpawning = false;
        }
    }
}