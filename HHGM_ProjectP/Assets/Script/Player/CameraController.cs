using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectP.Object
{
    /// <summary>
    /// 인 게임 카메라를 다스리는 클래스
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        // 회전속도
        public float rotationSpeed = 1f;

        private float mouseX;
        private float mouseY;

        // 회전시킬 오브젝 트
        public Transform root;

        public float stomachOffset;

        public ConfigurableJoint hipJoint;
        public ConfigurableJoint stomachJoint;

        private void Start()
        {
            // 마우스 커서를 잠구기 위한 작업
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void FixedUpdate()
        {
            CamControl();
        }

        private void CamControl()
        {
            /// Mathf.Clamp : 최대 최소값 안에서 하나의 값을 선정

            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            mouseY = Mathf.Clamp(mouseY, -35, 60);

            Quaternion rootRotation = Quaternion.Euler(mouseY, mouseX, 0);

            // 새로운 회전값을 회전시킬 오브젝트에 적용하는 작업
            root.rotation = rootRotation;

            hipJoint.targetRotation = Quaternion.Euler(0, -mouseX, 0);
            stomachJoint.targetRotation = Quaternion.Euler(-mouseY + stomachOffset, 0, 0);
        }
    }
}