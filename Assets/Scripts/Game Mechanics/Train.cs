using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;


public class Train : MonoBehaviour
{
    [SerializeField] DOTweenPath trainPath;

    [SerializeField] Transform trainTransform;

    //[SerializeField] GameObject gateEffects;

    [SerializeField] bool quickSpawnTrain;

    public float MovementDuration => trainPath.duration;

  

    [Button]
    public void SpawnTrain()
    {
        if (quickSpawnTrain)
        {
            Tween tween = trainPath.GetTween();
            tween.timeScale = 100;
            Debug.Log("Quick spawn train, duration: " + trainPath.duration);
            trainPath.DOPlay();
        }
        else
        {
            trainTransform.gameObject.SetActive(true);
            //gateEffects.SetActive(true);
            trainPath.DOPlay();
            
        }
        
        Debug.Log("Quick spawn train, duration: " + trainPath.duration);
    }
    


    [Button]
    void ResetTrain()
    {
        trainPath.DORewind();
    }
    
    [Button]
    void SetToFinalPosition()
    {
        trainPath.DOComplete();
    }
}