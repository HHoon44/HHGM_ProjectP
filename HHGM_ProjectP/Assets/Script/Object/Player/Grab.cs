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
    private Rigidbody grabbedObjRb; // 잡은 물건의 Rigidbody를 기억하기 위한 변수
    private Collider grabbedObjCollider; // 잡은 물건의 Collider를 기억하기 위한 변수

    public float grabRange = 2f; // 잡기 가능한 최대 거리
    public LayerMask grabLayer; // 감지할 레이어
    public Transform grabPoint; // 레이캐스트를 발사할 위치
    public Transform holdPoint; // 물체를 잡을 위치
    public Quaternion grabbedObjOriginalRotation;
    public Collider rightPunchCollider; // 오른손 펀치에 사용될 콜라이더
    public Collider leftPunchCollider; // 왼손 펀치에 사용될 콜라이더

    private newcontrol playerControl; // PlayerMovement 스크립트 참조

    private void Start()
    {
        // PlayerMovement 스크립트 찾기
        playerControl = FindObjectOfType<newcontrol>();
    }

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

                if (grabbedObj == null)
                {
                    RaycastHit hit;
                    // 레이캐스트 디버깅 라인 그리기
                    Debug.DrawRay(grabPoint.position, grabPoint.forward * grabRange, Color.red, 2f);

                    if (Physics.Raycast(grabPoint.position, grabPoint.forward, out hit, grabRange, grabLayer))
                    {
                        Debug.Log("Raycast hit: " + hit.collider.name);
                        if (hit.collider.CompareTag("Item"))
                        {
                            grabbedObj = hit.collider.gameObject;
                            Debug.Log("Item detected: " + grabbedObj.name);
                            // 원래 부모를 저장하여 나중에 복구할 수 있도록 함
                            grabbedObjOriginalParent = grabbedObj.transform.parent;
                            // 잡은 물건을 손의 자식으로 설정
                            grabbedObj.transform.SetParent(holdPoint);
                            // 선택적으로 로컬 위치/회전을 초기화하여 손과 정렬
                            grabbedObj.transform.localPosition = Vector3.zero;
                            grabbedObj.transform.localRotation = Quaternion.identity;
                            // 잡은 물건의 Rigidbody와 Collider를 가져옴
                            grabbedObjRb = grabbedObj.GetComponent<Rigidbody>();
                            grabbedObjCollider = grabbedObj.GetComponent<Collider>();
                            if (grabbedObjRb != null)
                            {
                                grabbedObjRb.isKinematic = true;
                                grabbedObjRb.velocity = Vector3.zero; // 속도를 초기화
                                grabbedObjRb.angularVelocity = Vector3.zero; // 회전 속도를 초기화
                            }
                            if (grabbedObjCollider != null)
                            {
                                grabbedObjCollider.enabled = false; // Collider 비활성화
                            }

                            // 이동 속도 조절
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

        // 오른쪽 마우스 버튼 클릭 시 펀치 로직 시작
        if (Input.GetMouseButtonDown(1))
        {
            if (PunchCount == 0)
            {
                // 오른손 펀치
                anim.SetBool("isRightPunch", true);
                anim.SetBool("isLeftPunch", false); // 왼손 펀치 애니메이션 비활성화
                PunchCount = 1;
                StartCoroutine(EnablePunchColliderTemporarily(rightPunchCollider, 0.5f)); // 예시로 0.5초동안 활성화
            }
            else if (PunchCount == 1)
            {
                // 왼손 펀치
                anim.SetBool("isRightPunch", false); // 오른손 펀치 애니메이션 비활성화
                anim.SetBool("isLeftPunch", true);
                PunchCount = 0;
                StartCoroutine(EnablePunchColliderTemporarily(leftPunchCollider, 0.5f)); // 예시로 0.5초동안 활성화
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
                    // 잡은 물건의 부모를 원래대로 복구
                    grabbedObj.transform.SetParent(grabbedObjOriginalParent);
                    // 잡은 물건의 Rigidbody와 Collider를 원래 상태로 되돌림
                    if (grabbedObjRb != null)
                    {
                        grabbedObjRb.isKinematic = false;
                        grabbedObjRb.velocity = Vector3.zero; // 속도를 초기화
                        grabbedObjRb.angularVelocity = Vector3.zero; // 회전 속도를 초기화
                    }
                    if (grabbedObjCollider != null)
                    {
                        grabbedObjCollider.enabled = true; // Collider 활성화
                    }
                    grabbedObj = null;

                    // 이동 속도 원래대로 복구
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
            Vector3 originalDirection = holdPoint.up; // holdPoint의 원래 방향
            Quaternion holdRotation = Quaternion.FromToRotation(originalDirection, holdDirection) * grabbedObjOriginalRotation;
            grabbedObj.transform.rotation = holdRotation;
        }
    }

    // 물건을 감지하는 콜라이더를 사용하는 대신 직접 레이캐스트로 감지
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
            Debug.Log(punchCollider.name + " 콜라이더 켜짐");
            yield return new WaitForSeconds(duration);
            punchCollider.enabled = false;
            Debug.Log(punchCollider.name + " 콜라이더 꺼짐");
        }
    }

    private void DisablePunchCollider()
    {
        if (rightPunchCollider != null)
        {
            rightPunchCollider.enabled = false;
            Debug.Log("오른손 콜라이더 꺼짐");
        }
        if (leftPunchCollider != null)
        {
            leftPunchCollider.enabled = false;
            Debug.Log("왼손 콜라이더 꺼짐");
        }
    }

    private void AdjustPlayerSpeed(string itemName)
    {
        if (itemName.Contains("Large_Score"))
        {
            playerControl.AdjustSpeed(0.8f, 0.8f); // 속도를 절반으로 줄임
        }
        else if (itemName.Contains("Middle_Score"))
        {
            playerControl.AdjustSpeed(0.9f, 0.9f); // 속도를 80%로 줄임
        }
        else
        {
            playerControl.AdjustSpeed(1f, 1f); // 기본 속도
        }
    }
}
