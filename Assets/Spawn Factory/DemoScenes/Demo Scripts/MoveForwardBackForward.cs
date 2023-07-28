using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnFactory.Demo
{
    public class MoveForwardBackForward : MonoBehaviour
    {
        [SerializeField] private int moveSpeed = 2;
        [SerializeField] private int moveForwardTime1 = 2;
        [SerializeField] private int moveBackwardTime = 2;
        [SerializeField] private int moveForwardTime2 = 2;

        private Animator anim;
        private float timer = 0;
        private Quaternion backRot;
        private Quaternion forwardRot;
        bool rotatedBack = false;
        //bool rotatedBacka = false;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponentInChildren<Animator>();

            backRot = Quaternion.Euler(0, 90, 0);
            forwardRot = Quaternion.Euler(0, -90, 0);
            moveBackwardTime += moveForwardTime1;
            moveForwardTime2 += moveBackwardTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (moveForwardTime1 > timer)
            {
                MoveForward();
            }
            else if(moveBackwardTime > timer)
            {
                if (!rotatedBack)
                {
                    transform.rotation = backRot;
                    rotatedBack = true;
                }
                MoveForward();
            }
            else if(moveForwardTime2 > timer)
            {
                if (rotatedBack)
                {
                    transform.rotation = forwardRot;
                    rotatedBack = false;
                }
                MoveForward();
            }
            else
            {
                anim.SetBool("Moving", false);
            }
        }

        private void MoveForward()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            anim.SetBool("Moving", true);
            timer += Time.deltaTime;
        }
    }
}