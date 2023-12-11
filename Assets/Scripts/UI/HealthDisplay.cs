using System.Collections.Generic;
using UnityEngine;


public class HealthDisplay : MonoBehaviour
{
    [SerializeField] List<GameObject> healthIcons;
    
    [SerializeField] HealthController healthController;
    
    void OnEnable()
    {
        healthController.OnHealthChanged += UpdateHealthDisplay;
    }
    
    void OnDisable()
    {
        healthController.OnHealthChanged -= UpdateHealthDisplay;
    }
    
    
    void UpdateHealthDisplay(int health)
    {
        for (int i = 0; i < healthIcons.Count; i++)
        {
            if (i < health)
            {
                healthIcons[i].SetActive(true);
            }
            else
            {
                healthIcons[i].SetActive(false);
            }
        }
    }
    

}