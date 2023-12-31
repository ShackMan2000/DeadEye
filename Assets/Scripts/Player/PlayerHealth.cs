 
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[InlineEditor]
[CreateAssetMenu]
public class PlayerHealth : ScriptableObject
{
    [SerializeField] SelectableOption maxHealthOption;
    
    
    public int CurrentHealth;
    
    public int MaxHealth => maxHealthOption.SelectedValue;

    public event Action OnHealthReduced = delegate { };



    public void ResetHealth()
    {
        CurrentHealth = maxHealthOption.SelectedValue;
    }

    
    

    public void ReduceHealth()
    {
        if (maxHealthOption.SelectedValue == 0)
        {
            return;
        }

        CurrentHealth--;

        OnHealthReduced?.Invoke();
    }
}
