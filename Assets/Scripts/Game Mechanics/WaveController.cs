using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WaveController : MonoBehaviour
{

    // keeps track of each wave, listens to wave game mode to start and to reset wave back to 0
    
    // spawns all enemies and also controls how often they shoot, so it's not too many at the same time
    
    
    [SerializeField] WaveSettings settings;

    [SerializeField] Transform spawnMarker;

    [SerializeField] Train train;

    [SerializeField] int currentWaveIndex;
    
    [SerializeField] List<Enemy> activeEnemies;
    
    
    // for spawning tripplets etc. could disable the interval but add it to the max so the next one won't spawn too fast

    [SerializeField] GameObject testEnemy;

    float timeTillNextSpawn;


    [Button]
    void StartSpawnWaveRoutine()
    {
        StartCoroutine(SpawnWaveRoutine());
    }
    
    
    IEnumerator SpawnWaveRoutine()
    {
        train.SpawnTrain();
        
        yield return new WaitForSeconds(train.MovementDuration);
        
        // spawn enemies
      //  GameObject newEnemy = Instantiate(settings.AllEnemies[Random.Range(0, settings.AllEnemies.Count)], spawnMarker.position, Quaternion.identity);
        
      GameObject newEnemy = Instantiate(testEnemy, spawnMarker.position, Quaternion.identity);
        activeEnemies = new List<Enemy>();
        
        
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
    
}