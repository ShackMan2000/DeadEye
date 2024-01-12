using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletStreaks : MonoBehaviour
{
    [SerializeField] Gun gun;
    [SerializeField] Transform spawnPoint;

    [SerializeField] GameObject bulletStreakPrefab;
    [SerializeField] float speed = 4f;
    [SerializeField] float maxTimeVisible = 0.2f;
    
    List<GameObject> bulletStreakPool = new List<GameObject>();
    List<GameObject> activeBulletStreaks = new List<GameObject>();


    [SerializeField] float minDistance = 1f;
    
    //question is how to move them...

    void OnEnable()
    {
        bulletStreakPrefab.SetActive(false);
        gun.OnShotFiredWithDistance += SpawnBulletStreak;
        
        for (int i = activeBulletStreaks.Count - 1; i >= 0; i--)
        {
            activeBulletStreaks[i].gameObject.SetActive(false);
            bulletStreakPool.Add(activeBulletStreaks[i]);
            activeBulletStreaks.RemoveAt(i);
        }
    }


    void OnDisable()
    {
        gun.OnShotFiredWithDistance -= SpawnBulletStreak;
    }

    void SpawnBulletStreak(float distance)
    {
        if (distance < minDistance)
        {
            return;
        }
        
        GameObject bulletStreak = GetBulletStreak();
        bulletStreak.transform.position = spawnPoint.position;
        bulletStreak.transform.rotation = spawnPoint.rotation;
        
        float timeVisible = distance / speed;
        timeVisible = Mathf.Min(timeVisible, maxTimeVisible);
        
        StartCoroutine(MoveBulletStreakRoutine(bulletStreak, timeVisible));
    
    }

    GameObject GetBulletStreak()
    {
        GameObject bulletStreak = null;

        if (bulletStreakPool.Count > 0)
        {
            bulletStreak = bulletStreakPool[0];
            bulletStreakPool.RemoveAt(0);
        }
        else
        {
            Vector3 worldScale = bulletStreakPrefab.transform.lossyScale;
            bulletStreak = Instantiate(bulletStreakPrefab);
            bulletStreak.transform.localScale = worldScale;
        }

        activeBulletStreaks.Add(bulletStreak);
        bulletStreak.gameObject.SetActive(true);

        return bulletStreak;
    }
    
    
    IEnumerator MoveBulletStreakRoutine(GameObject bulletStreak, float timeVisible)
    {
        float time = 0f;
        while (time < timeVisible)
        {
            time += Time.deltaTime;
            bulletStreak.transform.position += bulletStreak.transform.forward * speed * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        
        bulletStreak.gameObject.SetActive(false);
        activeBulletStreaks.Remove(bulletStreak);
        bulletStreakPool.Add(bulletStreak);
    }
}