using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExplosionManager : MonoBehaviour
{

    [SerializeField] GameObject simpleExplosionPrefab;

    [SerializeField] List<GameObject> simpleExplosionsPool;

    
    [SerializeField] List<AudioSource> explosionSoundsPrefabs;
    [SerializeField] Vector2 explosionSoundPitchRange;
    [SerializeField] List<AudioSource> activeExplosionSounds;
        [SerializeField] List<AudioSource> explosionSoundsPool;

    void Awake()
    {
        simpleExplosionsPool = new List<GameObject>();
    }

    void OnEnable()
    {
        EnemyBase.OnSpawnExplosion += SpawnSimpleExplosion;
    }

    void OnDisable()
    {
        EnemyBase.OnSpawnExplosion -= SpawnSimpleExplosion;
    }
    
    
    void SpawnSimpleExplosion(Vector3 position)
    {
        // get explosion from pool or create new one
        if  (simpleExplosionsPool.Count > 0)
        {
            GameObject explosion = simpleExplosionsPool[0];
            explosion.transform.position = position;
            explosion.SetActive(true);
            simpleExplosionsPool.RemoveAt(0);
        }
        else
        {
            GameObject explosion = Instantiate(simpleExplosionPrefab, transform);
            explosion.transform.position = position;
            explosion.SetActive(true);
        }
        
        
       StartCoroutine(PlayExplosionSound(position));
    }


    IEnumerator PlayExplosionSound(Vector3 pos)
    {
        // get explosion sound from pool or create new one
        if (explosionSoundsPool.Count > 0)
        {
            AudioSource explosionSound = explosionSoundsPool[0];
            explosionSound.transform.position = pos;
            explosionSound.pitch = Random.Range(explosionSoundPitchRange.x, explosionSoundPitchRange.y);
            explosionSound.Play();
            explosionSoundsPool.RemoveAt(0);
            activeExplosionSounds.Add(explosionSound);
        }
        else
        {
            AudioSource explosionSound = Instantiate(explosionSoundsPrefabs[Random.Range(0, explosionSoundsPrefabs.Count)], pos, Quaternion.identity, transform);
         //   explosionSound.pitch = Random.Range(explosionSoundPitchRange.x, explosionSoundPitchRange.y);
            explosionSound.Play();
            activeExplosionSounds.Add(explosionSound);
        }
        
        yield return new WaitForSeconds(3f);
        activeExplosionSounds[0].Stop();
        explosionSoundsPool.Add(activeExplosionSounds[0]);
        activeExplosionSounds.RemoveAt(0);
    }
    
    
    // later make explosion script that does this at exact time needed
    IEnumerator ReturnToPool(GameObject explosion)
    {
        yield return new WaitForSeconds(3f);
        explosion.SetActive(false);
        simpleExplosionsPool.Add(explosion);
    }
}