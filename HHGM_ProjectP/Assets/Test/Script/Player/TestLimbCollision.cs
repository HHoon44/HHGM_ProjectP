using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLimbCollision : MonoBehaviour
{
    public TestPlayerController playerController;

    private void Start()
    {
        playerController =
            GameObject.FindObjectOfType<TestPlayerController>().GetComponent<TestPlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerController.isGround = false;
    }
}