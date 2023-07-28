using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // The prefab of the projectile the enemy fires
    public Transform shootPoint; // The position from where the projectile will be shot
    public float fireInterval = 1.5f; // Time interval between shots
    public float inaccuracyAngle = 5f; // The maximum angle in degrees for inaccuracy
    public GameObject blueProjectile;
    public GameObject redProjectile;
    private Transform mainCamera;
    private bool canShoot = true;
    public DeadEyeColorGuy colorGuy;

    private void Start()
    {
        mainCamera = Camera.main.transform;
         if (colorGuy.thisIsBlue)
        {
            projectilePrefab = blueProjectile;
        }
        else
        {
            projectilePrefab = redProjectile;
        }
        
    }

    private void Update()
    {   
        if (canShoot)
        {
            StartCoroutine(ShootWithDelay());
        }
    }

    private IEnumerator ShootWithDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(Random.Range(fireInterval - 0.5f, fireInterval + 0.5f)); // Add some randomness to the fire interval

        ShootProjectile();
        canShoot = true;
    }

    private void ShootProjectile()
    {
        Vector3 directionToCamera = (mainCamera.position - shootPoint.position).normalized;
        float inaccuracy = Random.Range(0f, inaccuracyAngle) * Mathf.Deg2Rad;
        Quaternion randomRotation = Quaternion.Euler(Random.Range(-inaccuracy, inaccuracy), Random.Range(-inaccuracy, inaccuracy), 0f);
       /*  if (colorGuy.thisIsBlue)
        {
            projectilePrefab = blueProjectile;
        }
        else
        {
            projectilePrefab = redProjectile;
        } */
        
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation * randomRotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Set velocity based on the direction to the camera and inaccuracy
        projectileRb.velocity = directionToCamera * 10f; // Adjust the speed as needed
    }
}