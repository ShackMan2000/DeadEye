using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnFactory.Demo
{
    public class MovePlayerForward : MonoBehaviour
    {
        [SerializeField] private int moveForSeconds = 2;
        [SerializeField] private int moveSpeed = 2;
        private Animator anim;
        private float timer = 0;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (moveForSeconds > timer)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
                anim.SetBool("Moving", true);
                timer += Time.deltaTime;
            }
            else
            {
                anim.SetBool("Moving", false);
            }
        }
    }
}