using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rigid;                 // ������ ���� ��
    public int isLeftOrRight;               // 0 ���� 1 ���
    public bool alreadyGrabbing = false;

    private GameObject grabbedObj;          // ���� ����

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // ��Ŭ���� ��� ����
        if (Input.GetMouseButtonDown(isLeftOrRight))
        {

            Debug.Log("click check");
            if (isLeftOrRight == 0)
            {
                // ������

                anim.SetBool("isGrab", true);
            }
            

            if (grabbedObj != null)
            {
                var fj = grabbedObj.AddComponent<FixedJoint>();
                fj.connectedBody = rigid;
                fj.breakForce = 9001;
            }
        }

        // ��� ����
        if (Input.GetMouseButtonUp(isLeftOrRight))
        {
            if (isLeftOrRight == 0 )
            {
                // ��� ����
                anim.SetBool("isGrab", false);
            }
           

            if (grabbedObj != null)
            {
                // ���� ������ �����ϴ� �۾�
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