using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRavljen.UnitFormation.Demo;

public class DroneWhisperer : MonoBehaviour
{
    public DeadeyeUnit deadeyeUnit;

    private void Start()
    {

        deadeyeUnit = GameObject.FindGameObjectWithTag("UnitFormationController").GetComponent<DeadeyeUnit>();


        deadeyeUnit.unitCount++;
        deadeyeUnit.MakeNewFriend(gameObject.transform, gameObject);
    }

}
