using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MultiDrone : MonoBehaviour
{
    // 

    // drone should be aligned perependicularly to the player

// account for laser being thicker than the raycast
    [SerializeField] ShotReceiver coreShotReceiver;


    [SerializeField] DroneSettings settings;

    [SerializeField] float laserDistance = 10f;

    [SerializeField] List<Transform> lasers;
    [SerializeField] List<Renderer> laserRenderers;

    [SerializeField] List<SideDrone> sideDrones;
    static readonly int AlphaReveal = Shader.PropertyToID("_AlphaReveal");


    bool freezeSideDrones = false;
    // must have a side thing[s], that is only damageable and destructable by the laser

    // need this because freezing and unfreezing the side drones should use same time.time
    float totalTimePassed = 0;

    // spawn new core...

    void OnEnable()
    {
        coreShotReceiver.OnDestroyedByCorrectWeapon += SpawnLasers;
    }

    void OnDisable()
    {
        coreShotReceiver.OnDestroyedByCorrectWeapon -= SpawnLasers;
    }


    [Button]
    void SpawnLasers(bool correctWeapon)
    {
        StartCoroutine(ShowLasersRoutine());
        
        

        int sideDronesHit = 0;
        foreach (Transform laser in lasers)
        {
            //raycast all 
            RaycastHit[] hits = Physics.RaycastAll(laser.position, laser.forward, laserDistance);


            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out SideDrone sideDrone))
                {
                    sideDronesHit++;
                    sideDrone.GetHitByLaser(laser.forward, settings);
                }
            }
        }

        if (sideDronesHit > 0 && sideDronesHit != sideDrones.Count)
        {
            Debug.LogError("Side drones hit is not 0 or equal to side drones count. Probably be an error because laser shout hit all or none");
        }

        if (sideDronesHit > 0)
        {
            sideDrones.Clear();
        }
    }


    IEnumerator ShowLasersRoutine()
    {
        freezeSideDrones = true;
        
        float timePassed = 0;

        while (timePassed < settings.LaserExpansionTime)
        {
            timePassed += Time.deltaTime;
            float t = timePassed / settings.LaserExpansionTime;
            foreach (Renderer laserRenderer in laserRenderers)
            {
                laserRenderer.material.SetFloat(AlphaReveal, t);
            }

            yield return null;
        }

        timePassed = 0;
        while (timePassed < settings.LaserStayTime)
        {
            timePassed += Time.deltaTime;
            yield return null;
        }

        timePassed = 0;
        while (timePassed < settings.LaserShrinkTime)
        {
            timePassed += Time.deltaTime;
            float t = 1 - timePassed / settings.LaserShrinkTime;
            foreach (Renderer laserRenderer in laserRenderers)
            {
                laserRenderer.material.SetFloat(AlphaReveal, t);
            }

            yield return null;
        }

        freezeSideDrones = false;
        coreShotReceiver.ShootingBlocked = false;
    }

    void Update()
    {
        if (!freezeSideDrones)
        {
            MoveSideDrones();
        }
    }

    void MoveSideDrones()
    {
        totalTimePassed += Time.deltaTime;
        float sineWave = Mathf.Sin(totalTimePassed * settings.BackAndForthSpeed);

        

        foreach (SideDrone sideDrone in sideDrones)
        {
            float newZ = sineWave * settings.BackAndForthDistance;
            sideDrone.transform.localPosition = new Vector3(sideDrone.transform.localPosition.x, sideDrone.transform.localPosition.y, newZ);
        }
    }

    // move back and forth in sine wave
    // make a debug check if they are aligned, change color or something...
}