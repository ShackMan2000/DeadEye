using System.Collections.Generic;
using UnityEngine;


public class SparksSpawner : MonoBehaviour
{
    [SerializeField] WeaponType blueWeaponType;
    [SerializeField] WeaponType redWeaponType;

    List<Sparks> blueSparksPool = new List<Sparks>();

    List<Sparks> redSparksPool = new List<Sparks>();

    [SerializeField] AudioClip hitEnemySound;
    [SerializeField] AudioClip hitGroundSound;
    
    
// turn into struct. So it can have more information what it hit
// hit enemy or not. If enemy, metal sound, otherwise ground sound. pass audioclip into sparks initialize.

    void OnEnable()
    {
        Gun.OnShotHitSomething += SpawnBulletImpact;
    }

    void OnDisable()
    {
        Gun.OnShotHitSomething -= SpawnBulletImpact;
    }

    void SpawnBulletImpact(Gun.ShotHitInfo shotHitInfo)
    {
        WeaponType weaponType = shotHitInfo.weaponType;
        Vector3 position = shotHitInfo.hitPoint;
        Vector3 faceNormal = shotHitInfo.hitNormal;
        
        AudioClip audioClip = shotHitInfo.hitEnemy ? hitEnemySound : hitGroundSound;
        
        
        if (weaponType == blueWeaponType)
        {
            Sparks blueSparks;

            if (blueSparksPool.Count == 0)
            {
                blueSparks = Instantiate(weaponType.SparksPrefab, position, Quaternion.LookRotation(faceNormal));
                blueSparks.Initialize(this, blueWeaponType);
            }
            else
            {
                blueSparks = blueSparksPool[0];
                blueSparks.transform.position = position;
                blueSparks.transform.rotation = Quaternion.LookRotation(faceNormal);
                blueSparksPool.RemoveAt(0);
            }

            blueSparks.PlaySparks(audioClip);
        }
        

        if (weaponType == redWeaponType)
        {
            Sparks redSparks;

            if (redSparksPool.Count == 0)
            {
                redSparks = Instantiate(weaponType.SparksPrefab, position, Quaternion.LookRotation(faceNormal));
                redSparks.Initialize(this, redWeaponType);
            }
            else
            {
                redSparks = redSparksPool[0];
                redSparks.transform.position = position;
                redSparks.transform.rotation = Quaternion.LookRotation(faceNormal);
                redSparksPool.RemoveAt(0);
            }

            redSparks.PlaySparks(audioClip);
        }
    }
    
    
    
    public void ReturnToPool(Sparks sparks)
    {
        if (sparks.WeaponType == blueWeaponType)
        {
            blueSparksPool.Add(sparks);
        }
        else if (sparks.WeaponType == redWeaponType)
        {
            redSparksPool.Add(sparks);
        }
    }
}