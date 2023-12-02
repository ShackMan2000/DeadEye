using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShooterDebug : MonoBehaviour
{
    [SerializeField] GameObject debugBulletPrefab;

    [SerializeField] float bulletSpeed = 2f;

    [SerializeField] Shooter shooter;

    List<BulletData> activeBullets = new List<BulletData>();

    [SerializeField] Camera centerEyeCamera;

    [SerializeField] bool useDebugBullets;

    [FormerlySerializedAs("weaponType")] [SerializeField] WeaponType leftWeapon;
    [SerializeField] WeaponType rightWeapon;
    
    WeaponType currentWeapon;


    void Awake()
    {
        currentWeapon = rightWeapon;
    }


    void SpawnDebugBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(debugBulletPrefab, transform);
        bullet.transform.position = transform.position;

        BulletData bulletData = new BulletData();

        bulletData.Bullet = bullet;
        bulletData.Direction = direction;
        bulletData.Speed = bulletSpeed;

        activeBullets.Add(bulletData);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = centerEyeCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 direction = hit.point - transform.position;

                if (useDebugBullets)
                {
                    SpawnDebugBullet(direction.normalized);
                }

                shooter.ShootAndDetermineTarget(transform.position, direction.normalized, currentWeapon);
            }
        }

        foreach (BulletData bulletData in activeBullets)
        {
            bulletData.MoveBullet(Time.deltaTime);
        }
        
        // switch weapon on space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentWeapon = currentWeapon == leftWeapon ? rightWeapon : leftWeapon;
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