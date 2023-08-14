using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSwapper : MonoBehaviour
{
    public GameObject[] ArrowBody;
    public Material RedMaterial;

    public Material BlueMaterial;
    
    // Start is called before the first frame update
    public void BLUE()
    {
          foreach (GameObject obj in ArrowBody)
                        {
                            Renderer renderer = obj.GetComponent<Renderer>();
                            
                            if (renderer != null)
                            {
                                renderer.material = BlueMaterial;
                            }
                        }
                    this.gameObject.tag = "BLUE";
    }

    // Update is called once per frame
    public void RED()
    {
          foreach (GameObject obj in ArrowBody)
                        {
                            Renderer renderer = obj.GetComponent<Renderer>();
                            
                            if (renderer != null)
                            {
                                renderer.material = RedMaterial;
                            }
                        }
                    this.gameObject.tag = "RED";
    }
}
