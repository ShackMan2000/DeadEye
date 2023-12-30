using UnityEngine;


public class Player : MonoBehaviour
{

    [SerializeField] PlayerPosition playerPosition;
    [SerializeField] Transform playerTransform;
    
    
    void Update()
    {
        
        if (playerTransform != null)
        {
            playerPosition.Position = playerTransform.position;
        }
    }

}