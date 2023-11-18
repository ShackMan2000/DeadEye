using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGun : MonoBehaviour
{
    public AudioClip hapticAudioClip;
    public AudioSource source;
    public AudioClip laserSound;

    public GameObject laser;

    public float shootPower = 1f;

    public Transform barrelLocation;

    private float fireRate = 0.05f;
    private float nextFire = 0.0f;

    public bool triggerDown;

    public bool rightController = false;
    public bool leftController = false;


    // Start is called before the first frame update
    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

    }

    // Update is called once per frame
    void Update()
    {
        CheckTrigger();
        Shoot();   
    }

    private void CheckTrigger()
    {

        OVRInput.Controller controller;
        if (rightController == true)
        {
            controller = OVRInput.Controller.RTouch;
        }   else
        {
            controller = OVRInput.Controller.LTouch;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            triggerDown = true;
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            triggerDown = false;
        }
    
    }

    private void Shoot()
    {
        if(triggerDown && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(laser, barrelLocation.position, barrelLocation.rotation * Quaternion.Euler(90f, 0f, 0f)).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shootPower);
            source.PlayOneShot(laserSound);
            
            OVRHapticsClip hapticsClip = new OVRHapticsClip(hapticAudioClip);
            OVRHaptics.RightChannel.Preempt(hapticsClip);
                    }
    }

}
