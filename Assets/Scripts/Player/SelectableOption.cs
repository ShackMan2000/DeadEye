using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[InlineEditor]
[CreateAssetMenu]
public class SelectableOption : ScriptableObject
{
    [FormerlySerializedAs("MaxHealthOptions")] public List<int> Options;

    [FormerlySerializedAs("SelectedMaxHealthIndex")] public int SelectedIndex;

    public int SelectedValue
    {
        get
        {
            if (SelectedIndex >= Options.Count)
            {
                Debug.LogError("SelectedIndex is out of range");
                return 0;
            }

            return Options[SelectedIndex];
        }
    }

}