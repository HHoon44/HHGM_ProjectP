using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public Collider leftPunchCollider; // �޼� ��ġ�� ���� �ݶ��̴�
    public Collider rightPunchCollider; // ������ ��ġ�� ���� �ݶ��̴�
    private Animator anim;
    private int PunchCount = 0;

    void Start()
    {
        // �ִϸ����͸� ĳ���� ��ü���� ã��
        anim = GetComponent<Animator>();

        if (leftPunchCollider != null)
            leftPunchCollider.enabled = false; // �ʱ⿡�� �ݶ��̴� ��Ȱ��ȭ

        if (rightPunchCollider != null)
            rightPunchCollider.enabled = false; // �ʱ⿡�� �ݶ��̴� ��Ȱ��ȭ
    }

    void Update()
    {
        // ������ ���콺 ��ư Ŭ�� �� ��ġ ���� ����
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

        // ������ ���콺 ��ư�� ������ ��ġ �ִϸ��̼� ����
        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("isRightPunch", false);
            anim.SetBool("isLeftPunch", false);
        }
    }

    public void EnableLeftPunchCollider()
    {
        if (leftPunchCollider != null)
        {
            leftPunchCollider.enabled = true;
        }
    }

    public void DisableLeftPunchCollider()
    {
        if (leftPunchCollider != null)
        {
            leftPunchCollider.enabled = false;
        }
    }

    public void EnableRightPunchCollider()
    {
        if (rightPunchCollider != null)
        {
            rightPunchCollider.enabled = true;
        }
    }

    public void DisableRightPunchCollider()
    {
        if (rightPunchCollider != null)
        {
            rightPunchCollider.enabled = false;
        }
    }
}
