using System.Collections;
using UnityEngine;
using BNG;

public class PerspectiveShield : MonoBehaviour
{
    public Vector3 downScale = new Vector3(0.5f, 0.5f, 0.5f);
    public float lerpSpeed = 1.0f;
    public float delaySeconds;

    private Vector3 originalScale;
    private Vector3 targetScale;
    public bool isScaling = false;
    public Collider colliderGuy;

    private void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    private void Update()
    {
        if (isScaling)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, lerpSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.localScale, targetScale) < 0.05f)
            {
                isScaling = false;
            }
        }
    }

    public void ShieldDown()
    {
        if (!isScaling)
        { 
            targetScale = downScale;
            isScaling = true;
            colliderGuy.enabled = false;
        
            StartCoroutine(ShieldDownCoroutine(delaySeconds));
        }
    }

  
    

    private IEnumerator ShieldDownCoroutine(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        colliderGuy.enabled = true;

        targetScale = originalScale;
        isScaling = true;
    }
}
