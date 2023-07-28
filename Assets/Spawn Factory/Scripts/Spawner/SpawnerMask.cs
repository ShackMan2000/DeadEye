using SpawnFactory.Triangles;
using SpawnFactory.UnityExtensions;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if VEGETATION_STUDIO_PRO
using AwesomeTechnologies.VegetationSystem.Biomes;
#endif

namespace SpawnFactory
{
    public class SpawnerMask : MonoBehaviour
    {
        [SerializeField] private bool showDisabledInspectorFields = true;
        [SerializeField] private bool showBoundsAlways = true;
        [SerializeField] private bool showArea = true;
        [SerializeField] private bool showHandles = true;
        [SerializeField] private bool showNodes = true;
        [SerializeField] private bool showRotArrow = true;
        [SerializeField] private bool showName = true;
        [SerializeField] private bool useObjectName = true;
        [SerializeField] private string maskName = "";
        [SerializeField] protected SpawnType spawnType = SpawnType.Area;
        [SerializeField] protected SpawnMethod spawnMethod = SpawnMethod.Instantiate;
        [SerializeField] private LayerMask groundLayerMask = new LayerMask();
        [SerializeField] [Range(0.1f, 10)] private float heightFromGround = 0.3f;
        [SerializeField] private bool navMeshSpawn = false;
        [SerializeField] private AvoidancePrecision obstacleAvoidPrecision = AvoidancePrecision.None;
        [SerializeField] private LayerMask obstaclesToAvoidLayers = new LayerMask();
        [SerializeField] private bool useRandomStartRotation = false;
        [SerializeField] private Vector3 startRotation = Vector3.zero;

        public Color bgColor = Color.black;
        public Color fgColor = Color.white;

#if VEGETATION_STUDIO_PRO
    [SerializeField] private bool spawnInBiome = false;
    [SerializeField] private BiomeMaskArea vspBiome;
#endif

        [SerializeField] public List<Vector3> Nodes = new List<Vector3>();

#if UNITY_EDITOR
        public int curEditorTab = 0;
#endif

#if VEGETATION_STUDIO_PRO
    [HideInInspector] [SerializeField] private List<Vector3> previousNodes;
    public bool SpawnInBiome { get { return spawnInBiome; } private set { spawnInBiome = value; } }
    public BiomeMaskArea VSPBiome { get { return vspBiome; } private set { vspBiome = value; } }
#endif

        public enum AvoidancePrecision { None, Low, Medium, High }
        public enum SpawnMethod { Pooling, Instantiate }
        public enum SpawnType { Area, Point }

        public bool NavMeshSpawn { get { return navMeshSpawn; } private set { navMeshSpawn = value; } }
        public AvoidancePrecision ObstacleAvoidPercision { get { return obstacleAvoidPrecision; } private set { obstacleAvoidPrecision = value; } }
        public SpawnType SPAWNTYPE { get { return spawnType; } private set { spawnType = value; } }
        public SpawnMethod SPAWNMETHOD { get { return spawnMethod; } private set { spawnMethod = value; } }
        public bool UseRandomStartRotation { get { return useRandomStartRotation; } private set { useRandomStartRotation = value; } }
        public Vector3 StartRotation { get { return startRotation; } private set { startRotation = value; } }
        public LayerMask GroundLayerMask { get { return groundLayerMask; } private set { groundLayerMask = value; } }
        public LayerMask ObstaclesToAvoidLayers { get { return obstaclesToAvoidLayers; } private set { obstaclesToAvoidLayers = value; } }
        public List<Triangle> Triangles { get; private set; }
        public float TotalArea { get; private set; }

        protected bool ShowBoundsAlways { get { return showBoundsAlways; } private set { showBoundsAlways = value; } }

        #region Unity Callbacks
        // Start is called before the first frame update
        protected virtual void Start()
        {
#pragma warning disable CS0162 // Unreachable code detected
            if (false)
                Debug.Log(showDisabledInspectorFields + " " + showHandles + " " + useObjectName + " " + heightFromGround);
#pragma warning restore CS0162 // Unreachable code detected

            if (Triangles == null || TotalArea == 0)
            {
                if (Nodes.Count > 2)
                {
                    InitializeTriangles();
                }
            }
        }

        public void Reset()
        {
            ClearNodes();
            CreateDefaultNodes();
        }
        #endregion

