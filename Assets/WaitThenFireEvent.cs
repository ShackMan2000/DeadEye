using UnityEngine;
using UnityEngine.Events;

public class WaitThenFireEvent : MonoBehaviour
{
    public float delayInSeconds = 2f; // Adjust this to set the delay time
    public UnityEvent delayedEvent;
    public UnityEvent instantEvent;

    private void Awake()
    {
        // Use Invoke to delay the execution of the Unity event
        instantEvent.Invoke();
        Invoke("TriggerDelayedEvent", delayInSeconds);
        
    }

    private void TriggerDelayedEvent()
    {
        // Invoke the Unity event after the specified delay
        delayedEvent.Invoke();
    }
}
