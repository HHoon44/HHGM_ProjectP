using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class CopymotionGPT : MonoBehaviour
{
    public Transform targetLimb; // 애니메이션을 따라갈 부분의 Transform
    public bool mirror; // 거울 모드 여부 (왼쪽과 오른쪽이 대칭인 경우 사용)

    private ConfigurableJoint cj;

    private void Awake()
    {
        cj = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        // 거울 모드가 아닌 경우
        if (!mirror)
        {
            cj.targetRotation = targetLimb.rotation;
        }
        // 거울 모드인 경우
        else
        {
            // 거울 모드에서는 오른쪽과 왼쪽이 대칭이므로 y축 회전의 방향을 반전하여 적용
            Vector3 targetRotEuler = targetLimb.rotation.eulerAngles;
            targetRotEuler.y = -targetRotEuler.y;
            Quaternion mirroredRotation = Quaternion.Euler(targetRotEuler);
            cj.targetRotation = mirroredRotation;
        }
    }
}