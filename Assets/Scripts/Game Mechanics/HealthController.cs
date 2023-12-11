using System;
using UnityEngine;


public class HealthController : MonoBehaviour
{
    
    [SerializeField] int health;
    
    [SerializeField] WaveController waveController;
    
    public event Action<int> OnHealthChanged = delegate { };
    
    
    void OnEnable()
    {
        EnemyBullet.OnAnyEnemyBulletHitPlayer += ReduceHealth;
        health = 3;
    }
    
    void OnDisable()
    {
        EnemyBullet.OnAnyEnemyBulletHitPlayer -= ReduceHealth;
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
            GameManager.FinishGame();
        }
        OnHealthChanged?.Invoke(health);
    }
}