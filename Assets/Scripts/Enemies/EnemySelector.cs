using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class EnemySelector : MonoBehaviour
{
   // [SerializeField] GameSettings settings;


    public List<EnemySettings> allEnemyOptions;
    public List<EnemySettings> SelectedEnemies;

    [SerializeField] List<EnemyToggle> activeEnemyToggles;
    [ShowInInspector] List<EnemyToggle> inactiveEnemyToggles = new List<EnemyToggle>();


    void Awake()
    {
        UpdateAllToggles();
    }
    
    
    // wave controller and time trial inject in start based on the spawn settings, selector than keeps track of selection itself
    // graph will inject it every time it toggles between waves and time trial.
    // First time all enemies will be selected, this should be saved so when switching between game modes, the selection stays the same.
    // might be best to have 2 lists? this is where the SO workflow would be better, so the graph can hold a list instance for each.
    // otherwise the graph could save the current selection before injecting another one
    
    // what is the simplest method? not changing the selection at all. when clicking wave stats, it will inject the options and all are selected
    // okay, so graph can keep track of what was disabled for wave and time trial and just update the list after injecting options

    public void InjectAllOptions(List<EnemySettings> options)
    {
        allEnemyOptions = new List<EnemySettings>();

        foreach (var enemySettings in options)
        {
            allEnemyOptions.Add(enemySettings);
        }
        
        
        if (activeEnemyToggles.Count > allEnemyOptions.Count)
        {
            for (int i = allEnemyOptions.Count; i < activeEnemyToggles.Count; i++)
            {
                activeEnemyToggles[i].gameObject.SetActive(false);
                inactiveEnemyToggles.Add(activeEnemyToggles[i]);
            }
        }


        for (int i = 0; i < allEnemyOptions.Count; i++)
        {
            if (i >= activeEnemyToggles.Count)
            {
                EnemyToggle toggle = GetToggle();
                activeEnemyToggles.Add(toggle);
                toggle.gameObject.SetActive(true);
            }

            activeEnemyToggles[i].InjectEnemySettings(allEnemyOptions[i], this);
        }


        EnemyToggle GetToggle()
        {
            if (inactiveEnemyToggles.Count > 0)
            {
                EnemyToggle toggle = inactiveEnemyToggles[0];
                inactiveEnemyToggles.RemoveAt(0);
                return toggle;
            }
            else
            {
                EnemyToggle toggle = Instantiate(activeEnemyToggles[0], activeEnemyToggles[0].transform.parent);
                return toggle;
            }
        }
    }

    public void SetAllEnemiesSelected()
    {
        SelectedEnemies = new List<EnemySettings>();

        foreach (var enemySettings in allEnemyOptions)
        {
            SelectedEnemies.Add(enemySettings);
        }
        
        UpdateAllToggles();
    }


    void UpdateAllToggles()
    {
        foreach (var toggle in activeEnemyToggles)
        {
            toggle.UpdateSelected(SelectedEnemies.Contains(toggle.EnemySettings));
        }
    }


    public void ToggleSelected(EnemySettings enemySettings, bool selected)
    {
        if (selected)
        {
            if (SelectedEnemies.Contains(enemySettings))
            {
                Debug.LogError("Enemy already selected, should not be possible");
                return;
            }

            SelectedEnemies.Add(enemySettings);
        }
        else
        {
            if (!SelectedEnemies.Contains(enemySettings))
            {
                Debug.LogError("Enemy not selected, should not be possible");
                return;
            }

            SelectedEnemies.Remove(enemySettings);
        }

    }
}