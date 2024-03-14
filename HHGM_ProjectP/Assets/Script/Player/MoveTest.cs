using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerMove { 
public class MoveTest : MonoBehaviour
{
    // �÷��̾� ���ǵ� ������ ��ǥ�� ���� ���� ����
    public float speed;
    float hAxis;
    float vAxis;

    Vector3 moveVec;

   

    // Update is called once per frame
    void Update()
    {
        // Ű�����Է����� ��ǥ���� ���� �� �Է�
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        // ���ͺ����� �Էµ� �� �ֱ�
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        //�Էµ� ��ŭ �̵�
        transform.position += moveVec * speed * Time.deltaTime;
        //���⿡���� �� ȸ��
        transform.LookAt(transform.position + moveVec);
    }
}
}
