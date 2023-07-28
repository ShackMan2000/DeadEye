using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnFactory.UnityExtensions
{
    [System.Serializable]
    public class IntObjDictionary : SerializableDictionary<int, EnemySpawner.Wave> { }
}