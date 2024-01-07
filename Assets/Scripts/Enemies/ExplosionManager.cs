using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class ExplosionManager : MonoBehaviour
{
    [ShowInInspector] Dictionary<Explosion, List<Explosion>> explosionPools;
    [ShowInInspector] Dictionary<Explosion, List<Explosion>> activeExplosions;


    void Awake()
    {
        explosionPools = new Dictionary<Explosion, List<Explosion>>();
        activeExplosions = new Dictionary<Explosion, List<Explosion>>();
    }

    void OnEnable()
    {
        EnemyBase.OnSpawnExplosion += SpawnSimpleExplosion;
    }

    void OnDisable()
    {
        EnemyBase.OnSpawnExplosion -= SpawnSimpleExplosion;
    }


// okay that went kinda out of hand with the pooling. Issue was reconnecting the instance to the prefab.
// might be worth checking out to send in a struct/class that has the data when instantiating it

    void SpawnSimpleExplosion(EnemySettings settings, Vector3 position)
    {
        if (settings.ExplosionPrefab == null)
        {
            Debug.LogError("No explosion prefab set for " + settings.name);
            return;
        }

        Explosion explosionPrefab = settings.ExplosionPrefab;


        if (explosionPools.ContainsKey(explosionPrefab) && explosionPools[explosionPrefab].Count > 0)
        {
            Explosion explosion = explosionPools[explosionPrefab][0];
            explosion.transform.position = position;
            explosionPools[explosionPrefab].RemoveAt(0);

            if (!activeExplosions.ContainsKey(explosionPrefab))
            {
                activeExplosions.Add(explosionPrefab, new List<Explosion>());
            }

            activeExplosions[explosionPrefab].Add(explosion);
            explosion.Play();
        }
        else
        {
            Explosion explosion = Instantiate(explosionPrefab, transform);
            explosion.transform.position = position;
            explosion.InjectPool(this);

            if (!activeExplosions.ContainsKey(explosionPrefab))
            {
                activeExplosions.Add(explosionPrefab, new List<Explosion>());
            }

            activeExplosions[explosionPrefab].Add(explosion);
            explosion.Play();
        }
    }


    public void ReturnToPool(Explosion explosion)
    {
        Explosion prefab = null;
        foreach (var explosionPrefab in activeExplosions.Keys)
        {
            if (activeExplosions[explosionPrefab].Contains(explosion))
            {
                prefab = explosionPrefab;
                activeExplosions[explosionPrefab].Remove(explosion);
                break;
            }
        }

        if (prefab == null)
        {
            Debug.LogError("Explosion not found in pool, should not be possible");
            Destroy(explosion.gameObject);
            return;
        }
        
        
        if (!explosionPools.ContainsKey(prefab))
        {
            explosionPools.Add(prefab, new List<Explosion>());
        }

        explosion.gameObject.SetActive(false);
        explosionPools[prefab].Add(explosion);
    }
}