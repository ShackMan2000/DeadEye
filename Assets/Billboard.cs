using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        // Find the main camera in the scene
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        // Calculate the direction from the transform to the camera
        Vector3 directionToCamera = cameraTransform.position - transform.position;

        // Align the local Z-axis of the transform with the direction to the camera
        transform.rotation = Quaternion.LookRotation(-directionToCamera, transform.forward);
    }
}
