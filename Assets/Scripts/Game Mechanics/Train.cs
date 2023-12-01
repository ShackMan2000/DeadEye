using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;


public class Train : MonoBehaviour
{

    [SerializeField] DOTweenPath trainPath;
    
    [SerializeField] Transform trainTransform;

    [SerializeField] GameObject gateEffects;
    
    Vector3 _startPosition;
    Quaternion _startRotation;

    
    public float MovementDuration => trainPath.duration;

    void Awake()
    {
        _startPosition = trainTransform.position;
        _startRotation = trainTransform.rotation;
    }

    [Button]
    public void SpawnTrain()
    {
        trainTransform.gameObject.SetActive(true);
        gateEffects.SetActive(true);
        trainPath.DOPlay();
    }

    
    [Button]
    void ResetTrain()
    {
        trainPath.DORewind();
    }

}