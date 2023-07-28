using SpawnFactory.UnityExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityExtensions;
#endif

namespace SpawnFactory.Pooling
{
    public class PoolingManager : MonoBehaviour
    {
        public static PoolingManager instance;

        public StringObjDictionary pooledGroups = StringObjDictionary.New<StringObjDictionary>();

#if UNITY_EDITOR
#if !UNITY_2020_2_OR_NEWER
        [ReorderableList(elementsAreSubassets = true)]
#endif
        [SerializeField] private string[] categories = new string[1];
        public string[] Categories { get { return categories; } }
        public int curEditorTab = 0;
#if !UNITY_2020_2_OR_NEWER
        [ReorderableList(elementsAreSubassets = true)]
#endif
        public List<ObjectToPool> currentPoolGroup;
        public int curCategoryTab = 0;
        public StringObjDictionary editorPooledGroups = StringObjDictionary.New<StringObjDictionary>();
#endif

        public Dictionary<string, Queue<GameObject>> poolDict = new Dictionary<string, Queue<GameObject>>();
        public Dictionary<string, PooledObject> pooledObjs = new Dictionary<string, PooledObject>();

        [System.Serializable]
        public class PoolGroup
        {
            public List<ObjectToPool> pooledObjs;
        }

        [System.Serializable]
        public struct ObjectToPool
        {
            public string poolTag;
            public GameObject pooledObject;
            public int amountToPool;
            public bool canExpandPool;
        }

        public struct PooledObject
        {
            public GameObject obj;
            public bool canExpandPool;
            public GameObject parent;

            public PooledObject(GameObject obj, bool canExpandPool, GameObject parent)
            {
                this.obj = obj;
                this.canExpandPool = canExpandPool;
                this.parent = parent;
            }
        }

