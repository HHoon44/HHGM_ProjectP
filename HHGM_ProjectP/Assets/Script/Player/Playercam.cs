using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CamCon
{
    public class Playercam : MonoBehaviour
    {
        // �÷��̾� ��ġ�� ���� ��������
        public Transform target;
        // ī�޶� ��ġ���� ����
        public Vector3 offset;


        void Update()
        {
            // ���� �÷��̾� ��ġ���� offset ��ŭ�� �Ÿ��� ������ä�� ������
            transform.position = target.position + offset;
        }
    }
}