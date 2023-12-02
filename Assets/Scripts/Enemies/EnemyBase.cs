using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class EnemyBase : MonoBehaviour
{

    // could do the settings directly in here, a list of options
    // would need a bool first so wave controller can check if there is any option avaialble for this wave

    [ShowInInspector] EnemySettings settings;
    
    [SerializeField] EnemyMovement movement;    



    public void Initialize(EnemySettings settingsOption, CheckPointsList checkPointsList)
    {
        settings = settingsOption;
        movement.Initialize(settings, checkPointsList);
    }
}


