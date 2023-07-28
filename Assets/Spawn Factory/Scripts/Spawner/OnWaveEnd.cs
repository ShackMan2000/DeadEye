using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpawnFactory.Events
{
    [System.Serializable]
    public class OnWaveEnd : UnityEvent<int> { }
}