using UnityEngine;

public class DisableOnAwake : MonoBehaviour
{
    public GameObject baby;
    // Start is called before the first frame update
    void Start()
    {
        baby.SetActive(false);
    }

    
}
