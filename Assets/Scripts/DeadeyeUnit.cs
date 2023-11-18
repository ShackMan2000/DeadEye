using System.Collections.Generic;
using UnityEngine;
using TRavljen.UnitFormation.Formations;
//using TRavljen.UnitFormation;
using UnityEngine.UI;

namespace TRavljen.UnitFormation.Demo
{

    public class DeadeyeUnit : MonoBehaviour
    {

        #region Public Properties

        /// <summary>
        /// List of units in the scene
        /// </summary>
        public List<GameObject> units = new List<GameObject>();

        private bool isBlue = true;

        #endregion

        #region Inspector Properties

        /// <summary>
        /// Specifies the layer mask used for mouse point raycasts in order to
        /// find the drag positions in world/scene.
        /// </summary>
        [SerializeField] private LayerMask groundLayerMask;

        /// <summary>
        /// Specifies the line renderer used for rendering the mouse drag line
        /// that indicates the unit facing direction.
        /// </summary>
        [SerializeField] private LineRenderer LineRenderer;

        /// <summary>
        /// Specifies the unit count that will be generated for the scene.
        /// May be adjusted in realtime.
        /// </summary>
       // [SerializeField] private Slider UnitCountSlider;

        /// <summary>
        /// Specifies the unit spacing that will be used to generate formation
        /// positions.
        /// </summary>
       // [SerializeField] private Slider UnitSpacingSlider;

       // [SerializeField] private Slider RectangleColumnCountSlider;

        /// <summary>
        /// Specifies the <see cref="Text"/> used to represent the unit count
        /// selected by <see cref="UnitCountSlider"/>.
        /// </summary>
        //[SerializeField] private Text UnitCountText;

        /// <summary>
        /// Specifies the <see cref="Text"/> used to represent the unit spacing
        /// selected by <see cref="UnitSpacingSlider"/>.
        /// </summary>
      //  [SerializeField] private Text UnitSpacingText;

      //  [SerializeField] private Text RectangleColumnCountText;

        [SerializeField] private GameObject UnitPrefab = null;
        [SerializeField] private GameObject UnitPrefab2 = null;
        [SerializeField] private GameObject UnitPrefab3 = null;
        [SerializeField] private GameObject UnitPrefab4 = null;

        [SerializeField]
        public IFormation currentFormation;

        [SerializeField]
        private DeadEyeColorGuy deadEyeColorGuy;

        private bool isDragging = false;
        [SerializeField]
        private bool pivotInMiddle = false;
        public bool noiseEnabled = false;
        [SerializeField]
        public int unitCount;
        [SerializeField]
        private int rectangleColumnCount;
        [SerializeField]
        private float unitSpacing;

        #endregion

       // #region Private Properties
      /*   [SerializeField]
        private IFormation currentFormation;

        private bool isDragging = false;
        [SerializeField]
        private bool pivotInMiddle = false;
        private bool noiseEnabled = false;
        [SerializeField]
        private int unitCount => (int)UnitCountSlider.value;
        [SerializeField]
        private int rectangleColumnCount => (int)RectangleColumnCountSlider.value;
        [SerializeField]
        private float unitSpacing => UnitSpacingSlider.value;
 */
      //  #endregion

        private void Awake()
        {
            LineRenderer.enabled = false;
            SetUnitFormation(new LineFormation(unitSpacing));

            // Initial UI update
            //UpdateUnitCountText();
            UpdateUnitSpacing();
            UpdateRectangleColumnCountText();
        }

        private void Update()
        {
            for (int index = units.Count; index < unitCount; index++)   
                        {
                            if (units[index] == null)
                            {
                                units.RemoveAll(unit => unit == null);
                                
                                break;
                                
                            }
                           // ReinstantiateFormation();
                           // ApplyCurrentUnitFormation();
                        }

                // ApplyCurrentUnitFormation;

            }

        
           /*    if (units.Count < unitCount)
            {
               MakeNewFriend(gameObject.transform);

               
            }
              if (units.Count > unitCount)
            {

                
                            
                for (int index = units.Count - 1; index >= unitCount; index--)
                {
                    var gameObject = units[index];
                    units.RemoveAt(index);
                  //  Destroy(gameObject);
                } 
 */
               
           
          
