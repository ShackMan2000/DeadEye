using System.Collections.Generic;
using Backend;
using UnityEngine;


public class ClearStatsButton : MonoBehaviour
{
    
    [SerializeField] StatsPanel statsPanel;

    [SerializeField] GameObject reallyClearStatsPanel;
    
    
    public void ShowReallyClearStatsPanel()
    {
        reallyClearStatsPanel.SetActive(true);
    }
    
    public void CancelClearStats()
    {
        reallyClearStatsPanel.SetActive(false);
    }
    
    
    public void ClearStats()
    {
        SaveManager.Instance.ClearSaveData();
        statsPanel.RemoveAllGraphs();
        reallyClearStatsPanel.SetActive(false);
    }


}