        #region Initialize
        public void InitializeTriangles()
        {
            List<Vector3> nodeWorldPos = GetWorldSpaceNodePositions();
            Triangles = nodeWorldPos.Triangulate();
            TotalArea = 0;
            // calculate total area
            foreach (Triangle tri in Triangles)
                TotalArea += tri.area;
        }

        public void CreateDefaultNodes()
        {
            Bounds tempBounds = new Bounds(new Vector3(0, 0, 0), new Vector3(6f, 1f, 6f));
            ClearNodes();
            for (int i = 0; i <= 3; i++)
            {
                Vector3 node = new Vector3();
                switch (i)
                {
                    case 0:
                        node = new Vector3(tempBounds.extents.x, tempBounds.extents.y, tempBounds.extents.z);
                        break;
                    case 1:
                        node = new Vector3(-tempBounds.extents.x, tempBounds.extents.y, tempBounds.extents.z);
                        break;
                    case 2:
                        node = new Vector3(-tempBounds.extents.x, tempBounds.extents.y, -tempBounds.extents.z);
                        break;
                    case 3:
                        node = new Vector3(tempBounds.extents.x, tempBounds.extents.y, -tempBounds.extents.z);
                        break;
                }
                Nodes.Add(node);
            }
        }
        #endregion

        #region Add/Delete Node
        /// <summary>
        /// Add a node to Nodes list in its correct position
        /// </summary>
        /// <param name="worldPosition">The node's position in world space</param>
        /// <returns>Returns new List of nodes</returns>
        public List<Vector3> AddNode(Vector3 worldPosition)
        {
            List<Vector3> nodes = Nodes;
            if (nodes.Count == 0)
            {
                nodes.Add(transform.InverseTransformPoint(worldPosition));
                return nodes;
            }

            Vector3 nearestNode = FindNearestNode(worldPosition);
            float nextDistance = GetDisToNextNode(nearestNode, worldPosition);
            float previousDistance = GetDisToPrevNode(nearestNode, worldPosition);
            int currentNodeIndex = GetNodeIndex(nearestNode);

            Vector3 localNode = transform.InverseTransformPoint(worldPosition);
            if (nextDistance < previousDistance)
            {
                // If not at end of list insert, otherwise add
                if (currentNodeIndex != nodes.Count - 1)
                    nodes.Insert(currentNodeIndex + 1, localNode);
                else
                    nodes.Add(localNode);
            }
            else
                nodes.Insert(currentNodeIndex, localNode);

            return nodes;
        }

        /// <summary>
        /// Removes a Node from the List
        /// </summary>
        /// <param name="node">Node to remove</param>
        /// <returns>Returns new List of Nodes</returns>
        public List<Vector3> DeleteNode(Vector3 node)
        {
            List<Vector3> newNodes = Nodes;
            newNodes.Remove(node);
            return Nodes;
        }
        #endregion

        #region Getter Functions
        /// <summary>
        /// Get distance from the next nearest node to the one being added
        /// </summary>
        /// <param name="nearestNode">closest node to the new node</param>
        /// <param name="nodeAdded">node being added</param>
        /// <returns>Return distance from next nearest node to the node being added</returns>
        public float GetDisToNextNode(Vector3 nearestNode, Vector3 nodeAdded)
        {
            Vector3 nextNode = GetNextNode(nearestNode);
            V3LineSeg nextSegment = new V3LineSeg(transform.TransformPoint(nearestNode), transform.TransformPoint(nextNode));
            return nextSegment.DistanceTo(nodeAdded);
        }

        /// <summary>
        /// Get distance from the previous nearest node to the one being added
        /// </summary>
        /// <param name="nearestNode">closest node to the new node</param>
        /// <param name="nodeAdded">node being added</param>
        /// <returns>Return distance from previous nearest node to the node being added</returns>
        public float GetDisToPrevNode(Vector3 nearestNode, Vector3 nodeAdded)
        {
            Vector3 previousNode = GetPreviousNode(nearestNode);
            V3LineSeg previousSegment = new V3LineSeg(transform.TransformPoint(nearestNode), transform.TransformPoint(previousNode));
            return previousSegment.DistanceTo(nodeAdded);
        }

