using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class IncreaseHitBox : MonoBehaviour
{

    
    
    [SerializeField] float hitboxMultiplier = 2f;

    
    
    [Button]
    void IncreaseHitBoxSize()
    {
        var shotReceivers = FindObjectsOfType<ShotReceiver>();
        
        foreach (var shotReceiver in shotReceivers)
        {
            // get sphere collider
            var sphereCollider = shotReceiver.GetComponent<SphereCollider>();
            if (sphereCollider == null)
            {
                Debug.LogError("No sphere collider found on " + shotReceiver.name);
                continue;
            }
                    //multiply radius
            sphereCollider.radius *= hitboxMultiplier;
        }
    }
    
    
}