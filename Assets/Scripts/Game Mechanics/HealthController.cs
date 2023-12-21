using System;
using UnityEngine;


public class HealthController : MonoBehaviour
{
    [SerializeField] int health;

    [SerializeField] int maxHealth = 3;
    
    [SerializeField] WaveController waveController;

    public event Action<int> OnHealthChanged = delegate { };

   public bool UnlimitedHealth;

    void OnEnable()
    {
        EnemyBullet.OnAnyEnemyBulletHitPlayer += ReduceHealth;
        UIController.OnEnableUnlimitedHealth += EnableUnlimitedHealth;
        
        GameManager.OnStartingNewTimeTrialGame += EnableUnlimitedHealth;
        GameManager.OnStartingNewWaveGame += ResetHealth;
        
        health = 3;
    }

    void OnDisable()
    {
        EnemyBullet.OnAnyEnemyBulletHitPlayer -= ReduceHealth;
        UIController.OnEnableUnlimitedHealth -= EnableUnlimitedHealth;
        
        GameManager.OnStartingNewTimeTrialGame -= EnableUnlimitedHealth;
        GameManager.OnStartingNewWaveGame += ResetHealth;
    }

    void ResetHealth()
    {
        health = maxHealth;
    }


    void Start()
    {
        OnHealthChanged?.Invoke(health);
    }


    void ReduceHealth()
    {
        health--;

        if (health <= 0)
        {
            if (UnlimitedHealth)
            {
                health = 3;
            }
            else
            {
                health = 0;
                GameManager.WaveFailed();
            }
        }

        OnHealthChanged?.Invoke(health);
    }

    public void EnableUnlimitedHealth()
    {
        UnlimitedHealth = true;
    }
}