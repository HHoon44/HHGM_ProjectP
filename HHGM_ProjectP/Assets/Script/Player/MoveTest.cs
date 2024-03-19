using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectP.Object
{
    public class MoveTest : MonoBehaviour
    {
        // �÷��̾� ���ǵ� ������ ��ǥ�� ���� ���� ����
        public float speed;

        private float hAxis;
        private float vAxis;

        private Vector3 moveVec;

        private void Update()
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
