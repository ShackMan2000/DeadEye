using UnityEngine;
using UnityEngine.Events;

public class DepthRotateFinal : MonoBehaviour
{
    public Collider droneCollider1;
    public Collider droneCollider2;

    public UnityEvent onBothInsideCollider;
    public UnityEvent onEitherOutsideCollider;

    private bool insideCollider1;
    private bool insideCollider2;

    private void OnTriggerEnter(Collider other)
    {
        if (other == droneCollider1)
        {
            insideCollider1 = true;
            CheckAndFireEvents();
        }
        else if (other == droneCollider2)
        {
            insideCollider2 = true;
            CheckAndFireEvents();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == droneCollider1)
        {
            insideCollider1 = false;
            CheckAndFireEvents();
        }
        else if (other == droneCollider2)
        {
            insideCollider2 = false;
            CheckAndFireEvents();
        }
    }

    private void CheckAndFireEvents()
    {
        if (insideCollider1 && insideCollider2)
        {
            // Both colliders are inside the attached collider
            onBothInsideCollider.Invoke();
        }
        else if (!insideCollider1 || !insideCollider2)
        {
            // Either one or both colliders are outside the attached collider
            onEitherOutsideCollider.Invoke();
        }
    }
}
