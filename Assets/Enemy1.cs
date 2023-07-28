using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BNG;

public class Enemy1 : MonoBehaviour
{   
    public bool Shooty = false;
    public bool Movey = false;

    public bool Swapey =false;
    private int index = 0;
    public Transform[] LocationCords;

    public float _NormalSpeed = 10f;
    private float normalSpeed = 10f;

    public GameObject redSon;
    public GameObject blueSon;

    public ScoreManager scoreManager;
    

    //Shoot at these locations
    public int[]shootLocations;

    public GameObject Barrel;
    public GameObject blueLaser;
    public GameObject redLaser;
   
    public ProjectileLauncher launcher;


    //private float shootPower = 1000f;
[SerializeField]
    private float fireRate = 2f;
[SerializeField]
    private float nextFire = 0.0f;

    public int hp = 2;

    public Material blueMat;
    public Material redMat;

    public bool blue;

    public AudioSource source;
    public AudioClip laserSound;

    void Awake()
    {   scoreManager = GameObject.FindWithTag("SCORE").GetComponent<ScoreManager>();
        if (Swapey){
        if (blue)
        {
            gameObject.tag = "BLUE";
             redSon.SetActive(false);
             blueSon.SetActive(true);
             if(Shooty){
             launcher.ProjectileObject = blueLaser;
             }
        }
        else
        {
           gameObject.tag = "RED";
           redSon.SetActive(true);
           blueSon.SetActive(false);
           if (Shooty) {
           launcher.ProjectileObject = redLaser;
          }   
        } 
    }
    }
    void Swap()
    {
       
        if (blue)
        {
           gameObject.tag = "RED";
           redSon.SetActive(true);
           blueSon.SetActive(false);
           blue = false;
           launcher.ProjectileObject = redLaser;

        } 
        
        else
       
        {   
             gameObject.tag = "BLUE";
             redSon.SetActive(false);
             blueSon.SetActive(true);
             blue = true;
             launcher.ProjectileObject = blueLaser;
        }
    }

    void Update()
    {
        if (Movey)
        {
        Move();    
        }
    }   

    void OnTriggerEnter(Collider other)
    {
            
        if (other.tag=="RING")
        {  
            Swap();
        }
    }

    private void Move()
    {
        transform.LookAt(Camera.main.transform);
       // transform.Rotate(new Vector3(-8, 5, 0));
        if (Vector3.Distance(LocationCords[index].position, LocationCords[index + 1].position) < 0.5f)

        {
            normalSpeed = _NormalSpeed / 4;
        } else 
        {
            normalSpeed = _NormalSpeed;
        }

        if (shootLocations.Contains(index) && Time.time > nextFire)
        {
            if (Shooty)
            {
            launcher.ShootProjectile();
            nextFire = Time.time + fireRate;
            }
        }

        if (index > LocationCords.Length - 3)
        {
            index = 0;
        }

        float step = normalSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, LocationCords[index].position, step);

        if (Vector3.Distance(transform.position, LocationCords[index].position) < 0.001f)

        {
            index++;
        }
    }

    
}