        #region Unity Callbacks
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance);
            }
            else
            {
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            InitializePool();
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
        #endregion

        #region Initialize
        private void InitializePool()
        {
            /*if (pooledObjects.Count > 0)
            {
                foreach (ObjectToPool poolObj in pooledObjects)
                {
                    if (poolObj.pooledObject != null && poolObj.poolTag != "" && poolObj.amountToPool != 0)
                        AddObjectToPool(poolObj.poolTag, poolObj.pooledObject, poolObj.amountToPool, poolObj.canExpandPool, gameObject);
                }
            }*/

            // Add Pool groups to the main pool
            if (pooledGroups.dictionary.Count > 0)
            {
                foreach (KeyValuePair<string, PoolGroup> poolGroup in pooledGroups.dictionary)
                {
                    foreach (ObjectToPool poolObj in poolGroup.Value.pooledObjs)
                    {
                        if (poolObj.pooledObject != null && poolObj.poolTag != "" && poolObj.amountToPool != 0)
                            AddObjectToPool(poolObj.poolTag, poolObj.pooledObject, poolObj.amountToPool, poolObj.canExpandPool, gameObject);
                    }
                }
            }
        }
        #endregion

        #region Add To Pool
        public void AddObjectToPool(string poolTag, GameObject obj, int amount, bool canExpandPool, GameObject parent)
        {
            if (poolDict.ContainsKey(poolTag))
            {
                Queue<GameObject> queue = SpawnObjects(poolDict[poolTag], obj, amount, parent);
                poolDict[poolTag] = queue;
            }
            else
            {
                Queue<GameObject> queue = SpawnObjects(new Queue<GameObject>(), obj, amount, parent);
                poolDict.Add(poolTag, queue);
            }

            if (!pooledObjs.ContainsKey(poolTag))
            {
                PooledObject poolObj = new PooledObject(obj, canExpandPool, parent);
                pooledObjs.Add(poolTag, poolObj);
            }
        }

        private Queue<GameObject> SpawnObjects(Queue<GameObject> queue, GameObject objToSpawn, int amount, GameObject parent)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject newObj = SpawnAnObject(objToSpawn, parent);
                queue.Enqueue(newObj);
            }
            return queue;
        }

        private GameObject SpawnAnObject(GameObject objToSpawn, GameObject parent)
        {
            GameObject obj;
            if (parent != null)
                obj = Instantiate(objToSpawn, new Vector3(1000, 1000, 1000), Quaternion.identity, parent.transform);
            else
                obj = Instantiate(objToSpawn, new Vector3(1000, 1000, 1000), Quaternion.identity);
            obj.SetActive(false);
            return obj;
        }
        #endregion

        #region Return To Pool
        public void ReturnObjToPool(string poolTag, GameObject obj)
        {
            Queue<GameObject> queue;
            if (poolDict.TryGetValue(poolTag, out queue))
            {
                if (obj != null)
                {
                    queue.Enqueue(obj);
                    obj.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("Tag \"" + poolTag + "\" is not in pool. Can't return \"" + obj.name + "\" to pool.");
            }
        }

        public void ReturnObjToPool(string poolTag, GameObject obj, float timer)
        {
            StartCoroutine(ReturnAfterTimer(poolTag, obj, timer));
        }

        IEnumerator ReturnAfterTimer(string poolTag, GameObject obj, float timer)
        {
            yield return new WaitForSeconds(timer);
            ReturnObjToPool(poolTag, obj);
            yield break;
        }
        #endregion

        #region Get Object & Expand
        public GameObject GetPooledObject(string poolTag)
        {
            Queue<GameObject> queue;
            if (poolDict.TryGetValue(poolTag, out queue))
            {
                bool canExpand = CheckTagCanExpand(poolTag);

                if (queue.Count != 0)
                {
                    GameObject obj = poolDict[poolTag].Dequeue();
                    obj.SetActive(true);
                    return obj;
                }
                else if (canExpand)
                {
                    GameObject expandObj = ExpandPool(poolTag, pooledObjs[poolTag].parent);
                    expandObj.SetActive(true);
                    return expandObj;
                }
                else
                    return null;
            }
            else
                return null;
        }

        private GameObject ExpandPool(string poolTag, GameObject parent)
        {
            PooledObject pooledObj;
            if (pooledObjs.TryGetValue(poolTag, out pooledObj))
            {
                AddObjectToPool(poolTag, pooledObj.obj, 1, pooledObj.canExpandPool, parent);
                if (poolDict[poolTag].Count > 0)
                    return poolDict[poolTag].Dequeue();
                else
                    return null;
            }
            else
                return null;
        }
        #endregion

        #region Checking Functions
        public bool CheckIfTagInPool(string poolTag)
        {
            if (poolDict.ContainsKey(poolTag))
                return true;
            else
                return false;
        }

        public bool CheckTagCanExpand(string poolTag)
        {
            PooledObject poolObj;
            if (pooledObjs.TryGetValue(poolTag, out poolObj))
                return poolObj.canExpandPool;
            else
                return false;
        }
        #endregion

        #region OnSceneUnLoaded Event
        private void OnSceneUnloaded(Scene current)
        {
            Dictionary<string, Queue<GameObject>> newPool = new Dictionary<string, Queue<GameObject>>();
            foreach (KeyValuePair<string, Queue<GameObject>> pool in poolDict)
            {
                if (pool.Value.Count > 0)
                {
                    Queue<GameObject> tempQ = new Queue<GameObject>();
                    // Remove null objects from queues
                    foreach (GameObject obj in pool.Value)
                    {
                        if (obj != null)
                        {
                            tempQ.Enqueue(obj);
                            obj.SetActive(false);
                        }
                    }

                    // If new queue is empty remove from dict
                    if (tempQ.Count != 0)
                        newPool.Add(pool.Key, tempQ);
                    else
                    {
                        if (pooledObjs.ContainsKey(pool.Key))
                            pooledObjs.Remove(pool.Key);
                    }
                }
                else
                {
                    if (pooledObjs.ContainsKey(pool.Key))
                        pooledObjs.Remove(pool.Key);
                }
            }
            poolDict = newPool;
        }
        #endregion
    }
}