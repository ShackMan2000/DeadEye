using System;
using UnityEngine;


public class HealthController : MonoBehaviour
{
    [SerializeField] int health;

    [SerializeField] WaveController waveController;

    public event Action<int> OnHealthChanged = delegate { };

    bool unlimitedHealth;

    void OnEnable()
    {
        EnemyBullet.OnAnyEnemyBulletHitPlayer += ReduceHealth;
        UIController.OnEnableUnlimitedHealth += EnableUnlimitedHealth;
        health = 3;
    }

    void OnDisable()
    {
        EnemyBullet.OnAnyEnemyBulletHitPlayer -= ReduceHealth;
        UIController.OnEnableUnlimitedHealth -= EnableUnlimitedHealth;
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
            if (unlimitedHealth)
            {
                health = 3;
            }
            else
            {
                health = 0;
                GameManager.FinishGame();
            }
        }

        OnHealthChanged?.Invoke(health);
    }

    void EnableUnlimitedHealth()
    {
        unlimitedHealth = true;
    }
}