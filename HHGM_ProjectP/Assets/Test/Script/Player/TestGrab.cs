using ProjectP.Object;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class TestGrab : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rigid;                 // 물건을 잡을 손
    public int isLeftOrRight;               // 0 왼손 1 오른손
    public bool alreadyGrabbing = false;

    [SerializeField]
    private GameObject checkObj;

    [SerializeField]
    private GameObject grabbedObj;          // 잡은 물건

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 잡기 시작
        if (Input.GetMouseButtonDown(isLeftOrRight))
        {
            if (isLeftOrRight == 0)
            {
                // 왼손
                anim.SetBool("isLeftHandUp", true);
            }
            else if (isLeftOrRight == 1)
            {
                // 오른손
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

        // 잡기 종료
        if (Input.GetMouseButtonUp(isLeftOrRight))
        {
            if (grabbedObj != null)
            {
                // 잡은 물건을 제거하는 작업
                Destroy(grabbedObj.GetComponent<FixedJoint>());
                grabbedObj = null;
            }

            if (isLeftOrRight == 0)
            {
                // 왼손
                anim.SetBool("isLeftHandUp", false);
            }
            else if (isLeftOrRight == 1)
            {
                // 오른손
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