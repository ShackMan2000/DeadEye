using System.Collections;
using UnityEngine;
using TMPro;
//using BNG; 
using UnityEngine.UI;

public class EnemyWaveSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to spawn
    public int numberOfWaves = 5; // Number of waves to spawn
    public int enemiesPerWave = 10; // Number of enemies to spawn per wave
    public float waveInterval = 5f; // Time interval between waves
    public float spawnInterval = 1f; // Time interval between enemy spawns
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f); // Size of the area where enemies can spawn

    public int currentWave = 0; // Current wave number
    public int enemiesSpawned = 0; // Number of enemies spawned in the current wave
    public TMP_Text WaveText;
    public TMP_Text SpawnText;
    public Slider enemySliderValue;
    public Slider waveSliderValue;

    private void Awake()
    {
       enemiesPerWave = Mathf.RoundToInt(enemySliderValue.value);
       numberOfWaves = Mathf.RoundToInt(waveSliderValue.value);
       WaveTextUpdate();
       SpawnTextUpdate();
    }

    private void Start()
    {
        // Start spawning waves
        
        StartCoroutine(SpawnWaves());

    }

    public void WaveTextUpdate()
    {
        WaveText.text = "Wave: "+currentWave;
    }
    public void SpawnTextUpdate()
    {
        SpawnText.text = "Spawned: "+ enemiesSpawned;
    }

    private IEnumerator SpawnWaves()
    {
        // Wait for the initial delay before starting the waves
        yield return new WaitForSeconds(waveInterval);

        // Spawn waves until reaching the desired number
        while (currentWave < numberOfWaves)
        {
            // Spawn enemies per wave
            for (int i = 0; i < enemiesPerWave; i++)
            {
                // Randomly select an enemy prefab
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

                // Generate a random spawn position within the spawn area
                Vector3 spawnPosition = transform.position + new Vector3(
                    Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                    0f,
                    Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f)
                );

                // Instantiate the enemy at the spawn position
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, this.transform );
                WaveTextUpdate();
                SpawnTextUpdate();

                // Wait for the specified spawn interval before spawning the next enemy
                yield return new WaitForSeconds(spawnInterval);
            }

            // Increment the current wave and reset the enemies spawned counter
            currentWave++;
            enemiesSpawned = 0;
            WaveTextUpdate();
            SpawnTextUpdate();
            



            // Wait for the specified wave interval before starting the next wave
            yield return new WaitForSeconds(waveInterval);
        }
    }
}
