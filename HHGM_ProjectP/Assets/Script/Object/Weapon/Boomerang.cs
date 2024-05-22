using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float speed = 10f;
    public float returnTime = 2f;
    private Vector3 targetPosition;
    private Transform playerTransform;
    private bool returning = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;  // 초기화 시 부메랑의 물리 영향을 비활성화
    }

    public void Initialize(Vector3 target, Transform player)
    {
        targetPosition = target;
        playerTransform = player;
        returning = false;
        rb.isKinematic = false;  // 던질 때 물리 영향을 활성화
        StartCoroutine(ReturnToPlayer());
    }

    void Update()
    {
        if (!returning)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, playerTransform.position) < 1f)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator ReturnToPlayer()
    {
        yield return new WaitForSeconds(returnTime);
        returning = true;
    }
}