using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;


public class PopUp : MonoBehaviour
{
    // fast and simple
    // set text
    // grow and disappear...
    // problem is right now they live longer than enemy.
    // do it like with explosions, use same event

    [SerializeField] float originalScale;
    [SerializeField] Color originalColor;
    [SerializeField] float distanceForOriginalScale;
    [SerializeField] float reduceScaleFactorLargeDistance = 0.8f;

    [SerializeField] float reducedScaleOnSpawning = 0.5f;
    [SerializeField] float timeToReachFullScale = 1f;
    [SerializeField] float timeToFade = 2f;

    PopUpManager popUpManager;

    [SerializeField] TextMeshProUGUI infoText;
    // depth needs percentage when killed
    // and extra method for tutorial to update
    // probably best to have a script for that, can use pop up prefab, set position and value every frame
    // for depth drone use two! one immediatly when shot, the other one like everything else on explosion


    // grow a bit, then fade and return to pool
    // make it a parameter to differntiate the pop up for accuracy = grow super fast, stay, fade quickly. Should by synced to the drone lasers
    // 


    public void Initialize(PopUpManager pm)
    {
        popUpManager = pm;
    }


    float adjustedTargetScale;

    public void SetText(string toString, float distanceToPlayer)
    {
        float adjustTargetScale = 1f;
        
        if (distanceToPlayer > distanceForOriginalScale)
        {
            adjustTargetScale += ((distanceToPlayer / distanceForOriginalScale) -1f) * reduceScaleFactorLargeDistance;
        }
        

        adjustedTargetScale = originalScale * adjustTargetScale;

        infoText.text = toString;

        transform.localScale = Vector3.one * reducedScaleOnSpawning * adjustedTargetScale;

        gameObject.SetActive(true);
        StartCoroutine(GrowRoutine());
    }


    IEnumerator GrowRoutine()
    {
        infoText.color = originalColor;
        
        float time = 0;
        while (time < timeToReachFullScale)
        {
            time += Time.deltaTime;
            float progress = time / timeToReachFullScale;
            transform.localScale = Vector3.one * Mathf.Lerp(reducedScaleOnSpawning * adjustedTargetScale, adjustedTargetScale, progress);
            yield return null;
        }

        time = 0;
        while (time < timeToFade)
        {
            time += Time.deltaTime;
            float progress = time / timeToFade;
            infoText.color = Color.Lerp(originalColor, Color.clear, progress);
            yield return null;
        }

        popUpManager.ReturnToPool(this);
    }


    [Button]
    void SetOriginalScaleAndColor()
    {
        originalScale = transform.localScale.x;
        originalColor = infoText.color;
    }
}