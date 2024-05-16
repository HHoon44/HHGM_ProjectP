using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BombExplosion : MonoBehaviour
{
    public float explosionRadius = 10f; // ���� �ݰ�
    public float explosionForce = 20f; // ���߷� ���� ��

    public void ExplodeAfterSeconds(float seconds)
    {
        Invoke("Explode", seconds);
    }

    void Explode()
    {
        // �ֺ��� Collider�� �����ɴϴ�.
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // ���߷� ���� ���� �־� ������Ʈ�� �����ϴ�.
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        // �ڽ��� �ı��մϴ�.
        Destroy(gameObject);
    }
}
