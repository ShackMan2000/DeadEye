using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public AudioSource source;
    public AudioClip ShieldSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyLaser"))
        //if red
        {
            source.PlayOneShot(ShieldSound);
            Destroy(other.gameObject);
        }
    }
}
