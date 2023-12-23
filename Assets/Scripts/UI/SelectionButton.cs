using System;
using System.Collections.Generic;

using UnityEngine;


public class SelectionButton : MonoBehaviour
{
    public static event Action<SelectionButton> OnSelectionButtonClicked = delegate { };

    [SerializeField] GameObject gameObjectToActivate;

  

    void OnEnable()
    {
        OnSelectionButtonClicked += DeactivateGameObject;
    }

    void OnDisable()
    {
        OnSelectionButtonClicked -= DeactivateGameObject;
    }

    public void OnClick()
    {
        OnSelectionButtonClicked?.Invoke(this);
        gameObjectToActivate.SetActive(true);
    }


    void DeactivateGameObject(SelectionButton selectionButton)
    {
        if (selectionButton != this)
        {
            gameObjectToActivate.SetActive(false);
        }
    }
}