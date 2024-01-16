using System.Collections.Generic;
using UnityEngine;


public class SparksSpawner : MonoBehaviour
{
    [SerializeField] WeaponType blueWeaponType;
    [SerializeField] WeaponType redWeaponType;

    List<Sparks> blueSparksPool = new List<Sparks>();

    List<Sparks> redSparksPool = new List<Sparks>();


    void OnEnable()
    {
        Gun.OnShotHitSomething += SpawnBulletImpact;
    }

    void OnDisable()
    {
        Gun.OnShotHitSomething -= SpawnBulletImpact;
    }

    void SpawnBulletImpact(WeaponType weaponType, Vector3 position, Vector3 faceNormal)
    {
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

            blueSparks.PlaySparks();
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

            redSparks.PlaySparks();
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