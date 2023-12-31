using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthDisplay : MonoBehaviour
{
    [SerializeField] List<Image> healthIcons;

    [SerializeField] Color fullColor;
    [SerializeField] Color emptyColor;

    [SerializeField] PlayerHealth playerHealth;

    [SerializeField] GameObject container;

    void OnEnable()
    {
        SetUpHearts();

        playerHealth.OnHealthReduced += UpdatePlayerHealthDisplay;
    }

    void OnDisable()
    {
        playerHealth.OnHealthReduced -= UpdatePlayerHealthDisplay;
    }


    void SetUpHearts()
    {
        if (healthIcons.Count == 0)
        {
            Debug.LogError("No health icons set in the inspector");
            return;
        }

        if (playerHealth.MaxHealth == 0)
        {
            container.SetActive(false);

            return;
        }

        container.SetActive(true);

        // go through health and make sure that there are enough heart icons, if not, instantiate and add to the list.
        for (int i = 0; i < playerHealth.MaxHealth; i++)
        {
            if (i < healthIcons.Count)
            {
                healthIcons[i].gameObject.SetActive(true);
            }
            else
            {
                Image newIcon = Instantiate(healthIcons[0], healthIcons[0].transform.parent);
                healthIcons.Add(newIcon);
            }
        }

        // go through health icons and make sure that there are not too many, if so, deactivate them.
        for (int i = 0; i < healthIcons.Count; i++)
        {
            if (i < playerHealth.MaxHealth)
            {
                healthIcons[i].gameObject.SetActive(true);
            }
            else
            {
                healthIcons[i].gameObject.SetActive(false);
            }
        }

        // set color of all to full.
        foreach (var icon in healthIcons)
        {
            icon.color = fullColor;
        }
    }


    void UpdatePlayerHealthDisplay()
    {
        if (playerHealth.MaxHealth == 0)
        {
            return;
        }
        
        for (int i = 0; i < healthIcons.Count; i++)
        {
            if (i < playerHealth.CurrentHealth)
            {
                healthIcons[i].color = fullColor;
            }
            else
            {
                healthIcons[i].color = emptyColor;
            }
        }
    }
}