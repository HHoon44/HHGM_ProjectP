using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newcontrol : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject bulletPrefab; // 총알 프리팹

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
        // 원래 속도를 저장
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
            if (attachedWeapon == null) // 무기가 장착되어 있지 않은 경우
            {
                // 플레이어 주변의 모든 무기를 검사하여 가장 가까운 무기를 찾습니다.
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

                // 가장 가까운 무기를 찾은 경우 해당 무기를 장착합니다.
                if (closestCollider != null)
                {
                    AttachWeapon(closestCollider.transform);
                }
            }
            else // 무기가 이미 장착되어 있는 경우
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

        if (attachedWeapon != null && attachedWeapon.gameObject.name == "Bomb" && Input.GetMouseButtonDown(1)) // 폭탄이 장착되어 있고 우클릭을 눌렀을 때
        {
            ThrowBomb();
        }

        // 단검 우클릭 감지 로직
        if (attachedWeapon != null && attachedWeapon.gameObject.name == "Dagger" && Input.GetMouseButtonDown(1))
        {
            ThrowDagger();
        }

        // 샷건 우클릭 감지 로직
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
        // 무기의 Transform을 장착할 부위의 Transform에 할당합니다.
        weaponTransform.parent = weaponSocket;
        weaponTransform.localPosition = Vector3.zero;
        weaponTransform.localRotation = Quaternion.identity;
        weaponTransform.localScale = Vector3.one;

        // 무기가 플레이어의 앞 방향을 바라보도록 회전합니다.
        Vector3 direction = player.forward;
        Quaternion rotation = Quaternion.LookRotation(direction);
        weaponTransform.rotation = rotation;

        // Rigidbody 비활성화
        Rigidbody weaponRb = weaponTransform.GetComponent<Rigidbody>();
        if (weaponRb != null)
        {
            weaponRb.isKinematic = true;
            weaponRb.detectCollisions = false;
        }

        // 현재 장착된 무기를 저장합니다.
        attachedWeapon = weaponTransform;

        // 폭탄의 경우 4초 후에 자동으로 폭발하도록 설정
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
                // 폭탄을 장착 해제하고 파괴합니다.
                Destroy(attachedWeapon.gameObject);
            }
            else
            {
                // 다른 무기를 장착 해제합니다.
                attachedWeapon.parent = null;

                // 무기의 위치를 장착 위치의 바로 아래로 설정합니다.
                attachedWeapon.position = weaponSocket.position - new Vector3(0, 0.5f, 0);

                // Rigidbody 활성화
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
        // 폭탄을 생성하고 throwPoint 위치에 던집니다.
        GameObject bomb = Instantiate(attachedWeapon.gameObject, weaponSocket.position, Quaternion.identity);
        Rigidbody bombRb = bomb.GetComponent<Rigidbody>();

        // Rigidbody 활성화
        if (bombRb != null)
        {
            bombRb.isKinematic = false;
            bombRb.detectCollisions = true;
            bombRb.AddForce(player.forward * 10f, ForceMode.Impulse);
        }

        // 일정 시간 후에 폭탄을 폭발시킵니다.
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

        // 단검이 날아갈 방향은 플레이어의 앞 방향으로 설정합니다.
        Vector3 targetDirection = player.forward;
        daggerScript.Initialize(targetDirection);

        // 현재 장착된 단검을 제거합니다.
        Destroy(attachedWeapon.gameObject);
        attachedWeapon = null;
    }

    // 샷건 발사 메서드
    void FireShotgun()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab is not assigned!");
            return;
        }

        int bulletCount = 10; // 샷건에서 발사할 총알의 개수
        float spreadAngle = 20f; // 총알의 퍼짐 각도

        for (int i = 0; i < bulletCount; i++)
        {
            // 총알 생성
            GameObject bullet = Instantiate(bulletPrefab, weaponSocket.position, Quaternion.identity);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            // 총알이 날아갈 방향을 퍼짐 각도 내에서 랜덤하게 설정
            Vector3 direction = player.forward;
            direction = Quaternion.Euler(UnityEngine.Random.Range(-spreadAngle, spreadAngle), UnityEngine.Random.Range(-spreadAngle, spreadAngle), 0) * direction;

            // 총알의 힘 설정
            if (bulletRb != null)
            {
                bulletRb.isKinematic = false;
                bulletRb.detectCollisions = true;
                bulletRb.AddForce(direction * 20f, ForceMode.Impulse); // 총알의 속도 설정
            }

            // 일정 시간 후에 총알을 파괴
            Destroy(bullet, 2f);
        }
    }

    // 추가할 메서드: AutoExplodeBomb
    void AutoExplodeBomb()
    {
        if (attachedWeapon != null && attachedWeapon.gameObject.name == "Bomb")
        {
            BombExplosion explosion = attachedWeapon.gameObject.AddComponent<BombExplosion>();
            explosion.explosionRadius = 5f;
            explosion.explosionForce = 10f;
            explosion.ExplodeAfterSeconds(0f); // 즉시 폭발
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
