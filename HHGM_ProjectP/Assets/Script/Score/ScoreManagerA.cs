using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerA : MonoBehaviour
{
    // ������ ���� ����
    public int ScoreA; 
    
    // ������ ������ UI �ؽ�Ʈ 
    public Text UIscoreA;
   

    
    void Update()
    {
        // ScoreA �� ���� ������ �ٲ㼭 �������
        UIscoreA.text = ScoreA.ToString();
       
    }

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� �±װ� �������� ��� �ı��ϰ� ���� ȹ��
        if(collision.gameObject.tag == "Item")
        {
            ScoreA += 1;
            Destroy(collision.gameObject);
        }   
    }
}
