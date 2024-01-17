using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class PopUpManager : MonoBehaviour
{
    [SerializeField] PlayerPosition playerPosition;

    [SerializeField] PopUp popUpPrefab;

    [SerializeField] Vector3 offset;

    List<PopUp> popUpPool = new List<PopUp>();

    // best tool to find offset?
    // run in update


    // allright, need to spawn a pop up above the drone
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


    void SpawnPointsPopUp(float points, Vector3 position)
    {
        PopUp popUp = GetPopUp();
        popUp.transform.position = position + offset;
        
        // look away from player
        popUp.transform.LookAt(playerPosition.Position);
        popUp.transform.Rotate(0, 180, 0);
        
        float distanceToPlayer = Vector3.Distance(position, playerPosition.Position);
        
        popUp.SetText(points.ToString("F0"), distanceToPlayer);
    }


    void SpawnMultiDroneAccuracyPopUp(MultiDroneHitInfo hitInfo)
    {
        // if (popUpPrefab == null)
        // {
        //     Debug.LogError("PopUp prefab is null");
        //     return;
        // }
        //
        // PopUp popUp = GetPopUp();
        // popUp.transform.position = hitInfo.Position + offset;
        // popUp.SetText(hitInfo.Accuracy.ToString());
        // popUp.gameObject.SetActive(true);
    }


    PopUp GetPopUp()
    {
        if (popUpPool.Count > 0)
        {
            PopUp popUp = popUpPool[0];
            popUpPool.RemoveAt(0);
            return popUp;
        }
        else
        {
            PopUp popUp = Instantiate(popUpPrefab, transform);
            popUp.Initialize(this);
            return popUp;
        }
    }


    public Transform testDrone;

    [Button]
    void SetOffset()
    {
        offset = popUpPrefab.transform.position - testDrone.position;
    }

    public bool testLookat;
    
    void Update()
    {
        if (testLookat)
        {
            popUpPrefab.transform.LookAt(playerPosition.Position);
            popUpPrefab.transform.Rotate(0, 180, 0);
        }
    }

    // spawn one with points when enemy destroyed, unless it was a single one with wrong weapon

    // pool them

    // can get the score multiplier from the score multi, but the tricky thing is that a wrong hit will be 0

    // show points pop up for single shot when it explodes, grab the score multi before it increases
    // show a percentage pop up whenever a multidrone gets hit, make it quick so it will be gone before drone explodes
    // show points pop up when multidrone gets destroyed. In both cases can use explosion, but need the extra info if it was correct weapon
    // and for multi drone shot need extra info of position
    public void ReturnToPool(PopUp popUp)
    {
        popUpPool.Add(popUp);
        popUp.gameObject.SetActive(false);
    }
}