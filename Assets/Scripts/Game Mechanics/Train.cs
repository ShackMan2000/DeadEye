using System;
using System.Collections;
using DG.Tweening;
using FluffyUnderware.Curvy.Controllers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class Train : MonoBehaviour
{
    [SerializeField] SplineController trainController;


    bool trainIsSpawned;


    [SerializeField] WaveController waveController;

    [SerializeField] TextMeshProUGUI waveNumberText;

    [SerializeField] float moveOutTime = 2f;

    [SerializeField] float pathPositionToStopAt = 0.8f;

    [FormerlySerializedAs("trainSpeedCurve")] [SerializeField] AnimationCurve moveInSpeedCurve;

    [SerializeField] AnimationCurve moveOutSpeedCurve;

    // set to start of spline when a new wave starts.
    // set the wave number on the UI

    // get time from wave setting so it can be synced with the wave spawning
    // try out some sound, better than nothing...
    // when shooting mode ends and train is spawned, move to end of curve (2nd gate)


    void OnEnable()
    {
        GameManager.OnStartingWave += MoveTrainIntoScene;
        GameManager.OnExitShootingMode += MoveTrainOutOfScene;
    }

    void OnDisable()
    {
        GameManager.OnStartingWave -= MoveTrainIntoScene;
        GameManager.OnExitShootingMode -= MoveTrainOutOfScene;
    }


    void Start()
    {
        trainController.gameObject.SetActive(false);
    }


    [Button]
    void MoveTrainIntoScene()
    {
        StopAllCoroutines();
        StartCoroutine(MoveTrainIntoSceneRoutine());
    }



    IEnumerator MoveTrainIntoSceneRoutine()
    {
        trainController.RelativePosition = 0f;
        waveNumberText.text = "Wave " + (waveController.currentWaveIndex + 1);
        trainController.gameObject.SetActive(true);

        // need to remap 0 to 0.8 to the curves's 0 to 1
        float movePerSecond = 1f / waveController.WarmupTime;

        float progress = 0f;

        while (progress < 1f)
        {
            progress += movePerSecond * Time.deltaTime;
            float positionOnPath = moveInSpeedCurve.Evaluate(progress);
            positionOnPath = Mathf.Clamp01(positionOnPath * pathPositionToStopAt);

            trainController.RelativePosition = positionOnPath;
            yield return null;
        }
    }


    
    [Button, GUIColor("red")]
    void MoveTrainOutOfScene()
    {
        StopAllCoroutines();
        StartCoroutine(MoveTrainOutOfSceneRoutine());
    }

    IEnumerator MoveTrainOutOfSceneRoutine()
    {
        float movePerSecond = 1f / moveOutTime;

        float progress = 0f;

        while (progress < 1f)
        {
            progress += movePerSecond * Time.deltaTime;
            float progressAdjusted = Mathf.Clamp01(moveOutSpeedCurve.Evaluate(progress));

            float positionOnPath = pathPositionToStopAt + (1 - pathPositionToStopAt) * progressAdjusted;
            trainController.RelativePosition = positionOnPath;
            yield return null;
        }

        trainController.gameObject.SetActive(false);
    }


   
    [Button]
    void SetToStopPosition()
    {
        trainController.gameObject.SetActive(true);
        trainController.RelativePosition = pathPositionToStopAt;
    }
    
    [Button]
    void SetToStart()
    {
        trainController.gameObject.SetActive(true);
        trainController.RelativePosition = 0f;
    }
}