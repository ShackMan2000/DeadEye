using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class CanvasMouseControl : MonoBehaviour
{



    [SerializeField] GameObject curvedMesh;

    [SerializeField] Camera mainCamera;
    // use a different camera for events


    public bool autoEnable;


    void Start()
    {
        if (autoEnable)
        {
            EnableMouseControl();
        }
    
    }

    [Button]
    void EnableMouseControl()
    {
        gameObject.SetActive(true);
        curvedMesh.SetActive(false);
        
        // set culling mask to everything
        mainCamera.cullingMask = -1;
        
        
    }

}