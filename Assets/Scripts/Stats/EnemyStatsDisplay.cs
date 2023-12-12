using System;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EnemyStatsDisplay : MonoBehaviour
{
    [SerializeField] Image enemyIcon;
    [SerializeField] Image correctWeaponIcon;

    public List<EnemySettings> enemiesGroupedInStat;
    [SerializeField] WeaponType correctWeapon;

    [SerializeField] TextMeshProUGUI correctPercentageText;


    StatsPerEnemy statsPerEnemy;


    public void InjectStats(StatsPerEnemy stats)
    {
        statsPerEnemy = stats;

        
        
        //   correctWeaponIcon.sprite = statsPerEnemy.EnemySettings.CorrectWeaponIcon;

        //   correctPercentageText.text = statsPerEnemy.CorrectPercentage.ToString("F0") + "%";
    }
    
    

    void SetIcons()
    {
        if (enemiesGroupedInStat.Count > 0)
        {
            if (enemiesGroupedInStat[0].Icon != null)
            {
                enemyIcon.sprite = enemiesGroupedInStat[0].Icon;
            }
            else
            {
                Debug.Log("ERROR: Enemy stats display has no icon");
            }
            
            if(enemiesGroupedInStat[0].CorrectWeaponsToGetShot != null && enemiesGroupedInStat[0].CorrectWeaponsToGetShot.Count > 0 && enemiesGroupedInStat[0].CorrectWeaponsToGetShot[0].Icon != null)
            {
                correctWeaponIcon.sprite = enemiesGroupedInStat[0].CorrectWeaponsToGetShot[0].Icon;
            }
            else
            {
                Debug.Log("ERROR: Enemy stats display has no correct weapon icon");
            }
            
        }
    }


    void OnValidate()
    {
        bool hasCorrectWeapon = true;

        foreach (var enemy in enemiesGroupedInStat)
        {
            if (!enemy.CorrectWeaponsToGetShot.Contains(correctWeapon))
            {
                hasCorrectWeapon = false;
            }
        }

        if (!hasCorrectWeapon)
        {
            Debug.Log("ERROR: Enemy stats display has wrong weapon");
        }
        
        SetIcons();
    }
}