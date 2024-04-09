using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float strafeSpeed;
    public float jumpForce;
    public bool isGround;
    public Animator anim;

    public Rigidbody hip;

    private void FixedUpdate()
    {
        // Forward Move
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("isWalk", true);
                anim.SetBool("isRun", true);

                hip.AddForce(hip.transform.forward * speed * 1.5f);
            }
            else
            {
                anim.SetBool("isWalk", true);
                anim.SetBool("isRun", false);

                hip.AddForce(hip.transform.forward * speed);
            }
        }
        else
        {
            anim.SetBool("isWalk", false);
            anim.SetBool("isRun", false);
        }

        // Right Move
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("isSideLeft", true);

            hip.AddForce(-hip.transform.right * strafeSpeed);
        }
        else
        {
            anim.SetBool("isSideLeft", false);
        }

        // Back Move
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("isWalk", true);

            hip.AddForce(-hip.transform.forward * speed);
        }
        else if (!Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isWalk", false);
        }

        // Left Move
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isSideRight", true);

            hip.AddForce(hip.transform.right * strafeSpeed);
        }
        else
        {
            anim.SetBool("isSideRight", false);
        }


        if (Input.GetAxis("Jump") > 0)
        {
            if (!isGround)
            {
                hip.AddForce(new Vector3(0, jumpForce, 0));
                isGround = true;
            }
        }

        if(Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(0)) {
            anim.SetBool("isDropkick", true);
            Invoke("EndDropkick", 1);
        }
    }
    void EndDropkick() {
        anim.SetBool("isDropkick", false);
    }
}