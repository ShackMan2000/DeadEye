using UnityEngine;


public class BulletImpacts : MonoBehaviour
{



 

    // pool them
    
    [SerializeField] GameObject bulletImpactPrefab;


    // void OnEnable()
    // {
    //     Shooter.OnHitObjectNotShootable += SpawnBulletImpact;
    // }
    //
    // void OnDisable()
    // {
    //     Shooter.OnHitObjectNotShootable -= SpawnBulletImpact;
    //
    // }

    void SpawnBulletImpact(Vector3 position, Vector3 faceNormal)
    {
        
        GameObject bulletImpact = Instantiate(bulletImpactPrefab, transform);
        bulletImpact.transform.position = position;
        
        Destroy(bulletImpact, 1f);
    }


}