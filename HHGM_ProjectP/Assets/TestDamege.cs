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
            Debug.Log("오른쪽 데미지를 줍니다.");
            AttackR = 1;
           
            Rpunch.enabled = true;
            Debug.Log("오른주먹 활성화");

        }
       if(AttackR == 1)
        {
            Debug.Log("오른쪽공격끝");
            AttackR = 0;
            Rpunch.enabled = false;
            Debug.Log("오른주먹 비활성");
        }


    }

    public void DealDamageL()
    {
        if (AttackL == 0)
        {
            Debug.Log("왼쪽 데미지를 줍니다.");
            AttackL = 1;
            
            Lpunch.enabled = true;
            Debug.Log("왼주먹 활성화");
        }
        if (AttackL == 1)
        {
            Debug.Log("왼쪽공격끝");
            AttackL = 0;
            Lpunch.enabled = false;
            Debug.Log("왼주먹 비활성");
        }
    }
}
