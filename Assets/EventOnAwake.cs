using UnityEngine;
using UnityEngine.Events;

public class AwakeEvent : MonoBehaviour
{

    public UnityEvent AwakeEventing;

    private void Awake()
    {
        // Check if there are subscribers to the event before firing it
        AwakeEventing.Invoke();
    }
}