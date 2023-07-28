using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGunAndShield : MonoBehaviour
{   

    public GameObject head; 
    public bool rightController;
    public bool leftController;
    private float switchingRate = 1f;
    private float nextSwitch = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfSwitch();
    }

    private void CheckIfSwitch()
    {
        Vector3 controllerPos;
    
        if (rightController == true)
    
        {
    
            controllerPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
    
        } else 

        {

            controllerPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        }

        if (Vector3.Angle(transform.up,Vector3.up)> 80 && controllerPos.y > head.transform.position.y - 0.4f && controllerPos.x < head.transform.position.x && Time.time > nextSwitch)
        {
            nextSwitch = Time.time + switchingRate;
            var rayGun = this.gameObject.transform.GetChild(0);
            var shield = this.gameObject.transform.GetChild(1);
            if(rayGun.gameObject.activeSelf == true)
            {
                rayGun.gameObject.SetActive(false);
                shield.gameObject.SetActive(true);
            }else if (rayGun.gameObject.activeSelf == false)
            {
                rayGun.gameObject.SetActive(true);
                shield.gameObject.SetActive(false);
            }

        }
    }
}
