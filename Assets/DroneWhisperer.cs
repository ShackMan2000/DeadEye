using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRavljen.UnitFormation.Demo;

public class DroneWhisperer : MonoBehaviour
{
    public DeadeyeUnit deadeyeUnit;

    private void Awake() {
        deadeyeUnit.unitCount++;
        deadeyeUnit.MakeNewFriend(gameObject.transform);
    }

    
}
