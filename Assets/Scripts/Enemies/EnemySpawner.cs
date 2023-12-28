using System;
using System.Collections.Generic;
using FluffyUnderware.Curvy;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{

    [ShowInInspector, ReadOnly] List<CurvySpline> splinesEntryForMainLoops;
    [ShowInInspector, ReadOnly] List<CurvySpline> splinesLingerEasyAvailable;
    [ShowInInspector, ReadOnly] List<CurvySpline> splinesLingerHardReserved;
    [ShowInInspector, ReadOnly] List<CurvySpline> splinesLingerHardAvailable;
    [ShowInInspector, ReadOnly] List<CurvySpline> splinesLingerEasyReserved;

    [ShowInInspector] Dictionary<EnemyBase, List<EnemyBase>> inactiveEnemies = new Dictionary<EnemyBase, List<EnemyBase>>();
    [ShowInInspector] public Dictionary<EnemyBase, List<EnemyBase>> activeEnemies = new Dictionary<EnemyBase, List<EnemyBase>>();


    public static event Action<int> OnActiveEnemiesCountChanged = delegate { };


    void OnEnable()
    {
        EnemyBase.OnAnyEnemyDestroyedPrefabType += OnEnemyDestroyedPrefabType;
        EnemyMovement.OnSplineFreedUp += OnSplineFreedUp;
    }

    void OnDisable()
    {
        EnemyBase.OnAnyEnemyDestroyedPrefabType -= OnEnemyDestroyedPrefabType;
        EnemyMovement.OnSplineFreedUp -= OnSplineFreedUp;
    }


    public void SetUpCurvyPaths(List<CurvySpline> entryPaths, List<CurvySpline> lingerEasy, List<CurvySpline> lingerHard)
    {
        splinesEntryForMainLoops = entryPaths;
        splinesLingerEasyAvailable = lingerEasy;
        splinesLingerHardAvailable = lingerHard;
        
        splinesLingerEasyReserved = new List<CurvySpline>();
        splinesLingerHardReserved = new List<CurvySpline>();
    }

    
    public int GetActiveEnemiesCount()
    {
        int activeEnemiesCount = 0;

        foreach (KeyValuePair<EnemyBase, List<EnemyBase>> pair in activeEnemies)
        {
            activeEnemiesCount += pair.Value.Count;
        }

        return activeEnemiesCount;
    }


    public void SpawnEnemy(EnemySettings enemySettings, bool isLooping)
    {
        if (enemySettings == null || enemySettings.Prefab == null)
        {
            Debug.LogError("Trying to spawn a prefab but it's null");
            return;
        }

        EnemyBase newEnemy = GetNewEnemy(enemySettings.Prefab);
        

        CurvySpline lingerSpline = null;

        if (enemySettings.MovementType == EnemyMovementType.Linger)
        {
            if (enemySettings.Difficulty == EnemyDifficulty.Easy && splinesLingerEasyAvailable.Count > 0)
            {
                lingerSpline = splinesLingerEasyAvailable[Random.Range(0, splinesLingerEasyAvailable.Count)];
                splinesLingerEasyReserved.Add(lingerSpline);
                splinesLingerEasyAvailable.Remove(lingerSpline);
            }
            else if (enemySettings.Difficulty == EnemyDifficulty.Hard && splinesLingerHardAvailable.Count > 0)
            {
                lingerSpline = splinesLingerHardAvailable[Random.Range(0, splinesLingerHardAvailable.Count)];
                splinesLingerHardReserved.Add(lingerSpline);
                splinesLingerHardAvailable.Remove(lingerSpline);
            }
            else
            {
                Debug.LogError("ERROR No splines available for difficulty " + enemySettings.Difficulty + "skipping linger enemy");
                return;
            }
        }
        else
        {
            lingerSpline = splinesEntryForMainLoops[Random.Range(0, splinesEntryForMainLoops.Count)];
        }
        
        if(lingerSpline == null)
        {
            Debug.LogError("ERROR! Linger spline is null");
            return;
        }

        
        // here is probably where it should be set to either clamp and destroy when reaching end of spline or loop
        newEnemy.Initialize(enemySettings, lingerSpline , isLooping);


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
            
            newEnemy.transform.SetParent(transform);
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
            enemy.DeactivateViaManager();
        }
    }
    
    
    void OnSplineFreedUp(CurvySpline spline)
    {
        if (splinesLingerEasyReserved.Contains(spline))
        {
            splinesLingerEasyReserved.Remove(spline);
            splinesLingerEasyAvailable.Add(spline);
        }
        else if (splinesLingerHardReserved.Contains(spline))
        {
            splinesLingerHardReserved.Remove(spline);
            splinesLingerHardAvailable.Add(spline);
        }
        else
        {
            Debug.LogError("ERROR! Trying to free up a spline that is not reserved");
        }
    }

    public void FreeAllSplines()
    {
        foreach (var spline in splinesLingerEasyReserved)
        {
            splinesLingerEasyAvailable.Add(spline);
        }
        
        foreach (var spline in splinesLingerHardReserved)
        {
            splinesLingerHardAvailable.Add(spline);
        }
        
        splinesLingerEasyReserved.Clear();
        splinesLingerHardReserved.Clear();
        
    }
}