          //   if (units.Count > 0)
         //   {
                //HandleMouseDrag();
        //    } 
            /* FindChildObjectsWithTag(transform, "DRONE");
           */

            
         public void RemoveEnemy(GameObject enemyToRemove)
    {
        if (enemyToRemove == null)
        {
            Debug.LogWarning("Trying to remove a null enemy.");
            return;
        }

        if (units.Contains(enemyToRemove))
        {       
            units.Remove(enemyToRemove);
            unitCount--;
            Destroy(enemyToRemove, 0.3f);
            // You may also want to trigger your methods like ReinstatiateFormation and ApplyCurrentFormation here.
            //ReinstantiateFormation();
           // ApplyCurrentUnitFormation();
        }
        else
        {
            Debug.LogWarning("Enemy not found in the units list.");
        }
    }

        private void HandleMouseDrag()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100, groundLayerMask))
                {
                    LineRenderer.enabled = true;
                    isDragging = true;

                    LineRenderer.SetPosition(0, hit.point);
                    LineRenderer.SetPosition(1, hit.point);
                }
            }
            else if (Input.GetKey(KeyCode.Mouse1) & isDragging)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100, groundLayerMask))
                {
                    LineRenderer.SetPosition(1, hit.point);

                }
            }
            if (Input.GetKeyUp(KeyCode.Mouse1) && isDragging)
            {
                isDragging = false;
                LineRenderer.enabled = false;
                ApplyCurrentUnitFormation();
            }
        }

        public void ApplyCurrentUnitFormation()
        {
            var direction = LineRenderer.GetPosition(1) - LineRenderer.GetPosition(0);

            UnitsFormationPositions formationPos;

            // Check if mouse drag was NOT minor, then we can calculate angle
            // (direction) for the mouse drag.
            if (direction.magnitude > 0.8f)
            {
                var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                var newPositions = FormationPositioner.GetAlignedPositions(
                    units.Count, currentFormation, LineRenderer.GetPosition(0), angle);

                formationPos = new UnitsFormationPositions(newPositions, angle);
            }
            else
            {
                var currentPositions = units.ConvertAll(obj => obj.transform.position);
                formationPos = FormationPositioner.GetPositions(
                    currentPositions, currentFormation, LineRenderer.GetPosition(0));
            }

            for (int index = 0; index < units.Count; index++)
            {
                Vector3 pos = formationPos.UnitPositions[index];
                if (noiseEnabled)
                {
                    pos += UnitFormationHelper.GetNoise(0.2f);
                }

                if (units[index].TryGetComponent(out FormationUnit unit))
                {
                    unit.SetTargetDestination(pos, formationPos.FacingAngle);
                }
            }
        }

        private void SetUnitFormation(IFormation formation)
        {
            currentFormation = formation;
            ApplyCurrentUnitFormation();
        }

        #region User Interactions

        public void OnNoiseToggleChanged(bool newState)
        {
            noiseEnabled = newState;

            ReinstantiateFormation();
            ApplyCurrentUnitFormation();
        }

        public void OnPivotToggleChanged(bool newState)
        {
            pivotInMiddle = newState;

            ReinstantiateFormation();
            ApplyCurrentUnitFormation();
        }

        public void LineFormationSelected() =>
            SetUnitFormation(new LineFormation(unitSpacing));

        public void CircleFormationSelected() =>
            SetUnitFormation(new CircleFormation(unitSpacing));

        public void TriangleFormationSelected() =>
            SetUnitFormation(new TriangleFormation(unitSpacing));

        public void ConeFormationSelected() =>
            SetUnitFormation(new ConeFormation(unitSpacing, pivotInMiddle));

        public void RectangleFormationSelected() =>
            SetUnitFormation(new RectangleFormation(rectangleColumnCount, unitSpacing, true, pivotInMiddle));

        public void UpdateRectangleColumnCountText()
        {
           // RectangleColumnCountText.text = "Units per ROW: " + rectangleColumnCount;

            if (currentFormation is RectangleFormation)
            {
                ReinstantiateFormation();
                ApplyCurrentUnitFormation();
            }
        }

       /*  public void UpdateUnitCountText()
        {
            UnitCountText.text = "Unit Count: " + unitCount;
        } */

        public void UpdateUnitSpacing()
        {
            //UnitSpacingText.text = $"Unit Spacing: {unitSpacing.ToString(("0.00"))}";

            ReinstantiateFormation();
            ApplyCurrentUnitFormation();
        }

        /// <summary>
        /// Instantiates a new formation based on the current type with the new
        /// configurations applied from UI.
        /// </summary>
        public void ReinstantiateFormation()
        {
            if (currentFormation is LineFormation)
            {
                currentFormation = new LineFormation(unitSpacing);
            }
            else if (currentFormation is RectangleFormation rectangleFormation)
            {
                currentFormation = new RectangleFormation(
                    rectangleColumnCount, unitSpacing, true, pivotInMiddle);
            }
            else if (currentFormation is CircleFormation)
            {
                currentFormation = new CircleFormation(unitSpacing);
            }
            else if (currentFormation is TriangleFormation)
            {
                currentFormation = new TriangleFormation(unitSpacing, pivotInMiddle: pivotInMiddle);
            }
            else if (currentFormation is ConeFormation)
            {
                currentFormation = new ConeFormation(unitSpacing, pivotInMiddle);
            }
        }

        public void MakeNewFriend(Transform SpawnerLocation, GameObject ParentFriend)
        {
             for (int index = units.Count; index < unitCount; index++)
                {
                    var newFriend = Instantiate(
                        UnitPrefab, SpawnerLocation.position, Quaternion.identity);
                    newFriend.transform.parent = gameObject.transform;
                    deadEyeColorGuy = newFriend.GetComponentInChildren<DeadEyeColorGuy>();
                    units.Insert(index, newFriend);

                ParentFriend.transform.parent = newFriend.transform;
                    
                    deadEyeColorGuy.DeadEyeColorCheck(isBlue);
                    isBlue = !isBlue;

                    ApplyCurrentUnitFormation();
                    
                }
        }

        public void MakeNewFriend2(Transform SpawnerLocation, GameObject ParentFriend2)
        {
            for (int index = units.Count; index < unitCount; index++)
            {
                var newFriend2 = Instantiate(
                    UnitPrefab2, SpawnerLocation.position, Quaternion.identity);
                newFriend2.transform.parent = gameObject.transform;
                deadEyeColorGuy = newFriend2.GetComponentInChildren<DeadEyeColorGuy>();
                units.Insert(index, newFriend2);

                ParentFriend2.transform.parent = newFriend2.transform;

                deadEyeColorGuy.DeadEyeColorCheck(isBlue);
                isBlue = !isBlue;

                ApplyCurrentUnitFormation();

            }
        }

        public void MakeNewFriend3(Transform SpawnerLocation, GameObject ParentFriend3)
        {
            for (int index = units.Count; index < unitCount; index++)
            {
                var newFriend3 = Instantiate(
                    UnitPrefab3, SpawnerLocation.position, Quaternion.identity);
                newFriend3.transform.parent = gameObject.transform;
                deadEyeColorGuy = newFriend3.GetComponentInChildren<DeadEyeColorGuy>();
                units.Insert(index, newFriend3);

                ParentFriend3.transform.parent = newFriend3.transform;

                deadEyeColorGuy.DeadEyeColorCheck(isBlue);
                isBlue = !isBlue;

                ApplyCurrentUnitFormation();

            }
        }

        public void MakeNewFriend4(Transform SpawnerLocation, GameObject ParentFriend4)
        {
            for (int index = units.Count; index < unitCount; index++)
            {
                var newFriend4 = Instantiate(
                    UnitPrefab4, SpawnerLocation.position, Quaternion.identity);
                newFriend4.transform.parent = gameObject.transform;
                deadEyeColorGuy = newFriend4.GetComponentInChildren<DeadEyeColorGuy>();
                units.Insert(index, newFriend4);

                ParentFriend4.transform.parent = newFriend4.transform;

                deadEyeColorGuy.DeadEyeColorCheck(isBlue);
                isBlue = !isBlue;

                ApplyCurrentUnitFormation();

            }
        }

        public void DestroyAllUnits()
    {
        foreach (GameObject unit in units)
        {
            Destroy(unit);
        }

        units.Clear(); // Clear the list after destroying all units
    }
        

 /*         void FindChildObjectsWithTag(Transform parent, string tag)
    {
        int childCount = parent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag(tag))
            {
                units.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                FindChildObjectsWithTag(child, tag);
            }
        }
    } */

        #endregion

    }

}
