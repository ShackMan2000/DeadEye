using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SideDrone : MonoBehaviour
{
    [SerializeField] List<Renderer> renderers;
    Material burnMaterial;
    static readonly int Burn = Shader.PropertyToID("_Burn");

    public Vector3 StartPositionLocal { get; set; }

    public void GetHitByLaser(Vector3 laserDirection, EnemySettings settings)
    {
        StartCoroutine(GetBurnedUpRoutine(laserDirection, settings));
    }


    IEnumerator GetBurnedUpRoutine(Vector3 laserDirection, EnemySettings settings)
    {
        float time = 0;

        while (time < settings.LaserExpansionTime / 4f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        time = 0;

        while (time < settings.LaserKnockbackTime)
        {
            time += Time.deltaTime;
            float progress = time / settings.LaserKnockbackTime;
            float speed = settings.LaserKnockbackSpeed * (1 - progress);
            transform.position += laserDirection * (speed * Time.deltaTime);

            foreach (Renderer render in renderers)
            {
                Material[] materials = render.materials;

                foreach (Material material in materials)
                {
                    material.SetFloat(Burn, progress);
                }

                render.materials = materials;
            }

            yield return null;
        }

        gameObject.SetActive(false);
    }

    public void ResetBurnMaterial()
    {
        foreach (Renderer render in renderers)
        {
            Material[] materials = render.materials;

            foreach (Material material in materials)
            {
                material.SetFloat(Burn, 0f);
            }

            render.materials = materials;
        }    }
}