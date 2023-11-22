using System;
using System.Collections.Generic;
using UnityEngine;

public class ShotReceiver : MonoBehaviour
{
    public List<WeaponType> DamagedBy  = new List<WeaponType>();
    
    public List<WeaponType> MustBeDestroyedBy  = new List<WeaponType>();

    
    
    //public event Action OnDestroyedByCorrectWeapon = delegate {  };
    
    
    // might just want to make this a component and implement the logic so it's the same for all of them
    public bool DestroyedByCorrectWeapon(WeaponType weaponType)
    {
        return MustBeDestroyedBy.Contains(weaponType);
    }

}