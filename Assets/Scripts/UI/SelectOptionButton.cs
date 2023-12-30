using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SelectOptionButton : MonoBehaviour
{
    [SerializeField] Image buttonImage;

    [SerializeField] TextMeshProUGUI buttonText;

    public int Index;

    public event Action<SelectOptionButton> OnHealthOptionSelected = delegate { };


    public void InjectHealthOption(int selectedIndex, int MaxHealthOption)
    {
        Index = selectedIndex;
        buttonText.text = MaxHealthOption.ToString();
        if (MaxHealthOption == 0)
        {
            buttonText.gameObject.SetActive(false);
        }
    }

    
    public void SelectOption()
    {
        OnHealthOptionSelected(this);
    }

    public void SetColor(Color selectedColor)
    {
        buttonImage.color = selectedColor;
    }
}