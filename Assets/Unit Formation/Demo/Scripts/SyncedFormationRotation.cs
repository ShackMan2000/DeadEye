using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TRavljen.UnitFormation.Demo
{

    /// <summary>
    /// Demonstration component for syncing direction change of all units.
    /// This will prevent units from facing formation angle until all units
    /// have reached their destination.
    /// </summary>
    public class SyncedFormationRotation : MonoBehaviour
    {

        public  DeadeyeUnit formationControls;
         List<FormationUnit> formationUnits => formationControls.units
            .ConvertAll(unit => unit.GetComponent<FormationUnit>());

        void Start()
        {
           // formationControls = GetComponent<DeadeyeUnit>();
        }

        void Update()
        {
             bool areAllPositioned = true;

            formationUnits.ForEach(unit =>
            {
                if (!unit.IsWithinStoppingDistance)
                {
                    areAllPositioned = false;
                }
            });

            formationUnits.ForEach(unit => unit.FacingRotationEnabled = areAllPositioned);
        }

    }

}