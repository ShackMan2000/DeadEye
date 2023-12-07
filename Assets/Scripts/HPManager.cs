using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public int HP;
    public Text hpText;
    public UnityEvent GameOver;
  //  public TimeController time;
    public GameObject[] Lasers;
    public GameObject KillSpawner;
    public GameObject[] Drones;
   /*  public bool Waiting;
    public float WaitingTime;
  
     */
    private void Awake()
    {
        hpText = this.GetComponent<Text>();
        HP = 3;
        HPUpdate();
       /*  Waiting = false; */
    
    }

    public void HPUpdate()
    {
    
        hpText.text = (""+HP);

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
                HP--;
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

        HP = 3;

       // Drones.Clear(); // Clear the list after destroying all units

       // UnitGuy.DestroyAllUnits();
    }

    public void GameStartGuy()
    {
        HP = 3;
        HPUpdate();
    }
        }
   /* public  System.Collections.IEnumerator WaitingTimeActivate()
    {
         yield return new WaitForSeconds(WaitingTime);
         Waiting = false;
         
    }     */
    
