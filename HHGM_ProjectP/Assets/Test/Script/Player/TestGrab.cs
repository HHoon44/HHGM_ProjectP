using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrab : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rigid;                 // 물건을 잡을 손
    public int isLeftOrRight;               // 0 왼손 1 오른손
    public bool alreadyGrabbing = false;

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

            if (grabbedObj != null)
            {
                var fj = grabbedObj.AddComponent<FixedJoint>();
                fj.connectedBody = rigid;
                fj.breakForce = 9001;
            }
        }

        // 잡기 종료
        if (Input.GetMouseButtonUp(isLeftOrRight))
        {
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