        /// <summary>
        /// Get the position of the node in the list
        /// </summary>
        /// <param name="node">node to get index of</param>
        /// <returns>Returns index in List of node</returns>
        public int GetNodeIndex(Vector3 node)
        {
            int nodeIndex = 0;
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i] == node)
                {
                    nodeIndex = i;
                    break;
                }
            }
            return nodeIndex;
        }

        /// <summary>
        /// Get next node in list from provided node
        /// </summary>
        /// <param name="node">Node to check what comes after it in list</param>
        /// <returns>Returns next node of provided node in Nodes Liste</returns>
        public Vector3 GetNextNode(Vector3 node)
        {
            int nodeIndex = 0;
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i] == node)
                {
                    nodeIndex = i;
                    break;
                }
            }

            if (nodeIndex == Nodes.Count - 1)
            {
                return Nodes[0];
            }
            else
            {
                return Nodes[nodeIndex + 1];
            }
        }

        /// <summary>
        /// Get previous node in list from provided node
        /// </summary>
        /// <param name="node">Node to check what comes before it in list</param>
        /// <returns>Returns previous node of provided node in Nodes List</returns>
        public Vector3 GetPreviousNode(Vector3 node)
        {
            int nodeIndex = 0;
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i] == node)
                {
                    nodeIndex = i;
                    break;
                }
            }

            if (nodeIndex == 0)
            {
                return Nodes[Nodes.Count - 1];
            }
            else
            {
                return Nodes[nodeIndex - 1];
            }
        }

        /// <summary>
        /// Get the nearest node in list to provided position
        /// </summary>
        /// <param name="worldPosition">Node position to check for nearest</param>
        /// <returns>Returns node nearest to worldPosition provided</returns>
        public Vector3 FindNearestNode(Vector3 worldPosition)
        {
            Vector3 returnNode = new Vector3();
            float smallestDistance = float.MaxValue;

            for (int i = 0; i < Nodes.Count; i++)
            {
                float distance = Vector3.Distance(worldPosition, transform.TransformPoint(Nodes[i]));
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    returnNode = Nodes[i];
                }
            }

            return returnNode;
        }

        /// <summary>
        /// Get nodes position converted to world space
        /// </summary>
        /// <returns>Returns Nodes positions converted to world space</returns>
        public List<Vector3> GetWorldSpaceNodePositions()
        {
            List<Vector3> worldSpaceNodeList = new List<Vector3>();

            for (int i = 0; i < Nodes.Count; i++)
            {
                worldSpaceNodeList.Add(transform.TransformPoint(Nodes[i]));
            }
            return worldSpaceNodeList;
        }

        /// <summary>
        /// Get nodes position converted to local space
        /// </summary>
        /// <returns>Returns Nodes positions converted to local space</returns>
        public List<Vector3> GetLocalSpaceNodePositions(List<Vector3> worldSpaceNodes)
        {
            List<Vector3> localSpaceNodeList = new List<Vector3>();

            foreach (Vector3 node in worldSpaceNodes)
                localSpaceNodeList.Add(transform.InverseTransformPoint(node));

            return localSpaceNodeList;
        }

        /// <summary>
        /// Get center point of all nodes
        /// </summary>
        /// <returns>Returns center point of all nodes</returns>
        public Vector3 GetMaskCenter()
        {
            List<Vector3> worldPositions = GetWorldSpaceNodePositions();
            return (GetMeanVector(worldPositions.ToArray()));
        }

        /// <summary>
        /// Get Mean of all positions
        /// </summary>
        /// <param name="positions"></param>
        /// <returns>Returns mean of all positions</returns>
        private Vector3 GetMeanVector(Vector3[] positions)
        {
            if (positions.Length == 0)
                return Vector3.zero;
            float x = 0f;
            float y = 0f;
            float z = 0f;
            foreach (Vector3 pos in positions)
            {
                x += pos.x;
                y += pos.y;
                z += pos.z;
            }
            return new Vector3(x / positions.Length, y / positions.Length, z / positions.Length);
        }
        #endregion

        #region More Functions
        /// <summary>
        /// Empty Nodes List
        /// </summary>
        public void ClearNodes()
        {
            Nodes.Clear();
        }

        /// <summary>
        /// Check if 2 Vector3 nodes are equal
        /// </summary>
        /// <param name="nodes1">First node</param>
        /// <param name="nodes2">Second node</param>
        /// <returns>Returns TRUE if equal, FALSE if they aren't</returns>
        public bool CheckNodesEqual(List<Vector3> nodes1, List<Vector3> nodes2)
        {
            if (nodes1 == null || nodes2 == null)
                return false;

            if (nodes1.Count != nodes2.Count)
                return false;
            else
            {
                for (int i = 0; i < nodes1.Count; i++)
                {
                    if (nodes1[i].x != nodes2[i].x || nodes1[i].y != nodes2[i].y || nodes1[i].z != nodes2[i].z)
                        return false;
                }
                return true;
            }
        }
        #endregion

        #region Gizmos
