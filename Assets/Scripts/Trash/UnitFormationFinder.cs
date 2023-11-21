using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRavljen.UnitFormation.Demo;

public class UnitFormationFinder : MonoBehaviour
{
    public void RemoveMyself()
    {
        DeadeyeUnit deadeyeUnit = GameObject.FindWithTag("UnitFormationController").GetComponent<DeadeyeUnit>();

        if (deadeyeUnit!= null)

        {
            deadeyeUnit.RemoveEnemy(gameObject);
        }
    }
}
