using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public Collider leftPunchCollider; // 왼손 펀치에 사용될 콜라이더
    public Collider rightPunchCollider; // 오른손 펀치에 사용될 콜라이더
    private Animator anim;
    private int PunchCount = 0;

    void Start()
    {
        // 애니메이터를 캐릭터 본체에서 찾음
        anim = GetComponent<Animator>();

        if (leftPunchCollider != null)
            leftPunchCollider.enabled = false; // 초기에는 콜라이더 비활성화

        if (rightPunchCollider != null)
            rightPunchCollider.enabled = false; // 초기에는 콜라이더 비활성화
    }

    void Update()
    {
        // 오른쪽 마우스 버튼 클릭 시 펀치 로직 시작
        if (Input.GetMouseButtonDown(1))
        {
            if (PunchCount == 0)
            {
                // 오른손 펀치
                anim.SetBool("isRightPunch", true);
                anim.SetBool("isLeftPunch", false); // 왼손 펀치 애니메이션 비활성화
                PunchCount = 1;
            }
            else if (PunchCount == 1)
            {
                // 왼손 펀치
                anim.SetBool("isRightPunch", false); // 오른손 펀치 애니메이션 비활성화
                anim.SetBool("isLeftPunch", true);
                PunchCount = 0;
            }
        }

        // 오른쪽 마우스 버튼을 놓으면 펀치 애니메이션 종료
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
