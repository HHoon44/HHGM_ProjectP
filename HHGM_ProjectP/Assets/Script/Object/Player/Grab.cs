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
    private Rigidbody grabbedObjRb; // ���� ������ Rigidbody�� ����ϱ� ���� ����
    private Collider grabbedObjCollider; // ���� ������ Collider�� ����ϱ� ���� ����

    public float grabRange = 2f; // ��� ������ �ִ� �Ÿ�
    public LayerMask grabLayer; // ������ ���̾�
    public Transform grabPoint; // ����ĳ��Ʈ�� �߻��� ��ġ
    public Transform holdPoint; // ��ü�� ���� ��ġ
    public Quaternion grabbedObjOriginalRotation;

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

                if (grabbedObj == null)
                {
                    RaycastHit hit;
                    // ����ĳ��Ʈ ����� ���� �׸���
                    Debug.DrawRay(grabPoint.position, grabPoint.forward * grabRange, Color.red, 2f);

                    if (Physics.Raycast(grabPoint.position, grabPoint.forward, out hit, grabRange, grabLayer))
                    {
                        Debug.Log("Raycast hit: " + hit.collider.name);
                        if (hit.collider.CompareTag("Item"))
                        {
                            grabbedObj = hit.collider.gameObject;
                            Debug.Log("Item detected: " + grabbedObj.name);
                            // ���� �θ� �����Ͽ� ���߿� ������ �� �ֵ��� ��
                            grabbedObjOriginalParent = grabbedObj.transform.parent;
                            // ���� ������ ���� �ڽ����� ����
                            grabbedObj.transform.SetParent(holdPoint);
                            // ���������� ���� ��ġ/ȸ���� �ʱ�ȭ�Ͽ� �հ� ����
                            grabbedObj.transform.localPosition = Vector3.zero;
                            grabbedObj.transform.localRotation = Quaternion.identity;
                            // ���� ������ Rigidbody�� Collider�� ������
                            grabbedObjRb = grabbedObj.GetComponent<Rigidbody>();
                            grabbedObjCollider = grabbedObj.GetComponent<Collider>();
                            if (grabbedObjRb != null)
                            {
                                grabbedObjRb.isKinematic = true;
                                grabbedObjRb.velocity = Vector3.zero; // �ӵ��� �ʱ�ȭ
                                grabbedObjRb.angularVelocity = Vector3.zero; // ȸ�� �ӵ��� �ʱ�ȭ
                            }
                            if (grabbedObjCollider != null)
                            {
                                grabbedObjCollider.enabled = false; // Collider ��Ȱ��ȭ
                            }
                        }
                        else
                        {
                            Debug.Log("Hit object is not an Item");
                        }
                    }
                    else
                    {
                        Debug.Log("Raycast did not hit anything");
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
                    // ���� ������ �θ� ������� ����
                    grabbedObj.transform.SetParent(grabbedObjOriginalParent);
                    // ���� ������ Rigidbody�� Collider�� ���� ���·� �ǵ���
                    if (grabbedObjRb != null)
                    {
                        grabbedObjRb.isKinematic = false;
                        grabbedObjRb.velocity = Vector3.zero; // �ӵ��� �ʱ�ȭ
                        grabbedObjRb.angularVelocity = Vector3.zero; // ȸ�� �ӵ��� �ʱ�ȭ
                    }
                    if (grabbedObjCollider != null)
                    {
                        grabbedObjCollider.enabled = true; // Collider Ȱ��ȭ
                    }
                    grabbedObj = null;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (grabbedObj != null)
        {
            grabbedObj.transform.position = holdPoint.position;
            grabbedObj.transform.rotation = holdPoint.rotation;

            Vector3 holdDirection = holdPoint.forward;
            Vector3 originalDirection = holdPoint.up; // holdPoint�� ���� ����
            Quaternion holdRotation = Quaternion.FromToRotation(originalDirection, holdDirection) * grabbedObjOriginalRotation;
            grabbedObj.transform.rotation = holdRotation;
        }
    }

    // ������ �����ϴ� �ݶ��̴��� ����ϴ� ��� ���� ����ĳ��Ʈ�� ����
    private void OnDrawGizmos()
    {
        if (grabPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(grabPoint.position, grabPoint.position + grabPoint.forward * grabRange);
        }
    }

    private void Punching()
    {
        // ��ġ ���� ����
    }
}
