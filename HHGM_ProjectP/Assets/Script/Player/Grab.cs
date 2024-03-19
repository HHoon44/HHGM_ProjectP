using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProjectP.Object
{
    /// <summary>
    /// 플레이어의 잡기 모션을 담당하는 클래스
    /// </summary>
    public class Grab : MonoBehaviour
    {
        public Animator anim;
        public Rigidbody rigid;
        public int isLeftorRight;
        public bool alreadyGrabbing = false;

        // 유저가 잡은 오브젝트
        private GameObject grabbedObj;

        private void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(isLeftorRight))
            {
                if (isLeftorRight == 0)
                {
                    anim.SetBool("isLeftHandUp", true);
                }
                else if (isLeftorRight == 1)
                {
                    anim.SetBool("isRightHandUp", true);
                }

                var fj = grabbedObj.AddComponent<FixedJoint>();
                fj.connectedBody = rigid;
                fj.breakForce = 9001;
            }
            else if (Input.GetMouseButtonUp(isLeftorRight))
            {
                if (isLeftorRight == 0)
                {
                    anim.SetBool("isLeftHandUp", false);
                }
                else if (isLeftorRight == 1)
                {
                    anim.SetBool("isLeftHandUp", false);
                }

                if (grabbedObj != null)
                {
                    Destroy(grabbedObj.GetComponent<FixedJoint>());
                }

                grabbedObj = null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Item"))
            {
                grabbedObj = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            grabbedObj = null;
        }
    }
}