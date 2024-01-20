using System;
using System.Collections;
using System.Collections.Generic;
using Backend;
using FluffyUnderware.Curvy;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;


public class Tutorial : MonoBehaviour
{
    [SerializeField] List<TutorialStep> steps;

    [SerializeField] TextMeshProUGUI instructionText;


    // save that tutorial was done
    // when game starts, play tutorial. otherwise show menu

    // need to show text and wait for event.

    [SerializeField] EnemySettings redEnemy;
    [SerializeField] EnemySettings blueEnemy;

    [SerializeField] MultiDrone multiDrone;

    [SerializeField] CurvySpline tutorialSpline;

    [System.Serializable]
    public class TutorialStep
    {
        public string instruction;
        public EnemyBase enemy;
    }


    int currentStepIndex = 0;


    // text: Shoot blue enemies with the blue gun
    // spawn a blue enemy and wait until it is shot.
    // if shot with the wrong weapon, spawn again
    // if shot with correct weapon, move on


    // text shhot red enemies with the red gun
    // spawn red enemy and wait


    [SerializeField] UIController uiController;

    void OnEnable()
    {
        EnemyBase.OnAnySingleEnemyDestroyedCorrectly += OnEnemyDestroyedCorrectly;
        MultiDrone.OnMultiDroneDestroyed += OnMultiDroneDestroyed;
    }

    void OnDisable()
    {
        EnemyBase.OnAnySingleEnemyDestroyedCorrectly -= OnEnemyDestroyedCorrectly;
        MultiDrone.OnMultiDroneDestroyed -= OnMultiDroneDestroyed;
    }

    void OnEnemyDestroyedCorrectly(EnemySettings enemySettings, bool destroyedCorrectly, Vector3 position)
    {
        if (destroyedCorrectly)
        {
            currentStepIndex++;
            if (currentStepIndex == 1)
            {
                instructionText.text = steps[currentStepIndex].instruction;
                EnemyBase enemy = steps[currentStepIndex].enemy;
                enemy.gameObject.SetActive(true);
                enemy.Initialize(blueEnemy, tutorialSpline, false);
            }
            else if (currentStepIndex == 2)
            {
                instructionText.text = steps[currentStepIndex].instruction;
                MultiDrone newDrone = Instantiate(multiDrone, multiDrone.transform.position, multiDrone.transform.rotation);
                newDrone.gameObject.SetActive(true);
            }
            else
            {
                FinishTutorial();
            }
        }
        else
        {
            if (currentStepIndex == 0)
            {
                EnemyBase enemy = steps[currentStepIndex].enemy;
                StartCoroutine(SpawnEnemyWithDelay(enemy, redEnemy));
            }
            else if (currentStepIndex == 1)
            {
                EnemyBase enemy = steps[currentStepIndex].enemy;
                StartCoroutine(SpawnEnemyWithDelay(enemy, blueEnemy));
            }
        }
        // if correctly, move to next step.
    }


    void OnMultiDroneDestroyed(MultiDroneHitInfo multiDroneHitInfo)
    {
        FinishTutorial();
    }


    [Button]
    public void StartTutorial()
    {
        GameManager.EnterShootingMode();

        currentStepIndex = 0;

        instructionText.text = steps[currentStepIndex].instruction;
        EnemyBase enemy = steps[currentStepIndex].enemy;
        enemy.gameObject.SetActive(true);
        enemy.Initialize(redEnemy, tutorialSpline, false);
    }


    IEnumerator SpawnEnemyWithDelay(EnemyBase enemy, EnemySettings settings)
    {
        yield return new WaitForSeconds(1f);
        enemy.gameObject.SetActive(true);
        enemy.Initialize(settings, tutorialSpline, false);
    }

    public void FinishTutorial()
    {
        GameManager.ExitShootingGameMode();
        SaveManager.Instance.GetSaveData().TutorialCompleted = true;
        SaveManager.Instance.WriteSaveData();

        uiController.FinishedTutorial();
    }
}