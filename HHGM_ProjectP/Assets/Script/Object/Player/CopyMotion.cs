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
        // JointDrive 설정을 통해 부드러운 움직임을 설정
        JointDrive drive = new JointDrive();
        drive.positionSpring = 500f; // 이 값을 조정하여 더 자연스러운 움직임을 얻습니다.
        drive.positionDamper = 100f; // 이 값을 조정하여 감쇠를 제어합니다.
        drive.maximumForce = 1000f;

        cj.angularXDrive = drive;
        cj.angularYZDrive = drive;

        // 관절 회전 한계 설정
        SoftJointLimit angularXLimit = new SoftJointLimit();
        angularXLimit.limit = 45f;
        cj.lowAngularXLimit = angularXLimit;
        cj.highAngularXLimit = angularXLimit;

        SoftJointLimit angularYZLimit = new SoftJointLimit();
        angularYZLimit.limit = 45f;
        cj.angularYLimit = angularYZLimit;
        cj.angularZLimit = angularYZLimit;

        // JointLimitSpring 설정
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
            // targetLimb의 로컬 회전을 기준으로 targetRotation 계산
            Quaternion targetRotation = Quaternion.Inverse(targetInitialLocalRotation) * targetLimb.localRotation;

            if (mirror)
            {
                // 미러 모드에서 회전을 반대로 설정 (x축과 z축을 반전)
                targetRotation = new Quaternion(-targetRotation.x, targetRotation.y, -targetRotation.z, targetRotation.w);
            }

            // 초기 로컬 회전을 고려하여 targetRotation 설정
            cj.targetRotation = Quaternion.Inverse(initialLocalRotation) * targetRotation;
        }
    }
}