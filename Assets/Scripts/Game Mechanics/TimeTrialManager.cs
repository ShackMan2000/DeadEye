using System;
using System.Collections.Generic;
using System.Linq;
using FluffyUnderware.Curvy;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class TimeTrialManager : MonoBehaviour
{
    [SerializeField] SelectableOption timeOptions;
    
    [SerializeField] WaveSettings waveSettings;
    
    [SerializeField] List<CurvySpline> splines;
    
    float timeTillNextSpawn;
    public float StartTimeInSeconds => timeOptions.SelectedValue * 60;

    public int MaxActiveActiveEnemies => waveSettings.MaxActiveEnemies;

    float timeLeft;
    
    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField] TextMeshProUGUI timeLeftText;
    
    bool gameIsRunning;


    [ShowInInspector]
    List<EnemySettings> enemiesToSpawnCurrentBatch;


    
    // enemies should not loop, too hard to control and messes with settings like spawn percentages
    // set motion to either loop or clamped. for time trial need to sub to reached end event.
    
    
    void OnEnable()
    {
        GameManager.OnStartingNewTimeTrialGame += OnStartingNewTimeTrialGame;
    }
    
    void OnDisable()
    {
        GameManager.OnStartingNewTimeTrialGame -= OnStartingNewTimeTrialGame;
    }


    void OnStartingNewTimeTrialGame()
    {
        timeLeft = StartTimeInSeconds;
        gameIsRunning = true;
        timeLeftText.gameObject.SetActive(true);
        
        
        enemySpawner.SetUpCurvyPaths(splines, null, null);
        
        // also tell enemy spawner about the paths.
        // pick a random enemy based on the percentages. Might be easiest to put them all in a list, draw one and remove
        // when list is empty, create new one
        
        // need to make sure that enemies that reached the gate are destroyed without score.
        // use that event thing
    }
    
    


    void SpawnRandomEnemy()
    {
        
        if(enemySpawner.GetActiveEnemiesCount() >= MaxActiveActiveEnemies)
        {
            return;
        }
        
        
        // create new batch
        if(enemiesToSpawnCurrentBatch == null || enemiesToSpawnCurrentBatch.Count == 0)
        {
            enemiesToSpawnCurrentBatch = new List<EnemySettings>();

            foreach (var option in waveSettings.AllEnemiesOptions)
            {
                for (int i = 0; i < option.SpawnAmountBase; i++)
                {
                    enemiesToSpawnCurrentBatch.Add(option.EnemySettings);
                }
            }
        }
        
        if(enemiesToSpawnCurrentBatch.Count == 0)
        {
            Debug.LogError("No enemies to spawn");
            return;
        }
        
        
        int randomIndex = Random.Range(0, enemiesToSpawnCurrentBatch.Count);

        EnemySettings selectedSetting = enemiesToSpawnCurrentBatch[randomIndex];
        
        enemiesToSpawnCurrentBatch.RemoveAt(randomIndex);


        if (selectedSetting.MovementType == EnemyMovementType.Linger)
        {
            Debug.LogError("Linger enemies not supported in time trial");
            return;
        }
        
        enemySpawner.SpawnEnemy(selectedSetting, true);
    }


    void Update()
    {
        if(gameIsRunning)
        {
            timeLeft -= Time.deltaTime;
            
            
         
            timeLeftText.text = timeLeft.ToString("F1") + "s";
            
            if (timeLeft <= 0)
            {
                timeLeftText.text = "0.0s";
                gameIsRunning = false;
                enemySpawner.MakeAllEnemiesInactive();
                
                GameManager.FinishTimeTrialGame();
            }
            
            timeTillNextSpawn -= Time.deltaTime;

            if (timeTillNextSpawn <= 0)
            {
                SpawnRandomEnemy();
                timeTillNextSpawn = Random.Range(waveSettings.SpawnIntervalMin, waveSettings.SpawnIntervalMax);
            }
        }
        
    }
}