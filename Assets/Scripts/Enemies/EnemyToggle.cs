using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EnemyToggle : MonoBehaviour
{
    public EnemySettings EnemySettings;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;

    [SerializeField] Sprite toggleOnSprite;
    [SerializeField] Sprite toggleOffSprite;

    [SerializeField] Image buttonImage;
    [SerializeField] Image iconImage;

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float unselectedAlpha = 0.8f;

    EnemySelector enemySelector;


    bool isEnemySelected;

    public event Action<bool> OnEnemySelected = delegate { };


    public void InjectEnemySettings(EnemySettings settings, EnemySelector selector)
    {
        EnemySettings = settings;
        enemySelector = selector;

        nameText.text = EnemySettings.Name;
        descriptionText.text = EnemySettings.Description;
        descriptionText.text += "\n" + EnemySettings.pointsForKill.ToString("F0") + " points for correct kill";
        
        if (EnemySettings.CanShoot)
        {
            descriptionText.text += "\nShoots at you";
        }
        iconImage.sprite = EnemySettings.Icon;
    }


    public void UpdateSelected(bool isSelected)
    {
        isEnemySelected = isSelected;
        buttonImage.sprite = isEnemySelected ? toggleOnSprite : toggleOffSprite;
        canvasGroup.alpha = isEnemySelected ? 1f : unselectedAlpha;

        // don't call event, will create endless loop!
    }


    public void ClickToggle()
    {
        if (enemySelector.SelectedEnemies.Count == 1)
        {
            Debug.Log("Can't deselect last enemy");
            Debug.Log("its this one though, that's good " + (enemySelector.SelectedEnemies[0] == EnemySettings));
            return;
        }

        isEnemySelected = !isEnemySelected;
        UpdateSelected(isEnemySelected);

        enemySelector.ToggleSelected(EnemySettings, isEnemySelected);
    }
}