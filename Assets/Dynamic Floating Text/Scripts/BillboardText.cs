using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardText : MonoBehaviour
{

    // Standard Billboard script which makes canvas objects always look
    // at the camera
    
    void LateUpdate()
    {
        transform.LookAt(transform.position + DynamicTextManager.mainCamera.forward);
    }
}
