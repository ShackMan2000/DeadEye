using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SelectOptionButton : MonoBehaviour
{
    [SerializeField] Image buttonImage;

    [SerializeField] TextMeshProUGUI buttonText;

    [ReadOnly]public int Index;

    public event Action<SelectOptionButton> OnHealthOptionSelected = delegate { };


    public void InjectOptionIndex(int selectedIndex)
    {
        Index = selectedIndex;
    }

    
    public void SelectOption()
    {
        OnHealthOptionSelected(this);
    }

    public void SetColor(Color selectedColor)
    {
        buttonImage.color = selectedColor;
    }
    
    public void SetButtonText(int MaxHealthOption)
    {
        buttonText.text = MaxHealthOption.ToString();
        
        if (MaxHealthOption == 0)
        {
            buttonText.gameObject.SetActive(false);
        }
    }
}
