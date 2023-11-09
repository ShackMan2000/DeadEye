

using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class GrabCheckerPrime : MonoBehaviour
{
    public GameObject grabbo1;
    public GameObject grabbo2;

    public GameObject tut1;
    public GameObject tut2;
    
    public GameObject tut3;

    public bool grabbed1;
    public bool grabbed2;

    public UnityEvent TheyGrabbedIt;
    public UnityEvent TheyDontGrabbedIt;
    // Start is called before the first frame update
    void Start()
    {
       grabbed1 = false;
       grabbed2 = false;
       tut3.SetActive(false);
        grabbed1 = grabbo1.GetComponent<grabChecker>().Grabbed;
        grabbed2 = grabbo1.GetComponent<grabChecker>().Grabbed;

        if (grabbed1 && grabbed2)
        {
            tut1.SetActive(true);
            tut2.SetActive(false);
            TheyGrabbedIt.Invoke();
        }
        else {tut1.SetActive(false);}tut2.SetActive(true);TheyDontGrabbedIt.Invoke();
        
        }

    // Update is called once per frame
  

    public void Grab1()
    {
        grabbed1 = true;
    }

    public void Grab2()
    {
        grabbed2 = true;   

    }
     void Update()
        {
            if (grabbed1 && grabbed2)
        {   
             tut1.SetActive(true);
            tut2.SetActive(false);
            TheyGrabbedIt.Invoke();
             
        }
    }

}