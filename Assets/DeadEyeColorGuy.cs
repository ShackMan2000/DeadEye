using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class DeadEyeColorGuy : MonoBehaviour
{   
    [SerializeField]
    public bool thisIsBlue = true;
    public Material blueMaterial;
    public Material redMaterial;

   /*  private void OnEnable()
    {
        EditorApplication.update += UpdateTagAndMaterial;
    }

    private void OnDisable()
    {
        EditorApplication.update -= UpdateTagAndMaterial;
    } */


    public void DeadEyeColorCheck(bool isBlue)
    {
        thisIsBlue = isBlue;
        UpdateTagAndMaterial();
    }
    private void UpdateTagAndMaterial()
    {
        if (thisIsBlue)
        {
            gameObject.tag = "BLUE";
            ApplyMaterial(blueMaterial);
        }
        else
        {
            gameObject.tag = "RED";
            ApplyMaterial(redMaterial);
        }
    }

    private void ApplyMaterial(Material material)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.sharedMaterial = material;
        }
        else
        {
            Debug.LogWarning("Object does not have a Renderer component.");
        }
    }
}