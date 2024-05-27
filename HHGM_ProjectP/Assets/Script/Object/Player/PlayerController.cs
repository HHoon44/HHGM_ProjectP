using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] weapons;

    public Transform weaponSocket;
    public Transform player;
    private Transform attachedWeapon;

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

        /*if (Input.GetKeyDown(KeyCode.Space) && isGround && (faint == false))
        {
            hip.AddForce(new Vector3(0, jumpForce, 0));
            isGround = false;
        }*/
        if(Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isFilpkick", true);
            hip.AddForce(Vector3.forward * speed * 5f);

        }
        else
        {
            anim.SetBool("isFilpkick", false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (attachedWeapon == null) // ���Ⱑ �����Ǿ� ���� ���� ���
            {
                // �÷��̾� �ֺ��� ��� ���⸦ �˻��Ͽ� ���� ����� ���⸦ ã���ϴ�.
                Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
                float closestDistance = Mathf.Infinity;
                Collider closestCollider = null;

                foreach (Collider col in colliders)
                {
                    if (col.CompareTag("Weapon"))
                    {
                        float distance = Vector3.Distance(transform.position, col.transform.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestCollider = col;
                        }
                    }
                }

                // ���� ����� ���⸦ ã�� ��� �ش� ���⸦ �����մϴ�.
                if (closestCollider != null)
                {
                    AttachWeapon(closestCollider.transform);
                }
            }
            else // ���Ⱑ �̹� �����Ǿ� �ִ� ���
            {
                DetachWeapon();
            }
        }

        if (HP <= 0)
        {
            if (nobanbok == 1)
            {
                gijal();
            }
        }

        if (attachedWeapon != null && attachedWeapon.gameObject.name == "Boomerang" && Input.GetMouseButtonDown(1)) // �θ޶� ����
        {
            ThrowBoomerang();
        }

        if (attachedWeapon != null && attachedWeapon.gameObject.name == "Bomb" && Input.GetMouseButtonDown(1)) // ��ź�� �����Ǿ� �ְ� ��Ŭ���� ������ ��
        {
            ThrowBomb();
        }
    }

    void AttachWeapon(Transform weaponTransform)
    {
        // ������ Transform�� ������ ������ Transform�� �Ҵ��մϴ�.
        weaponTransform.parent = weaponSocket;
        weaponTransform.localPosition = Vector3.zero;
        weaponTransform.localRotation = Quaternion.identity;
        weaponTransform.localScale = Vector3.one;

        // ���Ⱑ �÷��̾��� �� ������ �ٶ󺸵��� ȸ���մϴ�.
        Vector3 direction = player.forward;
        Quaternion rotation = Quaternion.LookRotation(direction);
        weaponTransform.rotation = rotation;

        // Rigidbody ��Ȱ��ȭ
        Rigidbody weaponRb = weaponTransform.GetComponent<Rigidbody>();
        if (weaponRb != null)
        {
            weaponRb.isKinematic = true;
            weaponRb.detectCollisions = false;
        }

        // ���� ������ ���⸦ �����մϴ�.
        attachedWeapon = weaponTransform;
    }

    void DetachWeapon()
    {
        if (attachedWeapon != null)
        {
            if (attachedWeapon.gameObject.name == "Bomb")
            {
                // ��ź�� ���� �����ϰ� �ı��մϴ�.
                Destroy(attachedWeapon.gameObject);
            }
            else
            {
                // �ٸ� ���⸦ ���� �����մϴ�.
                attachedWeapon.parent = null;

                // ������ ��ġ�� ���� ��ġ�� �ٷ� �Ʒ��� �����մϴ�.
                attachedWeapon.position = weaponSocket.position - new Vector3(0, 0.5f, 0);

                // Rigidbody Ȱ��ȭ
                Rigidbody weaponRb = attachedWeapon.GetComponent<Rigidbody>();
                if (weaponRb != null)
                {
                    weaponRb.isKinematic = false;
                    weaponRb.detectCollisions = true;
                }
            }

            attachedWeapon = null;
        }
    }

    void ThrowBoomerang()
    {
        GameObject boomerang = Instantiate(attachedWeapon.gameObject, weaponSocket.position, Quaternion.identity);
        Boomerang boomerangScript = boomerang.GetComponent<Boomerang>();

        Vector3 targetPosition = player.position + player.forward * 10f; // �θ޶��� ���ư� ��ǥ ��ġ ����
        boomerangScript.Initialize(targetPosition, player);

        // ���� ������ �θ޶��� �����մϴ�.
        Destroy(attachedWeapon.gameObject);
        attachedWeapon = null;
    }

    void ThrowBomb()
    {
        // ��ź�� �����ϰ� throwPoint ��ġ�� �����ϴ�.
        GameObject bomb = Instantiate(attachedWeapon.gameObject, weaponSocket.position, Quaternion.identity);
        Rigidbody bombRb = bomb.GetComponent<Rigidbody>();

        // Rigidbody Ȱ��ȭ
        if (bombRb != null)
        {
            bombRb.isKinematic = false;
            bombRb.detectCollisions = true;
            bombRb.AddForce(player.forward * 10f, ForceMode.Impulse);
        }

        // ���� �ð� �Ŀ� ��ź�� ���߽�ŵ�ϴ�.
        BombExplosion explosion = bomb.AddComponent<BombExplosion>();
        explosion.explosionRadius = 5f;
        explosion.explosionForce = 10f;
        explosion.ExplodeAfterSeconds(3f);

        DetachWeapon();
    }

    private void gijal()
    {
        faint = true;

        if (joint != null)
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