using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerB : MonoBehaviour
{
    // ������ ���� ����
    public int ScoreB;

    // ������ ������ UI �ؽ�Ʈ 
    public Text UIscoreB;
   


    
    void Update()
    {   // ScoreB �� ���� ������ �ٲ㼭 �������
        UIscoreB.text = ScoreB.ToString();
       
    }

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� �±װ� �������� ��� �ı��ϰ� ���� ȹ��
        if (collision.gameObject.tag == "Item")
        {
            ScoreB += 1;
            Destroy(collision.gameObject);
        }   
    }
}
