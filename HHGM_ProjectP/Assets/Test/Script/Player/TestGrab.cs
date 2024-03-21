using ProjectP.Object;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class TestGrab : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rigid;                 // ������ ���� ��
    public int isLeftOrRight;               // 0 �޼� 1 ������
    public bool alreadyGrabbing = false;

    [SerializeField]
    private GameObject checkObj;

    [SerializeField]
    private GameObject grabbedObj;          // ���� ����

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // ��� ����
        if (Input.GetMouseButtonDown(isLeftOrRight))
        {
            if (isLeftOrRight == 0)
            {
                // �޼�
                anim.SetBool("isLeftHandUp", true);
            }
            else if (isLeftOrRight == 1)
            {
                // ������
                anim.SetBool("isRightHandUp", true);
            }

            if (checkObj != null && grabbedObj == null)
            {
                var fj = checkObj.AddComponent<FixedJoint>();
                fj.connectedBody = rigid;
                fj.breakForce = 15000;
                fj.enableCollision = true;

                grabbedObj = checkObj;
            }
        }

        // ��� ����
        if (Input.GetMouseButtonUp(isLeftOrRight))
        {
            if (grabbedObj != null)
            {
                // ���� ������ �����ϴ� �۾�
                Destroy(grabbedObj.GetComponent<FixedJoint>());
                grabbedObj = null;
            }

            if (isLeftOrRight == 0)
            {
                // �޼�
                anim.SetBool("isLeftHandUp", false);
            }
            else if (isLeftOrRight == 1)
            {
                // ������
                anim.SetBool("isRightHandUp", false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            checkObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        checkObj = null;
    }
}