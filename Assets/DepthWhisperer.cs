using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRavljen.UnitFormation.Demo;

public class DepthWhisperer : MonoBehaviour
{
    public DeadeyeUnit deadeyeUnit;

    private void Start()
    {

        deadeyeUnit = GameObject.FindGameObjectWithTag("UnitFormationController").GetComponent<DeadeyeUnit>();


        deadeyeUnit.unitCount++;
        deadeyeUnit.MakeNewFriend2(gameObject.transform, gameObject);
    }

}
