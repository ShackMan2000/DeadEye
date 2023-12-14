using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyMultiStatsDisplay : MonoBehaviour
{
    // [SerializeField] Image enemyIcon;

    // [SerializeField] MultiDrone multiDrone;

    public EnemySettings EnemySettings;

    [SerializeField] Color outerRangeColor;
    [SerializeField] Color almostHitRangeColor;
    [SerializeField] Color correctRangeColor;


    [InfoBox("Make both together equal 1")] [SerializeField]
    float almostHitRangePercentage;

    [SerializeField] float outerRangePercentage;

    [SerializeField] List<MultiDroneStatsRangeSlot> rangeSlots;


    void Start()
    {
        SetRanges();
    }

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
        rangeSlots[0].SetColor(outerRangeColor);

        Vector2 almostHitRangeNegative = new Vector2(outerRangeNegative.y, outerRangeNegative.y + almostHitRange);
        rangeSlots[1].SetRangeAndHeight(almostHitRangeNegative, almostHitRange / 2f);
        rangeSlots[1].SetColor(almostHitRangeColor);

        rangeSlots[2].SetRangeAndHeight(new Vector2(-correctRange, correctRange), correctRange);
        rangeSlots[2].SetColor(correctRangeColor);

        Vector2 almostHitRangePositive = new Vector2(correctRange, correctRange + almostHitRange);
        rangeSlots[3].SetRangeAndHeight(almostHitRangePositive, almostHitRange / 2f);
        rangeSlots[3].SetColor(almostHitRangeColor);

        Vector2 outerHitRangePositive = new Vector2(almostHitRangePositive.y, 1f);
        rangeSlots[4].SetRangeAndHeight(outerHitRangePositive, outerRange / 2f);
        rangeSlots[4].SetColor(outerRangeColor);

        Debug.Log("sum of heights is" + (outerRange / 2f + almostHitRange / 2f + correctRange + almostHitRange / 2f + outerRange / 2f));
    }

    public void InjectStats(StatsMultiDrone s)
    {
        stats = s;
        if (EnemySettings.SideDronesMovementType == SideDronesMovementType.RotateAround)
        {
            AdjustShotRangesForRotation(s);
        }

        foreach (var rangeSlot in rangeSlots)
        {
            rangeSlot.ShotsInRange = 0;
        }

        stats = s;

        foreach (var shot in stats.rangeForEachShot)
        {
            for (int i = 0; i < rangeSlots.Count; i++)
            {
                if (shot >= rangeSlots[i].RangeRelative.x && shot <= rangeSlots[i].RangeRelative.y)
                {
                    rangeSlots[i].ShotsInRange++;
                }
            }
        }

        foreach (var rangeSlot in rangeSlots)
        {
            rangeSlot.SetPercentageText(stats.rangeForEachShot.Count);
        }
    }


    public void AdjustShotRangesForRotation(StatsMultiDrone s)
    {
        // only the z offset of the right side drone is logged, so it will travel through positive and negative values twice in one rotation.
     


        for (int i = 0; i < stats.rangeForEachShot.Count; i++)
        {
            float shot = stats.rangeForEachShot[i];
            float rotation = stats.rotationsRelativeWhenShot[i];

            if (shot >= 0f && rotation >= 0.25f)
            {
                stats.rangeForEachShot[i] = -shot;
            }
            else if (shot < 0f && rotation <= 0.75)
            {
                stats.rangeForEachShot[i] = -shot;
            }


            foreach (var rangeSlot in rangeSlots)
            {
                rangeSlot.SetPercentageText(stats.rotationsRelativeWhenShot.Count);
            }
        }


    }
}