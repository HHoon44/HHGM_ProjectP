using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    public float speed = 20f;
    public float spinSpeed = 720f; // �ܰ� ȸ�� �ӵ� (��/��), ȸ�� �ӵ��� ���ϴ� ��� ����
    public float maxDistance = 20f; // �ܰ��� ���ư� �ִ� �Ÿ�

    private Rigidbody rb; // Rigidbody ������Ʈ
    private Transform parentTransform; // �θ� ������Ʈ�� Transform

    // Dagger�� �ʱ�ȭ�մϴ�.
    public void Initialize(Vector3 direction)
    {
        // Rigidbody ������Ʈ�� �����ɴϴ�.
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            // Rigidbody�� ���ٸ� �߰��մϴ�.
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = false; // Kinematic ��带 �����Ͽ� ���� ������ �޵��� �մϴ�.

        // �ʱ� �ӵ��� �����մϴ�.
        rb.velocity = direction.normalized * speed;

        // �θ� ������Ʈ�� Transform�� �����ɴϴ�.
        parentTransform = transform.parent;

        // Collider ������Ʈ�� �������ų� �߰��մϴ�.
        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<Collider>();
        }
        collider.isTrigger = false; // Ʈ���� ��带 �����Ͽ� ������ �浹�� �����մϴ�.
    }

    void Update()
    {
        // �θ� ������Ʈ�� null�� �ƴ϶�� �ִ� �Ÿ��� �����ߴ��� Ȯ���մϴ�.
        if (parentTransform != null && Vector3.Distance(transform.position, parentTransform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}