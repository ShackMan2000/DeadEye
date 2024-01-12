using System;
using System.Collections;
using UnityEngine;


public class Gun : MonoBehaviour
{
    public WeaponType SelectedWeaponType;

    public Transform BulletSpawnPoint;

    [SerializeField] WeaponType leftGun;
    [SerializeField] WeaponType rightGun;

    [SerializeField] Transform chamber;

    [SerializeField] AudioSource shootingAudioSource;
    [SerializeField] bool debugShotHits;

    bool isPressed;
   [SerializeField] float pressedThreshold = 0.9f;
   [SerializeField] float releaseThreshold = 0.1f;


    public event Action<WeaponType> OnShotFired = delegate { };
    public event Action<float> OnShotFiredWithDistance = delegate {  };

    public static event Action<Vector3, Vector3> OnHitObjectNotShootable = delegate { };
    public static event Action<bool> ShotHitEnemy = delegate { };

    //
    // bool isChamberRotating;
    // float chamberRotationSpeed = 1000f;
    // float chamberRotationLeft;
    
    public bool IsLockedByOverheat;

    public void ShootAndDetermineTarget(Vector3 direction)
    {
        Vector3 startPosition = BulletSpawnPoint.position;

        ShotReceiver shotReceiver = null;
        GameObject hitObject = null;
        float distanceToHitPoint = 100000f;

        if (Physics.Raycast(startPosition, direction, out RaycastHit hit, Mathf.Infinity))
        {
            hitObject = hit.collider.gameObject;
            distanceToHitPoint = Vector3.Distance(startPosition, hit.point);
            // Try get is a little bit slower than geting a component that exists, but a lot faster than getting one that doesn't exist
            shotReceiver = hit.collider.TryGetComponent(out shotReceiver) ? shotReceiver : null;


            if (shotReceiver == null)
            {
              //  OnHitObjectNotShootable(hit.point, hit.normal);
                ShotHitEnemy?.Invoke(false);
            }
            else
            {
                // this will end the game, shot receiver tells enemy spawner  to disappear, which tells wave controller that wave is over
                // nah, keep this, move mesh a level down and in here just make sure that we are not in shooting mode
                shotReceiver.GetShot(SelectedWeaponType);
                ShotHitEnemy?.Invoke(true);
            }
        }

        OnShotFired?.Invoke(SelectedWeaponType);
        OnShotFiredWithDistance?.Invoke(distanceToHitPoint);

    }




    bool isVibrating;

    void Update()
    {
        if (!GameManager.ShootingModeActive)
        {
            return;
        }

        if (IsLockedByOverheat)
        {
            return;
        }
        
        
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
                PlayShootingAudio();
                if (isVibrating)
                {
                    StopAllCoroutines();
                }

                StartCoroutine(VibrationRoutine());
            }

            isPressed = true;
        }
        else if (triggerValue < releaseThreshold)
        {
            isPressed = false;
        }
    }


    IEnumerator VibrationRoutine()
    {
        isVibrating = true;
        if (SelectedWeaponType == leftGun)
        {
            OVRInput.SetControllerVibration(frequencyTest, amplitudeTest, OVRInput.Controller.LTouch);
            yield return new WaitForSeconds(vibrationDuration);
            OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LTouch);
        }
        else
        {
            OVRInput.SetControllerVibration(frequencyTest, amplitudeTest, OVRInput.Controller.RTouch);
            yield return new WaitForSeconds(vibrationDuration);
            OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
        }
        isVibrating = false;
    }


    public float frequencyTest =1f;
    public float amplitudeTest = 1f;
    public float vibrationDuration = 0.3f;

    void PlayShootingAudio()
    {
        if (shootingAudioSource != null)
        {
            shootingAudioSource.Play();
        }
    }
}