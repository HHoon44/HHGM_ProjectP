using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerB : MonoBehaviour
{
    public int ScoreB;
    public Text ScoreTextB;


    void Update()
    {
        ScoreTextB.text = ScoreB.ToString();
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            Debug.Log("������ �浹����");
            ScoreB += 1;           
            Destroy(collision.gameObject);
 
        }
    }
}
