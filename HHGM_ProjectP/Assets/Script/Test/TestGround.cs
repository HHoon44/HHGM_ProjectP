using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGround : MonoBehaviour
{
    public int count;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("�÷��̾� �߶�");

            count++;
        }
    }
}