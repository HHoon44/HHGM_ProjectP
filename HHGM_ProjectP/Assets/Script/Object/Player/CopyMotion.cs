using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    public Transform targetLimb;
    public bool mirror;

    private ConfigurableJoint cj;
    private Quaternion initialLocalRotation;
    private Quaternion targetInitialLocalRotation;

    private void Awake()
    {
        cj = GetComponent<ConfigurableJoint>();
        initialLocalRotation = transform.localRotation;
        targetInitialLocalRotation = targetLimb.localRotation;
    }

    private void Start()
    {
        // JointDrive ������ ���� �ε巯�� �������� ����
        JointDrive drive = new JointDrive();
        drive.positionSpring = 500f; // �� ���� �����Ͽ� �� �ڿ������� �������� ����ϴ�.
        drive.positionDamper = 100f; // �� ���� �����Ͽ� ���踦 �����մϴ�.
        drive.maximumForce = 1000f;

        cj.angularXDrive = drive;
        cj.angularYZDrive = drive;

        // ���� ȸ�� �Ѱ� ����
        SoftJointLimit angularXLimit = new SoftJointLimit();
        angularXLimit.limit = 45f;
        cj.lowAngularXLimit = angularXLimit;
        cj.highAngularXLimit = angularXLimit;

        SoftJointLimit angularYZLimit = new SoftJointLimit();
        angularYZLimit.limit = 45f;
        cj.angularYLimit = angularYZLimit;
        cj.angularZLimit = angularYZLimit;

        // JointLimitSpring ����
        SoftJointLimitSpring angularLimitSpring = new SoftJointLimitSpring();
        angularLimitSpring.spring = 500f;
        angularLimitSpring.damper = 100f;

        cj.angularXLimitSpring = angularLimitSpring;
        cj.angularYZLimitSpring = angularLimitSpring;
    }

    private void LateUpdate()
    {
        if (targetLimb != null)
        {
            // targetLimb�� ���� ȸ���� �������� targetRotation ���
            Quaternion targetRotation = Quaternion.Inverse(targetInitialLocalRotation) * targetLimb.localRotation;

            if (mirror)
            {
                // �̷� ��忡�� ȸ���� �ݴ�� ���� (x��� z���� ����)
                targetRotation = new Quaternion(-targetRotation.x, targetRotation.y, -targetRotation.z, targetRotation.w);
            }

            // �ʱ� ���� ȸ���� ����Ͽ� targetRotation ����
            cj.targetRotation = Quaternion.Inverse(initialLocalRotation) * targetRotation;
        }
    }
}