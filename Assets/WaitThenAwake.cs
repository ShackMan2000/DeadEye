
using UnityEngine;
using System.Collections;

public class WaitThenAwake : MonoBehaviour
{
    public GameObject objectToAwaken;
    public float delayInSeconds = 2.0f;

    private bool hasAwakened = false;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delayInSeconds);
        AwakeObject();
    }

    private void AwakeObject()
    {
        if (!hasAwakened && objectToAwaken != null)
        {
            objectToAwaken.SetActive(true);
            Debug.Log(objectToAwaken.name + " has been awakened!");
            hasAwakened = true;
        }
    }
}