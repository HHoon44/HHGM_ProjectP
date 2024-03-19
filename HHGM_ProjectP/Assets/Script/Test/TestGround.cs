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
            Debug.Log("플레이어 추락");

            count++;
        }
    }
}