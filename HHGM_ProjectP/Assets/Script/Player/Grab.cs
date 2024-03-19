using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProjectP.Object
{
    /// <summary>
    /// �÷��̾��� ��� ����� ����ϴ� Ŭ����
    /// </summary>
    public class Grab : MonoBehaviour
    {
        public Animator anim;
        public Rigidbody rigid;
        public int isLeftorRight;
        public bool alreadyGrabbing = false;

        // ������ ���� ������Ʈ
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