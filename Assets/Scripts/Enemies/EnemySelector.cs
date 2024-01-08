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


    // the goal is that the user can disable some enemies. For now could simply have a list of disabled enemies
    // then every time it switches between the game modes, it will create a filtered list with only the selected

    // so the first time the graph creates a list for the waves and thus will have a list of all enemy settings that are graphed
    // the user then might toggle some enemies, so that info must get to the graph so it can toggle the graph.
    // could then 
    // graph could listen to this
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