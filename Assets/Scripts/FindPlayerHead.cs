using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayerHead : MonoBehaviour
{   public GameObject Destination;
    // Start is called before the first frame update
    void Awake()
    {   Destination = GameObject.FindWithTag("MainCamera");
        transform.SetParent(Destination.transform);

    
    }
}
