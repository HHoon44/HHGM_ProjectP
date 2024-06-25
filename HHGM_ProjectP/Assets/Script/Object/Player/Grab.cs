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
    public Collider rightPunchCollider; // ������ ��ġ�� ���� �ݶ��̴�
    public Collider leftPunchCollider; // �޼� ��ġ�� ���� �ݶ��̴�

    private newcontrol playerControl; // PlayerMovement ��ũ��Ʈ ����

    private void Start()
    {
        // PlayerMovement ��ũ��Ʈ ã��
        playerControl = FindObjectOfType<newcontrol>();
    }

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

                            // �̵� �ӵ� ����
                            AdjustPlayerSpeed(grabbedObj.name);
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
                StartCoroutine(EnablePunchColliderTemporarily(rightPunchCollider, 0.5f)); // ���÷� 0.5�ʵ��� Ȱ��ȭ
            }
            else if (PunchCount == 1)
            {
                // �޼� ��ġ
                anim.SetBool("isRightPunch", false); // ������ ��ġ �ִϸ��̼� ��Ȱ��ȭ
                anim.SetBool("isLeftPunch", true);
                PunchCount = 0;
                StartCoroutine(EnablePunchColliderTemporarily(leftPunchCollider, 0.5f)); // ���÷� 0.5�ʵ��� Ȱ��ȭ
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

                    // �̵� �ӵ� ������� ����
                    playerControl.AdjustSpeed(1f, 1f);
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

    private IEnumerator EnablePunchColliderTemporarily(Collider punchCollider, float duration)
    {
        if (punchCollider != null)
        {
            punchCollider.enabled = true;
            Debug.Log(punchCollider.name + " �ݶ��̴� ����");
            yield return new WaitForSeconds(duration);
            punchCollider.enabled = false;
            Debug.Log(punchCollider.name + " �ݶ��̴� ����");
        }
    }

    private void DisablePunchCollider()
    {
        if (rightPunchCollider != null)
        {
            rightPunchCollider.enabled = false;
            Debug.Log("������ �ݶ��̴� ����");
        }
        if (leftPunchCollider != null)
        {
            leftPunchCollider.enabled = false;
            Debug.Log("�޼� �ݶ��̴� ����");
        }
    }

    private void AdjustPlayerSpeed(string itemName)
    {
        if (itemName.Contains("Large_Score"))
        {
            playerControl.AdjustSpeed(0.8f, 0.8f); // �ӵ��� �������� ����
        }
        else if (itemName.Contains("Middle_Score"))
        {
            playerControl.AdjustSpeed(0.9f, 0.9f); // �ӵ��� 80%�� ����
        }
        else
        {
            playerControl.AdjustSpeed(1f, 1f); // �⺻ �ӵ�
        }
    }
}
