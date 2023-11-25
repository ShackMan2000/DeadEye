using System.Collections.Generic;
using UnityEngine;


public class SideDrone : MonoBehaviour
{
    public void GetHitByLaser()
    {
        transform.localScale = new Vector3(2f, 2f, 2f);
        Destroy(gameObject, 2f);
    }
}