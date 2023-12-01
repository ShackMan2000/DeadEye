using System.Collections.Generic;
using UnityEngine;


public class EnemyBase : MonoBehaviour
{

    // could do the settings directly in here, a list of options
    // would need a bool first so wave controller can check if there is any option avaialble for this wave

    EnemySettings settings;
    
    public void InjectSettings(EnemySettings s)
    {
        settings = s;
    }
    

}