using UnityEngine;

public class HitDetector : MonoBehaviour
{

    public AudioSource source;
    public AudioClip explosionSound;
    public GameObject explosionPrefab;
    
   private void OnTriggerEnter(Collider other) {
    
    if (other.gameObject.CompareTag("Laser"))
    {
        var enemy = this.transform.gameObject;
        enemy.SendMessage("GotHit");
    }
   }

   public void Explode()

   {

        var enemy = this.transform.parent.gameObject;

        var exp = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(enemy);
        Destroy(exp, 0.4f);
        var playerHP = GameObject.FindGameObjectsWithTag("MainCamera");
        playerHP[1].SendMessage("Score");


   }
}
