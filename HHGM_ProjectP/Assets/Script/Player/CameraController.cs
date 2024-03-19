using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectP.Object
{
    /// <summary>
    /// �� ���� ī�޶� �ٽ����� Ŭ����
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        // ȸ���ӵ�
        public float rotationSpeed = 1f;

        private float mouseX;
        private float mouseY;

        // ȸ����ų ������ Ʈ
        public Transform root;

        public float stomachOffset;

        public ConfigurableJoint hipJoint;
        public ConfigurableJoint stomachJoint;

        private void Start()
        {
            // ���콺 Ŀ���� �ᱸ�� ���� �۾�
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void FixedUpdate()
        {
            CamControl();
        }

        private void CamControl()
        {
            /// Mathf.Clamp : �ִ� �ּҰ� �ȿ��� �ϳ��� ���� ����

            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            mouseY = Mathf.Clamp(mouseY, -35, 60);

            Quaternion rootRotation = Quaternion.Euler(mouseY, mouseX, 0);

            // ���ο� ȸ������ ȸ����ų ������Ʈ�� �����ϴ� �۾�
            root.rotation = rootRotation;

            hipJoint.targetRotation = Quaternion.Euler(0, -mouseX, 0);
            stomachJoint.targetRotation = Quaternion.Euler(-mouseY + stomachOffset, 0, 0);
        }
    }
}