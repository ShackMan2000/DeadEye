using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SideDrone : MonoBehaviour
{
    [SerializeField] Renderer render;
    [SerializeField] Material burnMaterial;
    static readonly int Burn = Shader.PropertyToID("_Burn");

    public void GetHitByLaser(Vector3 laserDirection, DroneSettings settings)
    {
        StartCoroutine(GetBurnedUpRoutine(laserDirection, settings));
        
    }
    
    
    IEnumerator GetBurnedUpRoutine(Vector3 laserDirection, DroneSettings settings)
    {
        float time = 0;
        
        while (time < settings.LaserExpansionTime / 4f)
        {
            time += Time.deltaTime;
            yield return null;
        }
        
        render.material = burnMaterial;
        time = 0;

        while (time < settings.LaserKnockbackTime)
        {
            time += Time.deltaTime;
            float progress = time / settings.LaserKnockbackTime;
            float speed = settings.LaserKnockbackSpeed * (1 - progress);
            transform.position += laserDirection * (speed * Time.deltaTime);
            
            render.material.SetFloat(Burn, progress);
            
            yield return null;
        }
        
        gameObject.SetActive(false);
        
    }
}