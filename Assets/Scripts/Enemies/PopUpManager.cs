using System;
using System.Collections.Generic;
using UnityEngine;


public class PopUpManager : MonoBehaviour
{
    [SerializeField] PlayerPosition playerPosition;
    
    [SerializeField] PopUp  popUpPrefab;
    
    List<PopUp> popUpPool = new List<PopUp>();


    void OnEnable()
    {
        MultiDrone.OnMultiDroneShot += SpawnMultiDroneAccuracyPopUp;
        ScoreController.OnPointsForKillAwarded += SpawnPointsPopUp;
    }
    
    void OnDisable()
    {
        MultiDrone.OnMultiDroneShot -= SpawnMultiDroneAccuracyPopUp;
        ScoreController.OnPointsForKillAwarded -= SpawnPointsPopUp;
    }

    // spawn one with points when enemy destroyed, unless it was a single one with wrong weapon

    // pool them

    // can get the score multiplier from the score multi, but the tricky thing is that a wrong hit will be 0
    
    // show points pop up for single shot when it explodes, grab the score multi before it increases
    // show a percentage pop up whenever a multidrone gets hit, make it quick so it will be gone before drone explodes
    // show points pop up when multidrone gets destroyed. In both cases can use explosion, but need the extra info if it was correct weapon
    // and for multi drone shot need extra info of position
    

}