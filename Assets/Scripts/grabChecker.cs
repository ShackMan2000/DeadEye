using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabChecker : MonoBehaviour
{

    public bool Grabbed;
    // Start is called before the first frame update
   
   void Start()
   { 
    Grabbed = false;
   }
   public void grabMe()

   {

    Grabbed = true;
   }
}
