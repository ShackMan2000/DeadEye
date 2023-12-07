using UnityEngine;

public class BottleBreak : MonoBehaviour
{   
    public AudioSource sound;
    public AudioClip glass;
    // Start is called before the first frame update

    private void Start() 
    { 
    sound = gameObject.GetComponent<AudioSource>();
    }
    void BreakMe()
    {
        sound.PlayOneShot(glass);
    }


}
