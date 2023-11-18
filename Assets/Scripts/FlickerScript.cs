using UnityEngine;

public class FlickerScript : MonoBehaviour
{
    public float flickerRate = 0.5f; // Flicker rate in tenths of a second
    public GameObject meshRenderer;
    public bool isFlickering = false;


    private void Update()
    {
        
        if (isFlickering)
        {
            // Start the flicker coroutine
            Debug.Log ("flivker 1");
              
            StartCoroutine(Flicker(flickerRate));
        }
    }

    private System.Collections.IEnumerator Flicker(float flickerRate)
    {
       //isFlickering = true;

        // Turn off the mesh renderer
       meshRenderer.SetActive(false);
        Debug.Log("flicker) 2");
        // Wait for the flicker time
        yield return new WaitForSeconds(flickerRate);

        // Turn on the mesh renderer
        meshRenderer.SetActive(true);

        
    }
}