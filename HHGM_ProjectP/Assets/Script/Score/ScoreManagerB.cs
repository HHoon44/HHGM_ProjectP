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
        if (collision.gameObject.tag == "Item")
        {

            Debug.Log("아이템 충돌감지");

            // Destroy(collision.gameObject);

            if (collision.gameObject.name == "Large_Score")
            {
                ScoreB += 3;
            }
            else if (collision.gameObject.name == "Middle_Score")
            {
                ScoreB += 2;
            }
            else
            {
                ScoreB += 1;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {

            Debug.Log("아이템 충돌감지");

            // Destroy(collision.gameObject);

            if (collision.gameObject.name == "Large_Score")
            {
                ScoreB -= 3;
            }
            else if (collision.gameObject.name == "Middle_Score")
            {
                ScoreB -= 2;
            }
            else
            {
                ScoreB -= 1;
            }
        }
    }
}
