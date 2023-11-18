using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventCycle : MonoBehaviour
{
    public UnityEvent[] events;
    public float delayBetweenEvents = 1f;
    public float delayAfterCycle = 3f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delayAfterCycle);

        while (true)
        {
            for (int i = 0; i < events.Length; i++)
            {
                events[i].Invoke();
                yield return new WaitForSeconds(delayBetweenEvents);
            }

            yield return new WaitForSeconds(delayAfterCycle);
        }
    }
}