using System;
using System.Collections.Generic;
using UnityEngine;

public class ShotReceiver : MonoBehaviour
{
    public List<WeaponType> DamagedBy = new List<WeaponType>();

    public List<WeaponType> MustBeDestroyedBy = new List<WeaponType>();

    public event Action<bool> OnShotByCorrectWeapon = delegate { };
    public event Action<bool> OnDestroyedByCorrectWeapon = delegate { };

    public bool IsDestroyed;

    public bool ShootingBlocked;

    // might be useful later to get shot multiple times
    public void GetShot(WeaponType weaponType)
    {
        if (ShootingBlocked)
        {
            return;
        }
            
            
        bool shotByCorrectWeapon = DamagedBy.Contains(weaponType);

        OnShotByCorrectWeapon(shotByCorrectWeapon);

        // for now only have one shot -> destroy, but might change
        GetDestroyed(weaponType);
    }


     void GetDestroyed(bool correctWeapon)
    {


        OnDestroyedByCorrectWeapon(correctWeapon);


        IsDestroyed = true;
        ShootingBlocked = true;
    }
}