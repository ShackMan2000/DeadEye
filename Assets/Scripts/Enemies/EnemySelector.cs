using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class EnemySelector : MonoBehaviour
{
    // [SerializeField] GameSettings settings;


    public List<EnemySettings> allEnemyOptions;
    public List<EnemySettings> SelectedEnemies;
    
    public bool PreventLastEnemyDeselection = true;

    [SerializeField] List<EnemyToggle> activeEnemyToggles;
    [ShowInInspector] List<EnemyToggle> inactiveEnemyToggles = new List<EnemyToggle>();


    public event Action<EnemySettings, bool> OnEnemySelected = delegate { };

    void Awake()
    {
        UpdateAllToggles();
    }


    public void InjectAllOptions(List<EnemySettings> options)
    {

        allEnemyOptions = new List<EnemySettings>();

        foreach (var enemySettings in options)
        {
            allEnemyOptions.Add(enemySettings);
        }


        
        if (activeEnemyToggles.Count > allEnemyOptions.Count)
        {
            // loop backwards so we can remove from the list
            for (int i = activeEnemyToggles.Count - 1; i >= allEnemyOptions.Count; i--)
            {
                inactiveEnemyToggles.Add(activeEnemyToggles[i]);
                activeEnemyToggles[i].gameObject.SetActive(false);
                activeEnemyToggles.RemoveAt(i);
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


    public void SetSomeEnemiesSelected(List<EnemySettings> selectedEnemies)
    {
        SelectedEnemies = new List<EnemySettings>();
        
        foreach (var enemySettings in selectedEnemies)
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

    public void SetAllToggleFramesToIconColor()
    {
        foreach (var toggle in activeEnemyToggles)
        {
            toggle.SetFrameToIconColor();
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
            OnEnemySelected(enemySettings, true);
        }
        else
        {
            if (!SelectedEnemies.Contains(enemySettings))
            {
                Debug.LogError("Enemy not selected, should not be possible");
                return;
            }

            SelectedEnemies.Remove(enemySettings);
            OnEnemySelected(enemySettings, false);
        }
    }
}