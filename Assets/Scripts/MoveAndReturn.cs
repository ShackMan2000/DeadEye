using UnityEngine;

public class MoveAndReturn : MonoBehaviour
{
    public Transform targetLocation; // The destination location to move to
    public float delayBeforeMove = 2f; // Time to wait before moving to the target location
    public float delayBeforeReturn = 2f; // Time to wait before returning to the original position
    public float movementSpeed = 5f; // Speed of the lerp movement

    private Vector3 originalPosition; // The original position of the GameObject
    private Vector3 targetPosition; // The position of the target location

    private void Start()
    {
        originalPosition = transform.position;
        targetPosition = Camera.main.transform.position;
        StartCoroutine(MoveRoutine());
    }

    private System.Collections.IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayBeforeMove);

            float startTime = Time.time;
            float journeyLength = Vector3.Distance(originalPosition, targetPosition);

            while (Time.time - startTime < delayBeforeReturn)
            {
                float normalizedTime = (Time.time - startTime) / delayBeforeReturn;
                float easedTime = EaseInOut(normalizedTime);
                transform.position = Vector3.Lerp(originalPosition, targetPosition, easedTime);
                yield return null;
            }

            // Ensure the object reaches the exact target position
            transform.position = targetPosition;

            yield return new WaitForSeconds(delayBeforeReturn);

            startTime = Time.time;
            journeyLength = Vector3.Distance(targetPosition, originalPosition);

            while (Time.time - startTime < delayBeforeReturn)
            {
                float normalizedTime = (Time.time - startTime) / delayBeforeReturn;
                float easedTime = EaseInOut(normalizedTime);
                transform.position = Vector3.Lerp(targetPosition, originalPosition, easedTime);
                yield return null;
            }

            // Ensure the object reaches the exact original position
            transform.position = originalPosition;
        }
    }

    // Easing function for smooth acceleration and deceleration
    private float EaseInOut(float t)
    {
        return t * t * (3f - 2f * t);
    }
}
