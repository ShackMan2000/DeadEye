using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class CheckPointSetter : MonoBehaviour
{
    
    [SerializeField] CheckPointsList checkPointsList;
    
    [SerializeField] List<Transform> checkPointsMarkers;


    
    [Button]
    void SetCheckPoints()
    {
        checkPointsList.CheckPoints = new List<Vector3>();
        foreach (Transform marker in checkPointsMarkers)
        {
            checkPointsList.CheckPoints.Add(marker.position);
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(checkPointsList);
#endif
    }


}