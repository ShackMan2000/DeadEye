using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRavljen.UnitFormation.Demo;

public class UnitFormation2Finder : MonoBehaviour
{
    public void RemoveMyself()
    {
        DeadeyeUnit deadeyeUnit = GameObject.FindWithTag("UnitFormationController2").GetComponent<DeadeyeUnit>();

        if (deadeyeUnit != null)

        {
            deadeyeUnit.RemoveEnemy(gameObject);
        }
    }
}
