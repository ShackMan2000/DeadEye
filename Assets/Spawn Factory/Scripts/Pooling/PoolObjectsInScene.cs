using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityExtensions;
#endif

namespace SpawnFactory.Pooling
{
    public class PoolObjectsInScene : MonoBehaviour
    {
#if UNITY_EDITOR && !UNITY_2020_2_OR_NEWER
        [ReorderableList(elementsAreSubassets = true)]
#endif
        [SerializeField] private List<PoolingManager.ObjectToPool> pooledObjects;

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (PoolingManager.ObjectToPool poolObj in pooledObjects)
            {
                if (poolObj.pooledObject != null && poolObj.poolTag != "" && poolObj.amountToPool != 0)
                    PoolingManager.instance.AddObjectToPool(poolObj.poolTag, poolObj.pooledObject, poolObj.amountToPool, poolObj.canExpandPool, gameObject);
            }
        }
    }
}