using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExplosionManager : MonoBehaviour
{

    [SerializeField] GameObject simpleExplosionPrefab;

    [SerializeField] List<GameObject> simpleExplosionsPool;


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
    }
    
    // later make explosion script that does this at exact time needed
    IEnumerator ReturnToPool(GameObject explosion)
    {
        yield return new WaitForSeconds(3f);
        explosion.SetActive(false);
        simpleExplosionsPool.Add(explosion);
    }
}