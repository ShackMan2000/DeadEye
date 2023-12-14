using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MultiDroneStatsRangeSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI correctPercentageText;

    public Vector2 RangeRelative;

    public bool IsCorrectRange;

    public void SetRangeAndHeight(Vector2 range, float height)
    {
        // get layout component and set height
        
        GetComponent<LayoutElement>().flexibleHeight = height *100;
        RangeRelative = range;
    }
    
}