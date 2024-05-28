using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    public float speed = 20f;
    public float spinSpeed = 720f; // 단검 회전 속도 (도/초), 회전 속도를 원하는 대로 조정
    public float maxDistance = 20f; // 단검이 날아갈 최대 거리

    private Rigidbody rb; // Rigidbody 컴포넌트
    private Transform parentTransform; // 부모 오브젝트의 Transform

    // Dagger를 초기화합니다.
    public void Initialize(Vector3 direction)
    {
        // Rigidbody 컴포넌트를 가져옵니다.
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            // Rigidbody가 없다면 추가합니다.
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = false; // Kinematic 모드를 해제하여 물리 영향을 받도록 합니다.

        // 초기 속도를 설정합니다.
        rb.velocity = direction.normalized * speed;

        // 부모 오브젝트의 Transform을 가져옵니다.
        parentTransform = transform.parent;

        // Collider 컴포넌트를 가져오거나 추가합니다.
        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<Collider>();
        }
        collider.isTrigger = false; // 트리거 모드를 해제하여 물리적 충돌을 감지합니다.
    }

    void Update()
    {
        // 부모 오브젝트가 null이 아니라면 최대 거리에 도달했는지 확인합니다.
        if (parentTransform != null && Vector3.Distance(transform.position, parentTransform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}