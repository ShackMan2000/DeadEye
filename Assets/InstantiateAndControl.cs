using System.Collections;
using UnityEngine;

public class InstantiateAndControl : MonoBehaviour
{
    public GameObject objectToInstantiate;
    public float delayInSeconds = 2.0f;

    private GameObject instantiatedObject;

    private void Awake()
    {
        // Wait for the specified delay and then instantiate the object
        StartCoroutine(InstantiateWithDelay());
    }

    public IEnumerator InstantiateWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        instantiatedObject = Instantiate(objectToInstantiate, transform.position, transform.rotation);
    }

    public void DeleteInstantiatedObject()
    {
        if (instantiatedObject != null)
        {
            Destroy(instantiatedObject);
        }
    }

    public void DeactivateAndDelete()
    {
        DeleteInstantiatedObject();
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        StartCoroutine(InstantiateWithDelay());
    }
}