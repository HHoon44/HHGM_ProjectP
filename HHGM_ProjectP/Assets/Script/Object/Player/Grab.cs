using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Animator anim;
    public int isLeftOrRight; // 0: 왼쪽 클릭, 1: 오른쪽 클릭
    public int PunchCount = 0;

    private GameObject grabbedObj; // 잡은 물건
    private Transform grabbedObjOriginalParent; // 잡은 물건의 원래 부모를 기억하기 위한 변수
    private Vector3 grabbedObjOriginalPosition; // 잡은 물건의 원래 위치를 기억하기 위한 변수
    private Quaternion grabbedObjOriginalRotation; // 잡은 물건의 원래 회전을 기억하기 위한 변수

    private void Update()
    {
        // 왼쪽 마우스 버튼 클릭 시 잡기 시작
        if (Input.GetMouseButtonDown(isLeftOrRight))
        {
            Debug.Log("Mouse button " + isLeftOrRight + " pressed");
            if (isLeftOrRight == 0)
            {
                // 잡기 애니메이션 시작
                anim.SetBool("isGrab", true);

                if (grabbedObj != null)
                {
                    // 원래 부모를 저장하여 나중에 복구할 수 있도록 함
                    grabbedObjOriginalParent = grabbedObj.transform.parent;
                    // 원래 위치와 회전을 저장
                    grabbedObjOriginalPosition = grabbedObj.transform.position;
                    grabbedObjOriginalRotation = grabbedObj.transform.rotation;
                    // 잡은 물건을 손의 자식으로 설정
                    grabbedObj.transform.SetParent(this.transform);
                    // 선택적으로 로컬 위치/회전을 초기화하여 손과 정렬
                    grabbedObj.transform.localPosition = Vector3.zero;
                    grabbedObj.transform.localRotation = Quaternion.identity;
                    // 잡은 물건의 Rigidbody를 Kinematic으로 설정하여 물리적 충돌을 비활성화
                    Rigidbody grabbedRb = grabbedObj.GetComponent<Rigidbody>();
                    if (grabbedRb != null)
                    {
                        grabbedRb.isKinematic = true;
                    }
                }
            }
        }

        // 오른쪽 마우스 버튼 클릭 시 펀치 로직 시작
        if (Input.GetMouseButtonDown(1))
        {
            if (PunchCount == 0)
            {
                // 오른손 펀치
                anim.SetBool("isRightPunch", true);
                anim.SetBool("isLeftPunch", false); // 왼손 펀치 애니메이션 비활성화
                PunchCount = 1;
                Punching();
            }
            else if (PunchCount == 1)
            {
                // 왼손 펀치
                anim.SetBool("isRightPunch", false); // 오른손 펀치 애니메이션 비활성화
                anim.SetBool("isLeftPunch", true);
                PunchCount = 0;
                Punching();
            }
        }

        // 오른쪽 마우스 버튼을 놓으면 펀치 애니메이션 종료
        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("isRightPunch", false);
            anim.SetBool("isLeftPunch", false);
        }

        // 왼쪽 마우스 버튼을 놓으면 잡기 종료
        if (Input.GetMouseButtonUp(isLeftOrRight))
        {
            if (isLeftOrRight == 0)
            {
                // 잡기 애니메이션 종료
                anim.SetBool("isGrab", false);

                if (grabbedObj != null)
                {
                    // 잡은 물건의 원래 부모 복구
                    grabbedObj.transform.SetParent(grabbedObjOriginalParent);
                    // 원래 위치와 회전을 복구
                    grabbedObj.transform.position = grabbedObjOriginalPosition;
                    grabbedObj.transform.rotation = grabbedObjOriginalRotation;
                    // 잡은 물건의 Rigidbody를 원래 상태로 되돌림
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
        // 펀치 로직 구현
    }
}
