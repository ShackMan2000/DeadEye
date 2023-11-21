using System;
using UnityEngine;

public class ShootWithMouse : MonoBehaviour
{


    
    
    [SerializeField] Shooter shooter;

    [SerializeField] Camera centerEyeCamera;
    
    
    //when clicking, call shoot from camera into the game world

    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     // get mouse position
        //     Vector3 mousePosition = Input.mousePosition;
        //     // get direction from camera to mouse position
        //     Vector3 direction = centerEyeCamera.ScreenToWorldPoint(mousePosition);
        //     // shoot bullet
        //     shooter.ShootBullet(centerEyeCamera.transform.position, direction);
        //     
        // }
    }
}