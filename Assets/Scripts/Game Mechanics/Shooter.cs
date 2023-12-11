using System;
using UnityEngine;
using UnityEngine.Serialization;


public class Shooter : MonoBehaviour
{
   public WeaponType SelectedWeaponType;

  public Transform BulletSpawnPoint;

    [SerializeField] WeaponType leftGun;
    [SerializeField] WeaponType rightGun;


    [SerializeField] bool debugShotHits;
    
    bool isPressed;
    float pressedThreshold = 0.9f;
    float releaseThreshold = 0.1f;


    public event Action<WeaponType> OnShotFired = delegate { };

    public static event Action<Vector3, Vector3> OnHitObjectNotShootable = delegate { };
    public static event Action<bool> ShotHitEnemy = delegate { }; 


    public void ShootAndDetermineTarget(Vector3 direction)
    {
        Vector3 startPosition = BulletSpawnPoint.position;
        
        ShotReceiver shotReceiver = null;
        GameObject hitObject = null;

        if (Physics.Raycast(startPosition, direction, out RaycastHit hit, Mathf.Infinity))
        {
            hitObject = hit.collider.gameObject;
            // Try get is a little bit slower than geting a component that exists, but a lot faster than getting one that doesn't exist
            shotReceiver = hit.collider.TryGetComponent(out shotReceiver) ? shotReceiver : null;


            if (shotReceiver == null)
            {
                OnHitObjectNotShootable(hit.point, hit.normal);
                ShotHitEnemy?.Invoke(false);
            }
            else
            {
                shotReceiver.GetShot(SelectedWeaponType);
                ShotHitEnemy?.Invoke(true);
            }
        }

        OnShotFired?.Invoke(SelectedWeaponType);
        
        if(debugShotHits)
        {
            Debug.Log("Fired a shot with  " + SelectedWeaponType + " and hit " + hitObject + " which has a shot receiver of " + shotReceiver);
        }
    }


    void Update()
    {
        float triggerValue;

        if (SelectedWeaponType == leftGun)
        {
            triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        }
        else
        {
            triggerValue = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        }

        
        if (triggerValue > pressedThreshold)
        {
            if (!isPressed)
            {
                ShootAndDetermineTarget(BulletSpawnPoint.forward);
            }

            isPressed = true;
        }
        else if (triggerValue < releaseThreshold)
        {
            isPressed = false;
        }
    }


   
}