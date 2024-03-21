using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestField : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var parent = other.transform.parent;

            Debug.Log(parent.name);

            Destroy(parent);
        }
    }
}