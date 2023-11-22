using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    // keeps track of each wave, listens to wave game mode to start and to reset wave back to 0
    
    // spawns all enemies and also controls how often they shoot, so it's not too many at the same time
    [SerializeField] WaveSettings testWaveSettings;


    [SerializeField] List<Enemy> activeEnemies;
    


    [Button]
    void SpawnWaveNextWave()
    {
        activeEnemies = new List<Enemy>();
        
        // spawn enemies and add them to the list
        // add a listener to each enemy when they get destroyed
        
        
        // need some kind of movement for the enemies, could just use a rotation for now, something related to depth
        
        
        // depth enemies will have special movement, either rotating around each other on special axis or linear back and forth
        
    }
    
}