#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            if (showBoundsAlways)
                DrawGizmos();
        }

        protected virtual void OnDrawGizmosSelected()
        {
            if (!showBoundsAlways)
                DrawGizmos();
        }
#endif


#if UNITY_EDITOR
        void DrawGizmos()
        {
            Camera sceneviewCamera = GetCurrentSceneViewCamera();
            if (!sceneviewCamera) return;

            Vector3 center = GetMaskCenter();
            GUIStyle stLabel = new GUIStyle(EditorStyles.whiteLabel);
            stLabel.alignment = TextAnchor.MiddleCenter;
            float dis = Vector3.Distance(sceneviewCamera.transform.position, transform.position);

            bool show = false;
#if VEGETATION_STUDIO_PRO
        if (spawnType == SpawnType.Area || spawnInBiome)
            show = true;
#else
            if (spawnType == SpawnType.Area)
                show = true;
#endif
            if (Nodes.Count > 2 && show)
            {
                // Adjust object's transform to center
                if (transform.position != center)
                {
                    List<Vector3> worldSpaceNodes = GetWorldSpaceNodePositions();
                    transform.position = center;
                    Nodes = GetLocalSpaceNodePositions(worldSpaceNodes);
                    EditorUtility.SetDirty(this);
                }

                // Show spawner name
                if (maskName != "" && showName && dis < 250)
                {
                    stLabel.contentOffset = new Vector2(-22, 0);
                    Handles.Label(center, maskName, stLabel);
                }
            }

            // Show Start Rotation direction
            if (showRotArrow && !useRandomStartRotation)
            {
                stLabel.contentOffset = new Vector2(-10, 0);

                Vector3 rotPos = new Vector3(center.x - 3, center.y + 3, center.z);
                Handles.color = Color.cyan;
                Handles.ArrowHandleCap(0, rotPos, Quaternion.Euler(transform.rotation.eulerAngles + startRotation), 3, EventType.Repaint);
                Handles.DrawWireDisc(rotPos, new Vector3(0, 1, 0), 0.3f);
                if (dis < 25)
                    Handles.Label(rotPos, "Start Rot", stLabel);
            }

            if (showNodes && show)
            {
                for (int i = 0; i < Nodes.Count; i++)
                {
                    var distance = Vector3.Distance(sceneviewCamera.transform.position, transform.TransformPoint(Nodes[i]));

                    if (distance < 200)
                    {
                        Gizmos.color = new Color(1, 0, 0, 0.9f);
                        Gizmos.DrawIcon(transform.TransformPoint(Nodes[i]), "Spawn Factory/Node_Cog.png", true);
                    }
                }
            }

            // Draws the lines between each point
            if (showArea && show)
            {
                List<Vector3> worldPointsClosedList = GetWorldSpaceNodePositions();
                worldPointsClosedList.Add(worldPointsClosedList[0]);
                Handles.color = new Color(1, 1, 1, 1f);
                Handles.DrawAAPolyLine(5, worldPointsClosedList.ToArray());
            }
            else if (showArea && spawnType == SpawnType.Point)
            {
                Gizmos.color = new Color(1, 0, 0, 0.9f);
                //var distance = Vector3.Distance(sceneviewCamera.transform.position, transform.position);
                //Gizmos.DrawSphere(transform.position, 0.02f * distance);
                Gizmos.DrawIcon(transform.transform.position, "Spawn Factory/Node_Cog.png", true);
            }

#if VEGETATION_STUDIO_PRO
        if (vspBiome != null && spawnInBiome && Selection.activeGameObject != gameObject)
        {
            if (!CheckNodesEqual(vspBiome.GetWorldSpaceNodePositions(), previousNodes))
            {
                List<Vector3> biomeWSNodes = vspBiome.GetWorldSpaceNodePositions();
                List<Vector3> localSpaceNodes = GetLocalSpaceNodePositions(biomeWSNodes);
                Nodes = localSpaceNodes;
                InitializeTriangles();
                previousNodes = biomeWSNodes;
            }
        }
#endif
        }
#endif

#if UNITY_EDITOR
        public static Camera GetCurrentSceneViewCamera()
        {
            Camera[] sceneviewCameras = SceneView.GetAllSceneCameras();
            return sceneviewCameras.Length > 0 ? sceneviewCameras[0] : null;
        }
#endif
        #endregion
    }
}