using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShootWithMouse : MonoBehaviour
{

    [SerializeField] Gun leftDebugGun;
    [SerializeField] Gun rightDebugGun;
    Gun currentGun;
    

    [SerializeField] Camera centerEyeCamera;


    [SerializeField] bool DEBUGShootWithMouse;
    
    public List<Transform> originalGuns;
    

    void Awake()
    {     
        currentGun = rightDebugGun;

#if ! UNITY_EDITOR
        DEBUGShootWithMouse = false;
#endif

        if (DEBUGShootWithMouse)
        {
            //scale original guns to 0.05f
            foreach (Transform gun in originalGuns)
            {
                gun.localScale = Vector3.one * 0.05f;
            }
        }
    }



        

    void Update()
    {
        if(!DEBUGShootWithMouse)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = centerEyeCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 direction = hit.point - currentGun.BulletSpawnPoint.position;
                //Debug.Log("Hit " + hit.collider.gameObject.name + " at " + hit.point + " with direction " + direction);

                currentGun.ShootAndDetermineTarget(direction);
            }
        }

    
        
        // switch weapon on space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentGun == leftDebugGun)
            {
                currentGun = rightDebugGun;
            }
            else
            {
                currentGun = leftDebugGun;
            }
        }
    }


    // class BulletData
    // {
    //     public GameObject Bullet;
    //     public Vector3 Direction;
    //     public float Speed;
    //
    //     public void MoveBullet(float deltaTime)
    //     {
    //         Bullet.transform.position += Direction * Speed * deltaTime;
    //     }
    // }
}