using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class PlayerGetsHitEffects : MonoBehaviour
{
    
    [SerializeField] PlayerHealth playerHealth;
    
    [SerializeField] Renderer curtainRenderer;
    [SerializeField] float redCurtainDuration = 0.3f;
    
    [SerializeField] AudioSource audioSource;
    
    
    void OnEnable()
    {
        playerHealth.OnPlayerHitByBullet += ShowEffects;
    }
    
    void OnDisable()
    {
        playerHealth.OnPlayerHitByBullet -= ShowEffects;
    }
    
    
    [Button]
    void ShowEffects()
    {
        audioSource.Play();
        StopAllCoroutines();
        StartCoroutine(ShowRedCurtainCoroutine());
    }


        
    // coroutine that enables curtain, sets alpha to full and decreases it over time
    
    IEnumerator ShowRedCurtainCoroutine()
    {
        curtainRenderer.gameObject.SetActive(true);
        // set alpha to full
        curtainRenderer.material.color = new Color(1f, 0f, 0f, 1f);
        float timeLeft = redCurtainDuration;
        
        
        while (timeLeft > 0f)
        {
            float alpha = timeLeft / redCurtainDuration;
            curtainRenderer.material.color = new Color(1f, 0f, 0f, alpha);
            
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        
        curtainRenderer.gameObject.SetActive(false);
        
    }
    
    
}