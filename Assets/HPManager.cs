using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using BNG;

public class HPManager : MonoBehaviour
{
    public float HP;
    public TMP_Text hpText;
    public UnityEvent GameOver;

    public TimeController time;
    public GameObject[] Lasers;
   /*  public bool Waiting;
    public float WaitingTime;
  
     */
    private void Awake()
    {
        hpText = this.GetComponent<TMP_Text>();
        HP = 3f;
        HPUpdate();
       /*  Waiting = false; */
    
    }

    public void HPUpdate()
    {
    
        hpText.text = ("HP: " + HP);

        if (HP<=0)
        { 
            GameOver.Invoke();
        }
    }

    public void Hit() {
 /*    if (!Waiting){ */
    {   /* Waiting = true; */
        Lasers = GameObject.FindGameObjectsWithTag("ActualLaser");
        foreach (GameObject obj in Lasers)
        {
            Destroy(obj);
        }
                HP = HP -1f;
        HPUpdate();
        time.ResumeTime();
     /*    WaitingTimeActivate(); */
    }
    }
    }
   /* public  System.Collections.IEnumerator WaitingTimeActivate()
    {
         yield return new WaitForSeconds(WaitingTime);
         Waiting = false;
         
    }     */
    
