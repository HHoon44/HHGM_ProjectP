using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limbcollision : MonoBehaviour
{
    //public PlayerCont playerCont;

    private void Start()
    {
       // playerCont = GameObject.FindObjectOfType<PlayerCont>().GetComponent<PlayerCont>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //  playerCont.isGrounded = true;

    }
}
