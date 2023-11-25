using System;
using System.Collections.Generic;
using UnityEngine;

public class ShotReceiver : MonoBehaviour
{
    public List<WeaponType> DamagedBy = new List<WeaponType>();

    public List<WeaponType> MustBeDestroyedBy = new List<WeaponType>();

    public event Action<bool> OnDestroyedByCorrectWeapon = delegate { };

    public bool IsDestroyed;
    
    
    // might be useful later to get shot multiple times
    public void GetShot(WeaponType weaponType) => ShootAndDestroy(weaponType);
    

    public void ShootAndDestroy(WeaponType weaponType)
    {
        bool destroyedByCorrectWeapon = MustBeDestroyedBy.Contains(weaponType);


        OnDestroyedByCorrectWeapon(destroyedByCorrectWeapon);


        IsDestroyed = true;
    }
}