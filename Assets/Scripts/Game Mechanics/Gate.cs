using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gate : MonoBehaviour
{

    
    // when enemy goes through gate, play sound.
    // use pool with prefab

    [SerializeField] AudioSource enemyLeavesGatePrefab;

    [SerializeField] AudioSource trainEntersGateSound;

    [SerializeField] List<AudioSource> enemyLeavesGatePool = new List<AudioSource>();
    
    [SerializeField] bool isExitGate;


    void OnEnable()
    {
        EnemyMovement.OnEnemyLeftThroughGate += PlayEnemyLeavesGateSound;
    }
    
    void OnDisable()
    {
        EnemyMovement.OnEnemyLeftThroughGate -= PlayEnemyLeavesGateSound;
    }
    
    
    public void PlayTrainEntersGateSound()
    {
        trainEntersGateSound.Play();
    }
    
    void PlayEnemyLeavesGateSound()
    {
        if (isExitGate)
        {
            if(enemyLeavesGatePool.Count > 0)
            {
                AudioSource audioSource = enemyLeavesGatePool[0];
                enemyLeavesGatePool.RemoveAt(0);
                audioSource.gameObject.SetActive(true);
                
                StartCoroutine(PlayAndPoolSound(audioSource));
            }
            else
            {
                //instantiate prefab
                AudioSource audioSource = Instantiate(enemyLeavesGatePrefab, transform.position, Quaternion.identity);
                
                StartCoroutine(PlayAndPoolSound(audioSource));
            }
        }
    }
    
    
    IEnumerator PlayAndPoolSound(AudioSource audioSource)
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.gameObject.SetActive(false);
        enemyLeavesGatePool.Add(audioSource);
    }




}