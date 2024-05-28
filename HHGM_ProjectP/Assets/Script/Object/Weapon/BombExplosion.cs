using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BombExplosion : MonoBehaviour
{
    public float explosionRadius = 20f; // ���� �ݰ�
    public float explosionForce = 100f; // ���߷� ���� ��

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

            // ���� �ݰ� ���� �÷��̾�� �������� �ݴϴ�.
            PlayerController playerController = col.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.HP -= 100;
            }
        }

        // �ڽ��� �ı��մϴ�.
        Destroy(gameObject);
    }
}
