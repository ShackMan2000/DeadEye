using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour
{
    [SerializeField] WaveSettings settings;

    [SerializeField] Transform spawnMarker;

    [SerializeField] Train train;

    [SerializeField] int currentWaveIndex;

    [SerializeField] List<EnemyBase> activeEnemies;

    [ShowInInspector]
    Dictionary<SpawnSettings, int> enemiesToSpawnCurrentWave;

    [SerializeField] List<CheckPointsList> checkPointsLists;

    float timeTillNextSpawn;
    
    float spawnIntervalCurrentWave;

    bool isSpawning;


    
    
    
    
    
    [Button]
    void InitializeWave()
    {
        spawnIntervalCurrentWave = settings.EnemySpawnIntervalBase - settings.EnemySpawnIntervalDecreasePerLevel * currentWaveIndex;
        CreateEnemiesToSpawnForCurrentWave();
        StartCoroutine(InitializeWaveRoutine());
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

        isSpawning  = true;
        
        activeEnemies = new List<EnemyBase>();
    }
    



    void Update()
    {
        if(!isSpawning) return;
        
        timeTillNextSpawn -= Time.deltaTime;
        
        if (timeTillNextSpawn <= 0)
        {
            SpawnRandomEnemy();
            timeTillNextSpawn = spawnIntervalCurrentWave;
        }
    }

    
    void SpawnRandomEnemy()
    {
        //int randomIndex = Random.Range(0, enemiesToSpawnCurrentWave.Count);
        
        // get random item from dictionary
        int randomIndex = Random.Range(0, enemiesToSpawnCurrentWave.Count);

       SpawnSettings selectedSetting = enemiesToSpawnCurrentWave.ElementAt(randomIndex).Key;
       
       enemiesToSpawnCurrentWave[selectedSetting]--;
         if (enemiesToSpawnCurrentWave[selectedSetting] <= 0)
         {
             enemiesToSpawnCurrentWave.Remove(selectedSetting);
         }



         EnemyBase prefab;
         if (selectedSetting.EnemyPrefabNeutral != null)
         {
             prefab = selectedSetting.EnemyPrefabNeutral;
         }
         else
         {
             prefab = Random.Range(0, 2) == 0 ? selectedSetting.EnemyPrefabLeft : selectedSetting.EnemyPrefabRight;
         }
         
         if(prefab == null)
         {
             Debug.LogError("Trying to spawn a prefab but it's null");
             return;
         }
         
        EnemyBase newEnemy = Instantiate(prefab);    
        newEnemy.transform.SetParent(transform);
        newEnemy.transform.position = spawnMarker.position;
        activeEnemies.Add(newEnemy);
        
        
        newEnemy.Initialize(selectedSetting.EnemySettings, checkPointsLists[Random.Range(0, checkPointsLists.Count)]);
      
            Debug.Log("enemiestospawn: " + enemiesToSpawnCurrentWave.Count);
        if (enemiesToSpawnCurrentWave.Count == 0)
        {
            Debug.Log("wave finished");
            isSpawning = false;
        }
    }



    [Button]
    void DestroyRandomEnemy()
    {
        
    }
}