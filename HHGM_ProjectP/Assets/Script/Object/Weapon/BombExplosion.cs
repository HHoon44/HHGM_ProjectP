using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BombExplosion : MonoBehaviour
{
    public float explosionRadius = 10f; // 폭발 반경
    public float explosionForce = 20f; // 폭발로 인한 힘

    public void ExplodeAfterSeconds(float seconds)
    {
        Invoke("Explode", seconds);
    }

    void Explode()
    {
        // 주변의 Collider를 가져옵니다.
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 폭발로 인한 힘을 주어 오브젝트를 날립니다.
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        // 자신을 파괴합니다.
        Destroy(gameObject);
    }
}
