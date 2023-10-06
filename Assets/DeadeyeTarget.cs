using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using BNG;

public class DeadeyeTarget : MonoBehaviour
{
    // Define the events for hitting different rings on the target
    public UnityEvent wrongColorEvent;
    public UnityEvent rightColorEvent;
    public ScoreManager scoreManager;
       
    public uint rightScore;
    public uint wrongScore;

   
    

    public Damageable daddy;


    // Prefab of the bullet decal
    
   

    private void Start()
    {   
        scoreManager = GameObject.FindWithTag("SCORE").GetComponent<ScoreManager>();
        daddy = GetComponent<Damageable>();
       
        // Check if the component was found
       
        
    }

    public void OnCollisionEnter(Collision collision)
    {   
       
        ContactPoint contact = collision.GetContact(0);
            Vector3 collisionPoint = contact.point;
      
            // Instantiate the prefab at the collision point
         
           
        
        
        // Check if the collision is with a projectile
        if (collision.collider.CompareTag(gameObject.tag)) 
        {       
                DynamicTextData data = GetComponent<Enemy>().textData;
                daddy.DealDamage(rightScore);
                rightColorEvent.Invoke();
                scoreManager.score(rightScore);
                DynamicTextManager.CreateText(collisionPoint, rightScore.ToString(), data);

        } 
             

        else if (collision.collider.CompareTag("RED") || collision.collider.CompareTag("BLUE")) 
        {
                DynamicTextData data = GetComponent<Enemy>().textData;
                wrongColorEvent.Invoke();
                scoreManager.score(wrongScore);
                
                DynamicTextManager.CreateText(collisionPoint, wrongScore.ToString(), data);

        }

                   
    }

}
