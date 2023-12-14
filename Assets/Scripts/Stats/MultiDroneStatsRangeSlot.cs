using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MultiDroneStatsRangeSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI correctPercentageText;

    [SerializeField] Image background;
    public Vector2 RangeRelative;

    public bool IsCorrectRange;
    
    public int ShotsInRange;


    public void SetRangeAndHeight(Vector2 range, float height)
    {
        // get layout component and set height
        
        GetComponent<LayoutElement>().flexibleHeight = height *100;
        RangeRelative = range;
    }

    public void SetColor(Color outerRangeColor)
    {
        background.color = outerRangeColor;
    }

    public void SetPercentageText(int totalShots)
    {
        correctPercentageText.text = ((float)ShotsInRange / totalShots * 100f).ToString("F0") + "%";
    }
}