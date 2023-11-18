using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using BNG;

public class TargetScript : MonoBehaviour
{
    // Define the events for hitting different rings on the target
    public UnityEvent blueHitBullseyeEvent;
    public UnityEvent blueHitRingEvent;
    public UnityEvent blueWrongColorEvent;
    public UnityEvent redHitBullseyeEvent;
    public UnityEvent redHitRingEvent;
    public UnityEvent redWrongColorEvent;
    public ScoreManager scoreManager;
    public GameObject daddy;   
    public GameObject triggerLine;
    public bool isThisLinedUp;
    public bool AmITandem;

    public string TandemTag;

    // Adjust this value based on the size of your target rings
    public float ringSize = 1f;

    public Damageable DaddyDamage1;
    public Damageable DaddyDamage2;


    // Prefab of the bullet decal
    public GameObject bulletDecalPrefab;

    private void Start()
    {   
        scoreManager = GameObject.FindWithTag("SCORE").GetComponent<ScoreManager>();
        daddy = transform.parent.gameObject;
       
        
    }

    public void OnCollisionEnter(Collision collision)
    {   
       
    
          bool myBlue = daddy.GetComponent<Enemy1>().blue;
        // Check if the collision is with a projectile
        if ((collision.collider.CompareTag("BLUE") && myBlue)) 
        {
            // Calculate the distance from the collision point to the center of the target
            Vector3 collisionPoint = collision.GetContact(0).point;
            Vector3 center = transform.position;
            float distance = Vector3.Distance(collisionPoint, center);

            // Determine which event to invoke based on the distance
            if (distance < ringSize)
            {   
                scoreManager.Score30();
                blueHitBullseyeEvent.Invoke();
            }
            else
            {
                blueHitRingEvent.Invoke();
                scoreManager.Score10();
            }
             Instantiate(bulletDecalPrefab, collisionPoint, Quaternion.identity);


           
        }

        else if ((collision.collider.CompareTag("RED") && !myBlue)) 
        {
            // Calculate the distance from the collision point to the center of the target
            Vector3 collisionPoint = collision.GetContact(0).point;
            Vector3 center = transform.position;
            float distance = Vector3.Distance(collisionPoint, center);

            // Determine which event to invoke based on the distance
            if (distance < ringSize)
            {
                redHitBullseyeEvent.Invoke();
            }
            else
            {
                redHitRingEvent.Invoke();
            }

             Instantiate(bulletDecalPrefab, collisionPoint, Quaternion.identity);

        }
        else if ((collision.collider.CompareTag("RED") && myBlue))
        {
            blueWrongColorEvent.Invoke();
            scoreManager.MinusScore();
        }
        else if ((collision.collider.CompareTag("BLUE") && !myBlue))
        {   
            redWrongColorEvent.Invoke();
            scoreManager.MinusScore();
        }


    }

           
       
}
