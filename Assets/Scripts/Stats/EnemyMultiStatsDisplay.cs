using System.Collections.Generic;
using System.Drawing;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;


public class EnemyMultiStatsDisplay : MonoBehaviour
{
   // [SerializeField] Image enemyIcon;

   // [SerializeField] MultiDrone multiDrone;

    public EnemySettings EnemySettings;

    
    [InfoBox("Make both together equal 1")]
    [SerializeField] float almostHitRangePercentage;
    [SerializeField] float outerRangePercentage;

    [SerializeField] List<MultiDroneStatsRangeSlot>  rangeSlots;
    
    
    
    
    // need to first get the correct range relative, -1 to 1
    // then split the remaining between 2 ranges and then assign all of that.
    // hard code for now, correct range, almost hit range, outer range


    // need an image row for each slot
    // which means I need to define the slots

    StatsMultiDrone stats;


    [Button]
    void SetRanges()
    {
        float correctRange = EnemySettings.SideDroneOffsetForKillRelative();
        float rangeLeft = 1f - correctRange;
        float outerRange = outerRangePercentage * rangeLeft;
        float almostHitRange = almostHitRangePercentage * rangeLeft;
        
        Vector2 outerRangeNegative = new Vector2(-1, -1 + outerRange);
        rangeSlots[0].SetRangeAndHeight(outerRangeNegative, outerRange / 2f);

        Vector2 almostHitRangeNegative = new Vector2(outerRangeNegative.y, outerRangeNegative.y + almostHitRange);
        rangeSlots[1].SetRangeAndHeight(almostHitRangeNegative, almostHitRange / 2f);
        
        rangeSlots[2].SetRangeAndHeight(new Vector2(-correctRange, correctRange), correctRange);

        Vector2 almostHitRangePositive = new Vector2(correctRange, correctRange + almostHitRange);
        rangeSlots[3].SetRangeAndHeight(almostHitRangePositive, almostHitRange / 2f);
        
        Vector2 outerHitRangePositive = new Vector2(almostHitRangePositive.y, 1f);
        rangeSlots[4].SetRangeAndHeight(outerHitRangePositive, outerRange / 2f);

        
        Debug.Log("sum of heights is" + (outerRange / 2f + almostHitRange / 2f + correctRange + almostHitRange / 2f + outerRange / 2f));
    }

    public void InjectStats(StatsMultiDrone s)
    {
//        killCountText.text = (statsPerSingleEnemy.DestroyedCorrectly + statsPerSingleEnemy.DestroyedByMistake).ToString();
  //      correctPercentageText.text = (statsPerSingleEnemy.ShotCorrectWeaponPercent * 100f).ToString("F0") + "%";


        //   correctWeaponIcon.sprite = statsPerEnemy.EnemySettings.CorrectWeaponIcon;

        //   correctPercentageText.text = statsPerEnemy.CorrectPercentage.ToString("F0") + "%";
    }





    class HitRangeSlot
    {
        
    }
    
    void SetIcons()
    {
        // if (enemiesGroupedInStat.Count > 0)
        // {
        //     if (enemiesGroupedInStat[0].Icon != null)
        //     {
        //         enemyIcon.sprite = enemiesGroupedInStat[0].Icon;
        //     }
        //     else
        //     {
        //         Debug.Log("ERROR: Enemy stats display has no icon");
        //     }
        //
        //     if (enemiesGroupedInStat[0].CorrectWeaponsToGetShot != null && enemiesGroupedInStat[0].CorrectWeaponsToGetShot.Count > 0 && enemiesGroupedInStat[0].CorrectWeaponsToGetShot[0].Icon != null)
        //     {
        //         correctWeaponIcon.sprite = enemiesGroupedInStat[0].CorrectWeaponsToGetShot[0].Icon;
        //     }
        //     else
        //     {
        //         Debug.Log("ERROR: Enemy stats display has no correct weapon icon");
        //     }
        // }
    }


    void OnValidate()
    {
        // bool hasCorrectWeapon = true;
        //
        // foreach (var enemy in enemiesGroupedInStat)
        // {
        //     if (!enemy.CorrectWeaponsToGetShot.Contains(correctWeapon))
        //     {
        //         hasCorrectWeapon = false;
        //     }
        // }
        //
        // if (!hasCorrectWeapon)
        // {
        //     Debug.Log("ERROR: Enemy stats display has wrong weapon");
        // }
        //
        // SetIcons();
    }
}