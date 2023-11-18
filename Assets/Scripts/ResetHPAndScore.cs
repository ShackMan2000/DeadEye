using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetHPAndScore : MonoBehaviour
{
    public float ResetHP;
    public float Score;
    public GameObject HPHolder;
    public GameObject ScoreHolder;

    // Start is called before the first frame update
     void Awake() 
    {
       HPHolder.GetComponent<HPManager>().HP = 3;
       ScoreHolder.GetComponent<ScoreManager>().Score = 0;
    }

   
}
