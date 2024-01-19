using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class EnemyBullet : MonoBehaviour
{

    [SerializeField] PlayerHealth selectableOption;
    
    public float Speed;

    bool isMoving;
    Vector3 direction;
    
    public static event Action OnAnyEnemyBulletHitPlayer = delegate { };

    [SerializeField] AudioSource audioSource;
    [SerializeField] float secondsBeforeImpactToPlaySwooshSound;
    
    

    // should start playing the swoosh sound
    // need to know how many seconds before impact it should start playing
    // how many seconds till impact, so count that down

    void OnEnable()
    {
        GameManager.OnGameFinished += DestroyBullet;
        GameManager.OnGamePaused += PauseSound;
        GameManager.OnGameResumed += ResumeSound;
    }
    
    void OnDisable()
    {
        GameManager.OnGameFinished -= DestroyBullet;
        GameManager.OnGamePaused -= PauseSound;
        GameManager.OnGameResumed -= ResumeSound;
        
    }


    public void Initialize(Vector3 position, Vector3 playerPositionPosition)
    {
        isMoving = true;
        transform.position = position;
        direction = (playerPositionPosition - position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        
        float timeTillImpact = Vector3.Distance(position, playerPositionPosition) / Speed;
        
        if (timeTillImpact > secondsBeforeImpactToPlaySwooshSound)
        {
            audioSource.PlayDelayed(timeTillImpact - secondsBeforeImpactToPlaySwooshSound);
        }
        else
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        if (!GameManager.IsPaused && isMoving)
        {
            transform.position += direction * (Speed * Time.deltaTime);
        }
    }
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selectableOption.ReduceHealth();
            Destroy(gameObject);
        }
    }
    
    void PauseSound()
    {
        audioSource.Pause();
    }
    
    void ResumeSound()
    {
        audioSource.UnPause();
    }
    
    
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}