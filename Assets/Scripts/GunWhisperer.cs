using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRavljen.UnitFormation.Demo;

public class GunWhisperer : MonoBehaviour
{
    public DeadeyeUnit deadeyeUnit;

    private void Start()
    {

        deadeyeUnit = GameObject.FindGameObjectWithTag("UnitFormationController").GetComponent<DeadeyeUnit>();


        deadeyeUnit.unitCount++;
        deadeyeUnit.MakeNewFriend3(gameObject.transform, gameObject);
    }

}
