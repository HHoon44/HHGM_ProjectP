using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Animator anim;
    public int isLeftOrRight; // 0: ���� Ŭ��, 1: ������ Ŭ��
    public int PunchCount = 0;

    private GameObject grabbedObj; // ���� ����
    private Transform grabbedObjOriginalParent; // ���� ������ ���� �θ� ����ϱ� ���� ����
    private Vector3 grabbedObjOriginalPosition; // ���� ������ ���� ��ġ�� ����ϱ� ���� ����
    private Quaternion grabbedObjOriginalRotation; // ���� ������ ���� ȸ���� ����ϱ� ���� ����

    private void Update()
    {
        // ���� ���콺 ��ư Ŭ�� �� ��� ����
        if (Input.GetMouseButtonDown(isLeftOrRight))
        {
            Debug.Log("Mouse button " + isLeftOrRight + " pressed");
            if (isLeftOrRight == 0)
            {
                // ��� �ִϸ��̼� ����
                anim.SetBool("isGrab", true);

                if (grabbedObj != null)
                {
                    // ���� �θ� �����Ͽ� ���߿� ������ �� �ֵ��� ��
                    grabbedObjOriginalParent = grabbedObj.transform.parent;
                    // ���� ��ġ�� ȸ���� ����
                    grabbedObjOriginalPosition = grabbedObj.transform.position;
                    grabbedObjOriginalRotation = grabbedObj.transform.rotation;
                    // ���� ������ ���� �ڽ����� ����
                    grabbedObj.transform.SetParent(this.transform);
                    // ���������� ���� ��ġ/ȸ���� �ʱ�ȭ�Ͽ� �հ� ����
                    grabbedObj.transform.localPosition = Vector3.zero;
                    grabbedObj.transform.localRotation = Quaternion.identity;
                    // ���� ������ Rigidbody�� Kinematic���� �����Ͽ� ������ �浹�� ��Ȱ��ȭ
                    Rigidbody grabbedRb = grabbedObj.GetComponent<Rigidbody>();
                    if (grabbedRb != null)
                    {
                        grabbedRb.isKinematic = true;
                    }
                }
            }
        }

        // ������ ���콺 ��ư Ŭ�� �� ��ġ ���� ����
        if (Input.GetMouseButtonDown(1))
        {
            if (PunchCount == 0)
            {
                // ������ ��ġ
                anim.SetBool("isRightPunch", true);
                anim.SetBool("isLeftPunch", false); // �޼� ��ġ �ִϸ��̼� ��Ȱ��ȭ
                PunchCount = 1;
                Punching();
            }
            else if (PunchCount == 1)
            {
                // �޼� ��ġ
                anim.SetBool("isRightPunch", false); // ������ ��ġ �ִϸ��̼� ��Ȱ��ȭ
                anim.SetBool("isLeftPunch", true);
                PunchCount = 0;
                Punching();
            }
        }

        // ������ ���콺 ��ư�� ������ ��ġ �ִϸ��̼� ����
        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("isRightPunch", false);
            anim.SetBool("isLeftPunch", false);
        }

        // ���� ���콺 ��ư�� ������ ��� ����
        if (Input.GetMouseButtonUp(isLeftOrRight))
        {
            if (isLeftOrRight == 0)
            {
                // ��� �ִϸ��̼� ����
                anim.SetBool("isGrab", false);

                if (grabbedObj != null)
                {
                    // ���� ������ ���� �θ� ����
                    grabbedObj.transform.SetParent(grabbedObjOriginalParent);
                    // ���� ��ġ�� ȸ���� ����
                    grabbedObj.transform.position = grabbedObjOriginalPosition;
                    grabbedObj.transform.rotation = grabbedObjOriginalRotation;
                    // ���� ������ Rigidbody�� ���� ���·� �ǵ���
                    Rigidbody grabbedRb = grabbedObj.GetComponent<Rigidbody>();
                    if (grabbedRb != null)
                    {
                        grabbedRb.isKinematic = false;
                    }
                    grabbedObj = null;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            grabbedObj = collision.transform.parent != null ? collision.transform.parent.gameObject : collision.gameObject;
            Debug.Log("Item detected: " + grabbedObj.name);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Debug.Log("Item exited: " + collision.gameObject.name);
            grabbedObj = null;
        }
    }

    private void Punching()
    {
        // ��ġ ���� ����
    }
}
