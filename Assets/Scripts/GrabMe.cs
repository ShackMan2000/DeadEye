using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GrabMe : MonoBehaviour
{
    public event Action onGrabbed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   public void Grabbed()
    {
         if (onGrabbed != null)
        {
            onGrabbed();
        }
    }

   

}
