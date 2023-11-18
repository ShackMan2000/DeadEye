using System.Collections;
using UnityEngine;

public class InstantiateAndControl : MonoBehaviour
{
    public GameObject objectToInstantiate;
    public float delayInSeconds = 2.0f;
    [SerializeField]
    private GameObject instantiatedObject;
    [SerializeField]
    private GameObject Kiki;

    private void Awake()
    {
        // Wait for the specified delay and then instantiate the object
        //StartCoroutine(InstantiateWithDelay());
       // instantiatedObject = Instantiate(objectToInstantiate, transform.position, transform.rotation);
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
            Kiki = GameObject.FindGameObjectWithTag("EnemySpawner");
            if (Kiki != null)
            {
                Destroy(Kiki);
            }

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