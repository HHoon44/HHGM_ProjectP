using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rigid;                 // ������ ���� ��
    public int isLeftOrRight;               // 0 ���� 1 ���
    public bool alreadyGrabbing = false;
    public int PunchCount = 0;
  
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
        if (Input.GetMouseButtonDown(1))
        {
            if (PunchCount == 0)
            {
                // ������ ��ġ
                anim.SetBool("isRightPunch", true);
                anim.SetBool("isLeftPunch", false); // �޼� ��ġ �ִϸ��̼� ��Ȱ��ȭ
                PunchCount = 1;
            }
            else if (PunchCount == 1)
            {
                // �޼� ��ġ
                anim.SetBool("isRightPunch", false); // ������ ��ġ �ִϸ��̼� ��Ȱ��ȭ
                anim.SetBool("isLeftPunch", true);
                PunchCount = 0;
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            // ��ġ ����
            anim.SetBool("isRightPunch", false);
            anim.SetBool("isLeftPunch", false);
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