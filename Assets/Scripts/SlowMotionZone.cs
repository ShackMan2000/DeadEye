using System.Collections;
using UnityEngine;

public class SlowMotionZone : MonoBehaviour
{
    public float delaySeconds = 0.2f;
   // public TimeController timeController;

    public MeshRenderer myMesh;

    public float originalFixedDelta;

    private IEnumerator resumeRoutine;
    
    public bool guyInsideMe;

    private void Start(){
        originalFixedDelta = Time.fixedDeltaTime;
    }
    private void Update() {
        if (guyInsideMe){
        myMesh.enabled = true;
    }
        else{
            myMesh.enabled = false;
         //   timeController.ResumeTime();
        }
        
    }

    public void OnTriggerEnter(Collider other) {
   
    
        if (other.CompareTag("EnemyLaser"))
        {   
            myMesh.enabled = true;
            //myMesh.enabled = true;
            //timeController.SlowTime();
            guyInsideMe = true;
            /* resumeRoutine = ResumeTimeTime();
           StartCoroutine(resumeRoutine);
             */
        }

    }   
    /*  public IEnumerator ResumeTimeTime() {
        myMesh.enabled = true;
        if (!guyInsideMe){
         

            yield return new WaitForSeconds(1f);

            Time.timeScale = 1;
            Time.fixedDeltaTime = originalFixedDelta;
            myMesh.enabled = false;
       
     } */

     
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("EnemyLaser"))
        {    myMesh.enabled = true;
             guyInsideMe = true;

         }
         else {guyInsideMe = false;}
    }
   /* public void RestartRoutine()
    {
        StartCoroutine(ResumeTimeTime());
    } */

}

    
    
  

