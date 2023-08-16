using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using BNG;
using TRavljen.UnitFormation.Demo;

public class HPManager : MonoBehaviour
{
    public float HP;
    public TMP_Text hpText;
    public UnityEvent GameOver;
    public DeadeyeUnit UnitGuy;
  //  public TimeController time;
    public GameObject[] Lasers;
    public GameObject KillSpawner;
    public GameObject[] Drones;
   /*  public bool Waiting;
    public float WaitingTime;
  
     */
    private void Awake()
    {
        hpText = this.GetComponent<TMP_Text>();
        HP = 5f;
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
       /* Waiting = true; */
        Lasers = GameObject.FindGameObjectsWithTag("ActualLaser");
        foreach (GameObject obj in Lasers)
        {
            Destroy(obj);
        }
                HP = HP -1f;
        HPUpdate();
       // time.ResumeTime();
     /*    WaitingTimeActivate(); */
    }

    public void GameOverGuy()
    {   
         KillSpawner = GameObject.FindWithTag("EnemySpawner");
        Drones = GameObject.FindGameObjectsWithTag("DRONE");
        //UnitGuy = GameObject.FindWithTag("UnitFormationController").GetComponent<DeadeyeUnit>();
        GameObject.Destroy(KillSpawner);
        foreach (GameObject unit in Drones)
        {
            Destroy(unit);
        }

        HP = 5f;

       // Drones.Clear(); // Clear the list after destroying all units

       // UnitGuy.DestroyAllUnits();
    }

    public void GameStartGuy()
    {
        HP = 5f;
        HPUpdate();
    }
        }
   /* public  System.Collections.IEnumerator WaitingTimeActivate()
    {
         yield return new WaitForSeconds(WaitingTime);
         Waiting = false;
         
    }     */
    
