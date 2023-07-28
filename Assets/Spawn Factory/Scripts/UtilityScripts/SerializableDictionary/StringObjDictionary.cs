using SpawnFactory.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnFactory.UnityExtensions
{
    [System.Serializable]
    public class StringObjDictionary : SerializableDictionary<string, PoolingManager.PoolGroup> { }
}