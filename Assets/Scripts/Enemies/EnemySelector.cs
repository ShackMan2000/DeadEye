using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class EnemySelector : MonoBehaviour
{

    [SerializeField] GameSettings settings;


    [SerializeField] List<EnemySettings> selectedEnemies;

    [SerializeField] List<EnemyToggle> enemyToggles;



    [Button]
    void SetUpToggles()
    {
        
    }


}


