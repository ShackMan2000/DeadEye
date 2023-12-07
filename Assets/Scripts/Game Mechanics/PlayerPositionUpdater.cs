using UnityEngine;


public class PlayerPositionUpdater : MonoBehaviour
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