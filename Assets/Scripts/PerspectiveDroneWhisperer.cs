using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRavljen.UnitFormation.Demo;

public class PerspectiveDroneWhisperer : MonoBehaviour
{
    public DeadeyeUnit deadeyeUnit;

    private void Start()
    {

        deadeyeUnit = GameObject.FindGameObjectWithTag("UnitFormationController").GetComponent<DeadeyeUnit>();


        deadeyeUnit.unitCount++;
        deadeyeUnit.MakeNewFriend4(gameObject.transform, gameObject);
    }

}
