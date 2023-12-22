using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class CheckList : MonoBehaviour
{

    public List<GameObject> MustBeDisabled;
    
    public List<GameObject> MustBeEnabled;

    [SerializeField] WaveController waveController;
    
    [SerializeField] WaveSettings testWaveSettings;
    [SerializeField] WaveSettings notTestWaveSettings;
    
    [Button]
    public void Check()
    {
        foreach (var go in MustBeDisabled)
        {
            if (go.activeSelf)
            {
                Debug.LogError($"{go.name} must be disabled");
            }
        }
        
        foreach (var go in MustBeEnabled)
        {
            if (!go.activeSelf)
            {
                Debug.LogError($"{go.name} must be enabled");
            }
        }
        
        if (waveController.settings == null || waveController.settings == testWaveSettings)
        {
            Debug.LogError($"WaveController settings is null or test settings");
        }
    }


    [Button]
    void Fix()
    {
        foreach (var go in MustBeDisabled)
        {
            go.SetActive(false);
        }
        
        foreach (var go in MustBeEnabled)
        {
            go.SetActive(true);
        }
        
        waveController.settings = notTestWaveSettings;
    }
    
}