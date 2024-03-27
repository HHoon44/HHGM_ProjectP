using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerA : MonoBehaviour
{
    public int ScoreA;
    public Text ScoreTextA;

    
    void Update()
    {
        ScoreTextA.text = ScoreA.ToString();
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            Debug.Log("아이템 충돌감지");
            ScoreA += 1;           
            Destroy(collision.gameObject);
 
        }
    }
}
