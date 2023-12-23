using System.Collections.Generic;
using System.Linq;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;
using Sirenix.OdinInspector;
using UnityEngine;


public class PathManager : ConnectedControlPointsSelector
{
    [SerializeField] List<CurvyConnection> allConnections;

    [ShowInInspector]
    List<FreeIndexesForConnections> freeIndexesForConnections;


    
    [Button]
    public void CreateFreeIndexes()
    {
        freeIndexesForConnections = new List<FreeIndexesForConnections>();

        foreach (var connection in allConnections)
        {
            FreeIndexesForConnections freeIndexesForConnection = new FreeIndexesForConnections();
            freeIndexesForConnection.FreeIndexes = new List<int>();

            for (int i = 0; i < connection.ControlPointsList.Count; i++)
            {
                freeIndexesForConnection.FreeIndexes.Add(i);
            }

            freeIndexesForConnections.Add(freeIndexesForConnection);
        }
    }
    
   

    public override CurvySplineSegment SelectConnectedControlPoint(SplineController caller, CurvyConnection connection, CurvySplineSegment currentControlPoint)
    {
        // find connection
        int connectionIndex = allConnections.FindIndex(x => x == connection);
        
        if (connectionIndex == -1)
        {
            Debug.LogError("Connection not found");
            return null;
        }
        
        // ah fucking hell need to make sure to not spawn an enemy when there is no free path. Make method in here
        
        
        


        return connection.ControlPointsList.Last();
    }


    [System.Serializable]
    public class FreeIndexesForConnections
    {
        public List<int> FreeIndexes;
    }
}