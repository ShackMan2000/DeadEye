using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;


public class PopUpManager : MonoBehaviour
{
    [SerializeField] PlayerPosition playerPosition;

    [FormerlySerializedAs("popUpPrefab")] [SerializeField] PopUp pointsPopUpPrefab;
    [SerializeField] PopUp accuracyPopUpPrefab;

    [SerializeField] Vector3 offset;

    List<PopUp> pointsPopUpsPool = new List<PopUp>();
    List<PopUp> accuracyPopUpsPool = new List<PopUp>();

    // 



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
        PopUp popUp = GetPointsPopUp();
        popUp.transform.position = position + offset;
        
        // look away from player
        popUp.transform.LookAt(playerPosition.Position);
        popUp.transform.Rotate(0, 180, 0);
        
        float distanceToPlayer = Vector3.Distance(position, playerPosition.Position);
        
        popUp.SetTextAndScale(points.ToString("F0"), distanceToPlayer);
        popUp.GrowAndFade();
    }


    void SpawnMultiDroneAccuracyPopUp(MultiDroneHitInfo hitInfo)
    {
        // popUp.transform.position = hitInfo.Position + offset;
        //
        // popUp.transform.LookAt(playerPosition.Position);
        // popUp.transform.Rotate(0, 180, 0);
        //
        // float distanceToPlayer = Vector3.Distance(hitInfo.Position, playerPosition.Position);
        //
        //
        // popUp.SetTextAndScale((hitInfo.OffsetOnShotRelative * 100f).ToString("F0") + "%", distanceToPlayer);
        // popUp.GrowAndFade();
    }



    PopUp GetPointsPopUp()
    {
        if (pointsPopUpsPool.Count > 0)
        {
            PopUp popUp = pointsPopUpsPool[0];
            pointsPopUpsPool.RemoveAt(0);
            return popUp;
        }
        else
        {
            PopUp popUp = Instantiate(pointsPopUpPrefab, transform);
            popUp.Initialize(this);
            return popUp;
        }
    }


    public Transform testDrone;

    [Button]
    void SetOffset()
    {
        offset = pointsPopUpPrefab.transform.position - testDrone.position;
    }

    public bool testLookat;
    
    void Update()
    {
        if (testLookat)
        {
            pointsPopUpPrefab.transform.LookAt(playerPosition.Position);
            pointsPopUpPrefab.transform.Rotate(0, 180, 0);
        }
    }
    
    // might be easiest to let the mulidrone handle the always pop up. Could just request one from popUpManager.
    // or maybe have it's own...

    // spawn one with points when enemy destroyed, unless it was a single one with wrong weapon

    // pool them

    // can get the score multiplier from the score multi, but the tricky thing is that a wrong hit will be 0

    // show points pop up for single shot when it explodes, grab the score multi before it increases
    // show a percentage pop up whenever a multidrone gets hit, make it quick so it will be gone before drone explodes
    // show points pop up when multidrone gets destroyed. In both cases can use explosion, but need the extra info if it was correct weapon
    // and for multi drone shot need extra info of position
    public void ReturnToPool(PopUp popUp)
    {
        pointsPopUpsPool.Add(popUp);
        popUp.gameObject.SetActive(false);
    }
}