using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<CheckPointsList> checkPointsForPaths;

    [SerializeField] List<CheckPointsList> checkPointsForLinger;

    
    
    [ShowInInspector] Dictionary<EnemyBase, List<EnemyBase>> inactiveEnemies = new Dictionary<EnemyBase, List<EnemyBase>>();
    [ShowInInspector] public Dictionary<EnemyBase, List<EnemyBase>> activeEnemies = new Dictionary<EnemyBase, List<EnemyBase>>();

    
    public static event Action<int> OnActiveEnemiesCountChanged = delegate { };
    
    
    void OnEnable()
    {
        EnemyBase.OnAnyEnemyDestroyedPrefabType += OnEnemyDestroyedPrefabType;
    }

    void OnDisable()
    {
        EnemyBase.OnAnyEnemyDestroyedPrefabType -= OnEnemyDestroyedPrefabType;
    }
    
    
    
    
    public void SetUpCheckPointsLists(List<CheckPointsList> paths, List<CheckPointsList> lingers)
    {
        checkPointsForPaths = paths;
        checkPointsForLinger = lingers;
    }


   public void SpawnEnemy(EnemySettings enemySettings, Vector3 spawnPosition)
    {
        if (enemySettings == null || enemySettings.Prefab == null)
        {
            Debug.LogError("Trying to spawn a prefab but it's null");
            return;
        }

        EnemyBase newEnemy = GetNewEnemy(enemySettings.Prefab);


        newEnemy.transform.SetParent(transform);
        newEnemy.transform.position = spawnPosition;


        if (enemySettings.MovementType == EnemyMovementType.Linger)
        {
            int checkpointsForDifficultyIndex = checkPointsForLinger.FindIndex(x => x.Difficulty == enemySettings.Difficulty);

            if (checkpointsForDifficultyIndex == -1)
            {
                Debug.Log("ERROR No checkpoints list found for difficulty " + enemySettings.Difficulty);
                checkpointsForDifficultyIndex = 0;
            }

            newEnemy.SetLingerPoint(checkPointsForLinger[checkpointsForDifficultyIndex]);
        }

        newEnemy.Initialize(enemySettings, checkPointsForPaths[Random.Range(0, checkPointsForPaths.Count)]);
        
        
        UpdateActiveEnemiesCount();
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


        UpdateActiveEnemiesCount();
    }

    void UpdateActiveEnemiesCount()
    {
        int activeEnemiesCount = 0;

        foreach (KeyValuePair<EnemyBase, List<EnemyBase>> pair in activeEnemies)
        {
            activeEnemiesCount += pair.Value.Count;
            
        }
        
        OnActiveEnemiesCountChanged(activeEnemiesCount);
    }

    public void MakeAllEnemiesInactive()
    {
        List<EnemyBase> enemiesToDisappear = new List<EnemyBase>();

        foreach (KeyValuePair<EnemyBase, List<EnemyBase>> pair in activeEnemies)
        {
            enemiesToDisappear.AddRange(pair.Value);
        }

        foreach (EnemyBase enemy in enemiesToDisappear)
        {
            enemy.DisappearWhenPlayerGotKilled();
        }
    }
}