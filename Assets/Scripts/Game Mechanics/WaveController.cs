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

    [SerializeField] Transform spawnMarker;

    [SerializeField] Train train;

    [SerializeField] int currentWaveIndex;

    [ShowInInspector] Dictionary<SpawnSettings, int> enemiesToSpawnCurrentWave;

    [FormerlySerializedAs("checkPointsLists")] [SerializeField] List<CheckPointsList> checkPointsForPaths;
    [SerializeField] CheckPointsList checkPointsForLinger;

    [ShowInInspector] List<int> availableIndexesForLinger;
    Dictionary<EnemyBase, int> activeEnemiesLingerIndex = new Dictionary<EnemyBase, int>();

    [ShowInInspector] Dictionary<EnemyBase, List<EnemyBase>> inactiveEnemies = new Dictionary<EnemyBase, List<EnemyBase>>();
    [ShowInInspector] Dictionary<EnemyBase, List<EnemyBase>> activeEnemies = new Dictionary<EnemyBase, List<EnemyBase>>();

    float timeTillNextSpawn;

    float spawnIntervalCurrentWave;

    bool isSpawning;

    
    [SerializeField] bool DEBUGIncreaseHitbox;

    void OnEnable()
    {
        EnemyBase.OnAnyEnemyDestroyedPrefabType += OnEnemyDestroyedPrefabType;
    }

    void OnDisable()
    {
        EnemyBase.OnAnyEnemyDestroyedPrefabType -= OnEnemyDestroyedPrefabType;
    }

    [Button]
    void InitializeWaveDebug(bool increaseHitbox)
    {
        DEBUGIncreaseHitbox = increaseHitbox;
        InitializeWave();
    }


    public void StartNewWaveGame()
    {
        currentWaveIndex = 1;
        InitializeWave();
    }
    
    public void StartNextWave()
    {
        currentWaveIndex++;
        InitializeWave();
    }
    
    
   public void InitializeWave()
    {
        spawnIntervalCurrentWave = settings.EnemySpawnIntervalBase - settings.EnemySpawnIntervalDecreasePerLevel * currentWaveIndex;

        
        availableIndexesForLinger = new List<int>();
        
        for (int i = 0; i < checkPointsForLinger.CheckPoints.Count; i++)
        {
            availableIndexesForLinger.Add(i);
        }
        
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

        EnemyBase prefab;
        
        if (selectedSetting.EnemyPrefabNeutral != null)
        {
            prefab = selectedSetting.EnemyPrefabNeutral;
        }
        else
        {
            prefab = Random.Range(0, 2) == 0 ? selectedSetting.EnemyPrefabLeft : selectedSetting.EnemyPrefabRight;
        }

        if (prefab == null)
        {
            Debug.LogError("Trying to spawn a prefab but it's null");
            return;
        }


        EnemyBase newEnemy = GetNewEnemy(prefab);
        
        
        newEnemy.transform.SetParent(transform);
        newEnemy.transform.position = spawnMarker.position;
        
        
        if (selectedSetting.EnemySettings.MovementType == EnemyMovementType.Linger)
        {
            // get a free index

            if (availableIndexesForLinger.Count == 0)
            {
                Debug.LogError("No more free indexes for linger, adding random to avoid crash");
                Vector3 randomPoint = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
                checkPointsForLinger.CheckPoints.Add(randomPoint);
                availableIndexesForLinger.Add(checkPointsForLinger.CheckPoints.Count - 1);
            }
            
            int index = availableIndexesForLinger[Random.Range(0, availableIndexesForLinger.Count)];
            availableIndexesForLinger.Remove(index);

            activeEnemiesLingerIndex.Add(newEnemy, index);
            newEnemy.SetLingerPoint(checkPointsForLinger.CheckPoints[index]);
        }

        newEnemy.Initialize(selectedSetting.EnemySettings, checkPointsForPaths[Random.Range(0, checkPointsForPaths.Count)]);

        if (enemiesToSpawnCurrentWave.Count == 0)
        {
            Debug.Log("wave finished");
            isSpawning = false;
        }
    }


    EnemyBase GetNewEnemy(EnemyBase prefab)
    {
        EnemyBase newEnemy = null;

        if (inactiveEnemies.ContainsKey(prefab) && inactiveEnemies[prefab].Count > 0)
        {
            newEnemy = inactiveEnemies[prefab][0];
            inactiveEnemies[prefab].RemoveAt(0);
        }
        else
        {
            newEnemy = Instantiate(prefab).GetComponent<EnemyBase>();
            newEnemy.Prefab = prefab;

            if (DEBUGIncreaseHitbox)
            {
                IncreaseHitBox(newEnemy);
            }
        }

        if (!activeEnemies.ContainsKey(prefab))
        {
            activeEnemies.Add(prefab, new List<EnemyBase>());
        }


        activeEnemies[prefab].Add(newEnemy);

        newEnemy.gameObject.SetActive(true);

        return newEnemy;
    }


    void OnEnemyDestroyedPrefabType(EnemyBase destroyedEnemy, EnemyBase enemyPrefab)
    {
        if (activeEnemies.ContainsKey(enemyPrefab))
        {
            activeEnemies[enemyPrefab].Remove(destroyedEnemy);

            if (!inactiveEnemies.ContainsKey(enemyPrefab))
            {
                inactiveEnemies.Add(enemyPrefab, new List<EnemyBase>());
            }

            inactiveEnemies[enemyPrefab].Add(destroyedEnemy);
        }
        else
        {
            Debug.LogError("An enemy was destroyed that was not in active enemies list, should never happen");
        }

        
        if (destroyedEnemy.Settings.MovementType == EnemyMovementType.Linger)
        {
            Debug.Log("freeing up index " + activeEnemiesLingerIndex[destroyedEnemy]);
            
            availableIndexesForLinger.Add(activeEnemiesLingerIndex[destroyedEnemy]);
            activeEnemiesLingerIndex.Remove(destroyedEnemy);
            
        }
        
        

        bool waveFinished;


        waveFinished = true;

        foreach (KeyValuePair<EnemyBase, List<EnemyBase>> pair in activeEnemies)
        {
            if (pair.Value.Count > 0)
            {
                waveFinished = false;
                break;
            }
        }

        if (waveFinished) Debug.Log("wave finished");
    }


    void IncreaseHitBox(EnemyBase enemy)
    {
        float hitboxMultiplier = 2f;

        // get all sphere colliders in enemy and children
        List<SphereCollider> colliders = enemy.GetComponentsInChildren<SphereCollider>().ToList();

        // check if main has collider too and add to list

        var maincollider = enemy.GetComponent<SphereCollider>();

        if (maincollider != null)
            colliders.Add(maincollider);


        foreach (var sphereCollider in colliders)
        {
            sphereCollider.radius *= hitboxMultiplier;
        }
    }



    [Button]
    void DestroyRandomEnemy()
    {
        
        
        
    }
}