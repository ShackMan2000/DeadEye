using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[InlineEditor]
[CreateAssetMenu(menuName = "Checkpoints")]
public class CheckPointsList : ScriptableObject
{
    [EnumToggleButtons]
    public EnemyDifficulty Difficulty;

    public List<Vector3> CheckPoints;

    [ShowInInspector] List<int> availableIndexesForLinger;


    public void ResetFreeIndexes()
    {
        availableIndexesForLinger = new List<int>();

        for (int i = 0; i < CheckPoints.Count; i++)
        {
            availableIndexesForLinger.Add(i);
        }
    }


    public int GetFreeIndex()
    {
        if (availableIndexesForLinger.Count == 0)
        {
            Debug.Log("ERROR! No more free indexes for linger, adding random to avoid crash");

            //using a point between though to make it unlikely to be below ground etc.
            int randomIndexA = Random.Range(0, CheckPoints.Count);
            int randomIndexB = Random.Range(0, CheckPoints.Count);
            
            Vector3 randomPoint = (CheckPoints[randomIndexA] + CheckPoints[randomIndexB]) / 2f;
            CheckPoints.Add(randomPoint);
            availableIndexesForLinger.Add(CheckPoints.Count - 1);
        }


        int index = availableIndexesForLinger[Random.Range(0, availableIndexesForLinger.Count)];
        availableIndexesForLinger.Remove(index);

        
        return index;

    }

    public void FreeUpIndex(int reservedLingerPointIndex)
    {
        availableIndexesForLinger.Add(reservedLingerPointIndex);
    }
}
