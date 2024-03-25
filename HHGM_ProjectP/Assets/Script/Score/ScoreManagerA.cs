using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerA : MonoBehaviour
{
    // 점수를 담을 변수
    public int ScoreA; 
    
    // 점수가 변동될 UI 텍스트 
    public Text UIscoreA;
   

    
    void Update()
    {
        // ScoreA 의 값을 정수로 바꿔서 집어넣음
        UIscoreA.text = ScoreA.ToString();
       
    }

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트의 태그가 아이템일 경우 파괴하고 점수 획득
        if(collision.gameObject.tag == "Item")
        {
            ScoreA += 1;
            Destroy(collision.gameObject);
        }   
    }
}
