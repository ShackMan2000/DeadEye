using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShotReceiver : MonoBehaviour
{
  //  public List<WeaponType> DamagedBy = new List<WeaponType>();

    [FormerlySerializedAs("MustBeDestroyedBy")] public List<WeaponType> CorrectWeaponsToGetShot = new List<WeaponType>();
    

    public event Action<bool> OnShotByCorrectWeapon = delegate { };

 
    public bool ShootingBlocked;

    // ball drones get destroyed with one shot, depth drones just need even and get blocked for another shot.
            
    public void GetShot(WeaponType weaponType)
    {
        if (ShootingBlocked)
        {
            return;
        }
            
            
        bool shotByCorrectWeapon = CorrectWeaponsToGetShot.Contains(weaponType);

        OnShotByCorrectWeapon(shotByCorrectWeapon);
    }



}