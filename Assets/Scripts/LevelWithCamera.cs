using UnityEngine;

public class LevelWithCamera : MonoBehaviour
{
    public Transform targetObject; // The object to keep level with the camera

    private void LateUpdate()
    {
        if (targetObject != null)
        {
            Vector3 targetPosition = targetObject.position;
            targetPosition.y = Camera.main.transform.position.y;
            transform.position = targetPosition;
        }
    }
}