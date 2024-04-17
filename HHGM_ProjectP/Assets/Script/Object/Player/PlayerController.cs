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

                hip.AddForce(Vector3.forward * speed * 1.5f);
                
            }
            else
            {
                anim.SetBool("isWalk", true);
                anim.SetBool("isRun", false);

                hip.AddForce(Vector3.forward * speed);
                Debug.Log("w_check");
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

            hip.AddForce(-Vector3.right * strafeSpeed);
            Debug.Log("a_check");
        }
        else
        {
            anim.SetBool("isSideLeft", false);
        }

        // Back Move
        if (Input.GetKey(KeyCode.S))
        {
            
            anim.SetBool("isWalk", true);

            hip.AddForce(-Vector3.forward * speed);
            Debug.Log("s_check");
        }
        else if (!Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isWalk", false);
        }

        // Left Move
        if (Input.GetKey(KeyCode.D))
        {
            
            anim.SetBool("isSideRight", true);

            hip.AddForce(Vector3.right * strafeSpeed);
            Debug.Log("d_check");
        }
        else
        {
            anim.SetBool("isSideRight", false);
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            hip.AddForce(new Vector3(0, jumpForce, 0));
            isGround = false;
            Debug.Log("Jump");
        }


    }
    
}