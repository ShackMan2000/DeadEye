using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthDisplay : MonoBehaviour
{
    [SerializeField] List<Image> healthIcons;
    
    [SerializeField] HealthController healthController;
    
    void OnEnable()
    {
        healthController.OnHealthChanged += UpdateHealthDisplay;
        
            foreach (var icon in healthIcons)
            {
                icon.color = healthController.UnlimitedHealth? Color.magenta : Color.white;
            }
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
                healthIcons[i].gameObject.SetActive(true);
            }
            else
            {
                healthIcons[i].gameObject.SetActive(false);
            }
        }
    }
    
    
    

}