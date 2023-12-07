using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShootWithMouse : MonoBehaviour
{
    [SerializeField] GameObject debugBulletPrefab;

    [SerializeField] float bulletSpeed = 2f;

    
    
    [SerializeField] Shooter leftDebugGun;
    [SerializeField] Shooter rightDebugGun;
    Shooter currentShooter;
    
    List<BulletData> activeBullets = new List<BulletData>();

    [SerializeField] Camera centerEyeCamera;

    [SerializeField] bool useDebugBullets;

    [FormerlySerializedAs("weaponType")] [SerializeField] WeaponType leftWeapon;
    [SerializeField] WeaponType rightWeapon;
    


    void Awake()
    {     
        currentShooter = rightDebugGun;
    }


    // void SpawnDebugBullet(Vector3 direction)
    // {
    //     GameObject bullet = Instantiate(debugBulletPrefab, transform);
    //     bullet.transform.position = transform.position;
    //
    //     BulletData bulletData = new BulletData();
    //
    //     bulletData.Bullet = bullet;
    //     bulletData.Direction = direction;
    //     bulletData.Speed = bulletSpeed;
    //
    //     activeBullets.Add(bulletData);
    // }
        

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = centerEyeCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 direction = hit.point - transform.position;
                Debug.Log("Hit " + hit.collider.gameObject.name + " at " + hit.point + " with direction " + direction);

                currentShooter.ShootAndDetermineTarget(direction);
            }
        }

        foreach (BulletData bulletData in activeBullets)
        {
            bulletData.MoveBullet(Time.deltaTime);
        }
        
        // switch weapon on space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentShooter == leftDebugGun)
            {
                currentShooter = rightDebugGun;
            }
            else
            {
                currentShooter = leftDebugGun;
            }
        }
    }


    class BulletData
    {
        public GameObject Bullet;
        public Vector3 Direction;
        public float Speed;

        public void MoveBullet(float deltaTime)
        {
            Bullet.transform.position += Direction * Speed * deltaTime;
        }
    }
}