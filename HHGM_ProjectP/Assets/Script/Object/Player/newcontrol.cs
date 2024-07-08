using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newcontrol : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject bulletPrefab; // �Ѿ� ������

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

    GameObject nearObject;
    public float rotationSpeed;

    private float originalSpeed;
    private float originalStrafeSpeed;

    private void Start()
    {
        // ���� �ӵ��� ����
        originalSpeed = speed;
        originalStrafeSpeed = strafeSpeed;
    }

    private void FixedUpdate()
    {
        Vector3 movementDirection = Vector3.zero;

        // Forward Move
        if (Input.GetKey(KeyCode.W) && (faint == false))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("isWalk", true);
                anim.SetBool("isRun", true);
                hip.AddForce(Vector3.forward * speed * 1.5f);
                movementDirection += Vector3.forward;
            }
            else
            {
                anim.SetBool("isWalk", true);
                anim.SetBool("isRun", false);
                hip.AddForce(Vector3.forward * speed);
                movementDirection += Vector3.forward;
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
            movementDirection -= Vector3.right;
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
            movementDirection -= Vector3.forward;
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
            movementDirection += Vector3.right;
        }
        else
        {
            anim.SetBool("isSideRight", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && (faint == false) && isGround)
        {
            hip.AddForce(new Vector3(0, jumpForce, 0));
            isGround = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space))
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

        if (attachedWeapon != null && attachedWeapon.gameObject.name == "Bomb" && Input.GetMouseButtonDown(1)) // ��ź�� �����Ǿ� �ְ� ��Ŭ���� ������ ��
        {
            ThrowBomb();
        }

        // �ܰ� ��Ŭ�� ���� ����
        if (attachedWeapon != null && attachedWeapon.gameObject.name == "Dagger" && Input.GetMouseButtonDown(1))
        {
            ThrowDagger();
        }

        // ���� ��Ŭ�� ���� ����
        if (attachedWeapon != null && attachedWeapon.gameObject.name.Contains("Shotgun") && Input.GetMouseButtonDown(1))
        {
            FireShotgun();
        }

        // If there's any movement, rotate the player to face the direction of movement
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            player.rotation = Quaternion.RotateTowards(player.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void AdjustSpeed(float speedMultiplier, float strafeSpeedMultiplier)
    {
        speed = originalSpeed * speedMultiplier;
        strafeSpeed = originalStrafeSpeed * strafeSpeedMultiplier;
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

        // ��ź�� ��� 4�� �Ŀ� �ڵ����� �����ϵ��� ����
        if (attachedWeapon.gameObject.name == "Bomb")
        {
            Invoke("AutoExplodeBomb", 4f);
        }
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

    void ThrowDagger()
    {
        GameObject dagger = Instantiate(attachedWeapon.gameObject, weaponSocket.position, Quaternion.identity);
        Dagger daggerScript = dagger.GetComponent<Dagger>();

        // �ܰ��� ���ư� ������ �÷��̾��� �� �������� �����մϴ�.
        Vector3 targetDirection = player.forward;
        daggerScript.Initialize(targetDirection);

        // ���� ������ �ܰ��� �����մϴ�.
        Destroy(attachedWeapon.gameObject);
        attachedWeapon = null;
    }

    // ���� �߻� �޼���
    void FireShotgun()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab is not assigned!");
            return;
        }

        int bulletCount = 10; // ���ǿ��� �߻��� �Ѿ��� ����
        float spreadAngle = 20f; // �Ѿ��� ���� ����

        for (int i = 0; i < bulletCount; i++)
        {
            // �Ѿ� ����
            GameObject bullet = Instantiate(bulletPrefab, weaponSocket.position, Quaternion.identity);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            // �Ѿ��� ���ư� ������ ���� ���� ������ �����ϰ� ����
            Vector3 direction = player.forward;
            direction = Quaternion.Euler(UnityEngine.Random.Range(-spreadAngle, spreadAngle), UnityEngine.Random.Range(-spreadAngle, spreadAngle), 0) * direction;

            // �Ѿ��� �� ����
            if (bulletRb != null)
            {
                bulletRb.isKinematic = false;
                bulletRb.detectCollisions = true;
                bulletRb.AddForce(direction * 20f, ForceMode.Impulse); // �Ѿ��� �ӵ� ����
            }

            // ���� �ð� �Ŀ� �Ѿ��� �ı�
            Destroy(bullet, 2f);
        }
    }

    // �߰��� �޼���: AutoExplodeBomb
    void AutoExplodeBomb()
    {
        if (attachedWeapon != null && attachedWeapon.gameObject.name == "Bomb")
        {
            BombExplosion explosion = attachedWeapon.gameObject.AddComponent<BombExplosion>();
            explosion.explosionRadius = 5f;
            explosion.explosionForce = 10f;
            explosion.ExplodeAfterSeconds(0f); // ��� ����
            attachedWeapon = null;
        }
    }

    private void gijal()
    {
        faint = true;
    }

    private void getup()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Punch")
        {
            HP -= 25;
        }
    }
}
