using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class OptionSelector : MonoBehaviour
{
    [FormerlySerializedAs("playerHealth")] [SerializeField] SelectableOption selectableOption;

    [SerializeField] List<SelectOptionButton> buttons;

    [SerializeField] List<int> optionsThatNeedWarning = new List<int>();

    [SerializeField] Color selectedColor;
    [SerializeField] Color notSelectedColor;

    [SerializeField] GameObject warningPanel;


    // need to inject each option into a button
    // set the colors to selected or not selected


    void OnEnable()
    {
        InjectIndexesAndAddListener();
        ToggleWarning();
    }

    void OnDisable()
    {
        foreach (var button in buttons)
        {
            if (button.gameObject.activeSelf)
            {
                button.OnHealthOptionSelected -= OnHealthOptionSelected;
            }
        }
    }

    void InjectIndexesAndAddListener()
    {
        // don't have the case yet with not enough buttons..
        for (int i = 0; i < buttons.Count; i++)
        {
            if (selectableOption.Options.Count > i)
            {
                buttons[i].gameObject.SetActive(true);
                 buttons[i].InjectOptionIndex(i);
                 buttons[i].OnHealthOptionSelected += OnHealthOptionSelected;

                if (i == selectableOption.SelectedIndex)
                {
                    buttons[i].SetColor(selectedColor);
                }
                else
                {
                    buttons[i].SetColor(notSelectedColor);
                }
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnHealthOptionSelected(SelectOptionButton button)
    {
        selectableOption.SelectedIndex = button.Index;

        foreach (var btn in buttons)
        {
            if (btn == button)
            {
                btn.SetColor(selectedColor);
            }
            else
            {
                btn.SetColor(notSelectedColor);
            }
        }
        
        ToggleWarning();
    }


    [Button]
    void SetAllButtonsToNotSelected()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i == selectableOption.SelectedIndex)
            {
                buttons[i].SetColor(selectedColor);
            }
            else
            {
                buttons[i].SetColor(notSelectedColor);
            }
        }
    }
    
    
    
    
    
    
    void ToggleWarning()
    {
        if(optionsThatNeedWarning == null || optionsThatNeedWarning.Count == 0)
        {
            return;
        }
        
        if (optionsThatNeedWarning.Contains(selectableOption.SelectedIndex))
        {
            warningPanel.SetActive(true);
        }
        else
        {
            warningPanel.SetActive(false);
        }

    }
    
    
    
    
    
    [Button]
     void SetUpButtonTexts()
     {
         for (int i = 0; i < buttons.Count; i++)
         {
             if (selectableOption.Options.Count > i)
             {
                 buttons[i].gameObject.SetActive(true);
               
                 buttons[i].SetButtonText(selectableOption.Options[i]);

                 if (i == selectableOption.SelectedIndex)
                 {
                     buttons[i].SetColor(selectedColor);
                 }
                 else
                 {
                     buttons[i].SetColor(notSelectedColor);
                 }
             }
             else
             {
                 buttons[i].gameObject.SetActive(false);
             }
         }
         
     }


    // might be safest to just select one in on enable and set the max health right away.
    // better to reset health in the game controllers, might start new games there.
    // or in the game manager
    // so this one is purely setting the max health
}