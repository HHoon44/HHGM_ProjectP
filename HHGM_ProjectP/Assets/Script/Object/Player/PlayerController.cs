using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] weapons;
    public bool[] hasWeapons;
    public float speed;
    public float strafeSpeed;
    public float jumpForce;
    public bool isGround;
    public Animator anim;
    bool isJump;
    public Rigidbody hip;
    public bool faint = false;
    public int nobanbok = 1;

    public float HP = 100;

    public ConfigurableJoint joint;

    GameObject nearObject;

    private void FixedUpdate()
    {
        // Forward Move
        if (Input.GetKey(KeyCode.W) && (faint == false))
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
               
            }
        }
        else
        {
            anim.SetBool("isWalk", false);
            anim.SetBool("isRun", false);
        }

        // Right Move
        if (Input.GetKey(KeyCode.A) && (faint == false))
        {
            
            anim.SetBool("isSideLeft", true);

            hip.AddForce(-Vector3.right * strafeSpeed);
            
        }
        else
        {
            anim.SetBool("isSideLeft", false);
        }

        // Back Move
        if (Input.GetKey(KeyCode.S) && (faint == false))
        {
            
            anim.SetBool("isWalk", true);

            hip.AddForce(-Vector3.forward * speed);
            
        }
        else if (!Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isWalk", false);
        }

        // Left Move
        if (Input.GetKey(KeyCode.D) && (faint == false))
        {
            
            anim.SetBool("isSideRight", true);

            hip.AddForce(Vector3.right * strafeSpeed);
           
        }
        else
        {
            anim.SetBool("isSideRight", false);
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGround && (faint == false) )
        {
            hip.AddForce(new Vector3(0, jumpForce, 0));
            isGround = false;
           
            
        }
        if(Input.GetKeyDown(KeyCode.E) && (faint == false))
        {
            Interation();
        }
        if (HP <= 0)
        {
            if(nobanbok == 1)
            {
                gijal();
            }
            
        }

    }
    void Interation()
    {
        if (nearObject != null) // nearObject�� null�� �ƴ� ��쿡�� ����
        {
            if (!isGround)
            {
                Debug.Log("���Ⱘ�� ���ܰ�");
                if (nearObject.tag == "Weapon")
                {
                    Debug.Log("���Ⱘ��");
                    Item item = nearObject.GetComponent<Item>();
                    int weaponIndex = item.value;
                    hasWeapons[weaponIndex] = true;

                    Destroy(nearObject);
                }
            }
        }
        else
        {
            Debug.Log("�ֺ��� ��ȣ�ۿ� ������ ������Ʈ�� �����ϴ�.");
        }
    }

    private void gijal()
    {
        faint = true;

        if(joint != null)
        {
            nobanbok = 0;
            JointDrive drive = joint.angularYZDrive;

            // ������ ���� ����
            drive.positionSpring = 0;

            // angularYZDrive ���� ����
            joint.angularYZDrive = drive;
            Invoke("getup", 5f);
        }
    }

    private void getup()
    {
        JointDrive drive = joint.angularYZDrive;

        // ������ ���� ����
        drive.positionSpring = 1000;

        // angularYZDrive ���� ����
        joint.angularYZDrive = drive;

        HP = 100;
        nobanbok = 1;
        faint = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Punch")
        {
            HP -= 25;
        }
    }

}