using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rigid;                 // 물건을 잡을 손
    public int isLeftOrRight;               // 0 놓기 1 잡기
    public bool alreadyGrabbing = false;
    public int PunchCount = 0;
  
    private GameObject grabbedObj;          // 잡은 물건

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 왼클릭시 잡기 시작
        if (Input.GetMouseButtonDown(isLeftOrRight))
        {


            if (isLeftOrRight == 0)
            {
                // 양손잡기

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

        if (Input.GetMouseButtonUp(1))
        {
            // 펀치 종료
            anim.SetBool("isRightPunch", false);
            anim.SetBool("isLeftPunch", false);
        }

        // 잡기 종료
        if (Input.GetMouseButtonUp(isLeftOrRight))
        {
            if (isLeftOrRight == 0 )
            {
                // 잡기 놓기
                anim.SetBool("isGrab", false);
            }
            


            if (grabbedObj != null)
            {
                // 잡은 물건을 제거하는 작업
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