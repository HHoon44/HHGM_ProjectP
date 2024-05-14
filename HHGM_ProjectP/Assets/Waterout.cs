using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterout : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);

        }
    }
}
