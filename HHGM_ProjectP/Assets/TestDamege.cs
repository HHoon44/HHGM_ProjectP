using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestDamege : MonoBehaviour
{
    public int AttackR = 0;
    public int AttackL = 0;

    public Collider Rpunch;
    public Collider Lpunch;

    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void DealDamageR()
    {
        
       if(AttackR == 0)
        {
            Debug.Log("������ �������� �ݴϴ�.");
            AttackR = 1;
           
            Rpunch.enabled = true;
            Debug.Log("�����ָ� Ȱ��ȭ");

        }
       if(AttackR == 1)
        {
            Debug.Log("�����ʰ��ݳ�");
            AttackR = 0;
            Rpunch.enabled = false;
            Debug.Log("�����ָ� ��Ȱ��");
        }


    }

    public void DealDamageL()
    {
        if (AttackL == 0)
        {
            Debug.Log("���� �������� �ݴϴ�.");
            AttackL = 1;
            
            Lpunch.enabled = true;
            Debug.Log("���ָ� Ȱ��ȭ");
        }
        if (AttackL == 1)
        {
            Debug.Log("���ʰ��ݳ�");
            AttackL = 0;
            Lpunch.enabled = false;
            Debug.Log("���ָ� ��Ȱ��");
        }
    }
}
