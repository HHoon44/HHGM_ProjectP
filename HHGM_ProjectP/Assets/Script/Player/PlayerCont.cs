using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerCont : MonoBehaviour
{
    public Animator animator;

    public float speed;
    public float strafeSpeed;
    public float jumpForce;

    public Rigidbody hips;
    public bool isGrounded;

    private void Start()
    {
        hips = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isWalk", true);
                animator.SetBool("isRun", true);
                hips.AddForce(hips.transform.forward * speed * 1.5f);
            }
            else
            {
                animator.SetBool("isRun", false);
                animator.SetBool("isWalk", true);
                hips.AddForce(hips.transform.forward * speed);
            }

        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);
        }
        
        


        if (Input.GetKey(KeyCode.A))
        {
            hips.AddForce(-hips.transform.right * strafeSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            hips.AddForce(-hips.transform.forward * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            hips.AddForce(hips.transform.right * strafeSpeed);
        }
        if (Input.GetAxis("Jump") > 0)
        {
            if (isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            }
        }

    }

}
