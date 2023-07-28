using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BNG;

public class ClotheslineCollision : MonoBehaviour
{   
    public Damageable damageboy;
    public UnityEvent GotShot;    // Start is called before the first frame update

     private void Start() {
    damageboy = GameObject.FindWithTag("HEAD").GetComponent<Damageable>();    
    }
    void OnCollisionEnter(Collision other)
    {
       if (other.gameObject.CompareTag("HEAD"))
       {
        GotShot.Invoke();
       }
    }
}
