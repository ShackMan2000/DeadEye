using UnityEngine;
using UnityEngine.Events;
using BNG;

public class IsThisShellRedOrBlue : MonoBehaviour
{
    public UnityEvent onBlueCollision;  // Event to be fired when colliding with an object tagged as "BLUE"
    public UnityEvent onRedCollision;   // Event to be fired when colliding with an object tagged as "RED"

    public Material RedMat;

    public Material BlueMat;


    public GameObject[] ChangeMat;

    public RaycastWeapon shotgun;

    public GameObject BlueProjectile;

    public GameObject RedProjectile;


    private string lastCollidedTag;  // Tag of the last collided object



    private void OnCollisionEnter(Collision collision)
    {
        lastCollidedTag = collision.gameObject.tag;

        if (lastCollidedTag == "BLUE")
        {
            onBlueCollision.Invoke();
            shotgun.ProjectilePrefab = BlueProjectile;
            foreach (GameObject obj in ChangeMat)
                        {
                            Renderer renderer = obj.GetComponent<Renderer>();
                            
                            if (renderer != null)
                            {
                                renderer.material = BlueMat;
                            }
                        }
        }
        else if (lastCollidedTag == "RED")
        {
            onRedCollision.Invoke();
            shotgun.ProjectilePrefab = RedProjectile;
               foreach (GameObject obj in ChangeMat)
                        {
                            Renderer renderer = obj.GetComponent<Renderer>();
                            
                            if (renderer != null)
                            {
                                renderer.material = RedMat;
                            }
                        }
        }
    }
}