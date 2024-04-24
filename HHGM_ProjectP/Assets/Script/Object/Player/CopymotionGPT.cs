using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class CopymotionGPT : MonoBehaviour
{
    public Transform targetLimb; // �ִϸ��̼��� ���� �κ��� Transform
    public bool mirror; // �ſ� ��� ���� (���ʰ� �������� ��Ī�� ��� ���)

    private ConfigurableJoint cj;

    private void Awake()
    {
        cj = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        // �ſ� ��尡 �ƴ� ���
        if (!mirror)
        {
            cj.targetRotation = targetLimb.rotation;
        }
        // �ſ� ����� ���
        else
        {
            // �ſ� ��忡���� �����ʰ� ������ ��Ī�̹Ƿ� y�� ȸ���� ������ �����Ͽ� ����
            Vector3 targetRotEuler = targetLimb.rotation.eulerAngles;
            targetRotEuler.y = -targetRotEuler.y;
            Quaternion mirroredRotation = Quaternion.Euler(targetRotEuler);
            cj.targetRotation = mirroredRotation;
        }
    }
}