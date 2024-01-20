using System.Collections.Generic;
using UnityEngine;


public class LeaderBoardEntriesList : MonoBehaviour
{
    [SerializeField] LeaderBoardEntryUI entryUIPrefab;


    [SerializeField] List<LeaderBoardEntryUI> entryDisplays = new List<LeaderBoardEntryUI>();
    [SerializeField] Transform contentTransform;
    
    public void ShowEntries(List<LeaderBoard.SimplifiedLeaderBoardEntry> entries)
    {
        for(int i = 0; i < entries.Count; i++)
        {
            if (i < entryDisplays.Count)
            {
                entryDisplays[i].gameObject.SetActive(true);
                entryDisplays[i].Initialize(entries[i]);
            }
            else
            {
                LeaderBoardEntryUI entryDisplay = Instantiate(entryUIPrefab, contentTransform);
                entryDisplay.gameObject.SetActive(true);
                
                entryDisplay.Initialize(entries[i]);
                entryDisplays.Add(entryDisplay);
            }
        }
        
        for(int i = entries.Count; i < entryDisplays.Count; i++)
        {
            entryDisplays[i].gameObject.SetActive(false);
        }
        
        entryUIPrefab.gameObject.SetActive(false);
    }
    
}