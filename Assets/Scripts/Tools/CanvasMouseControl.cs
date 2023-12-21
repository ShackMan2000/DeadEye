using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class CanvasMouseControl : MonoBehaviour
{

    [SerializeField] Canvas canvas;


    [SerializeField] Camera mouseCamera;
    // use a different camera for events


    [Button]
    void EnableMouseControl()
    {
        gameObject.SetActive(true);
        
        
        
    }

}