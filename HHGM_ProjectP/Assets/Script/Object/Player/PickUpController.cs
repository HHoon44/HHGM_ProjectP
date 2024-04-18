using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public float pickupRange = 2f; // ���⸦ �ֿ� �� �ִ� �ִ� �Ÿ�
    public KeyCode dropKey = KeyCode.Mouse0; // ���⸦ ����߸��� Ű ����
    public Transform handTransform; // ���⸦ �� ���� ��ġ

    private Transform player; // �÷��̾��� ��ġ
    private bool isCarried = false; // ���⸦ ������ �ִ��� ����

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // ���⸦ �ֿ� �� �ִ� �Ÿ��� �ְ�, ���� ���⸦ ������ ���� ���� ���
        if (!isCarried && Vector3.Distance(transform.position, player.position) <= pickupRange)
        {
            // �÷��̾ ���콺 Ŭ�� �Ǵ� dropKey�� ������ ���⸦ �ݽ��ϴ�.
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(dropKey))
            {
                transform.SetParent(player); // ���⸦ �÷��̾��� �ڽ����� �����Ͽ� ��� ����ϴ�.
                transform.localPosition = handTransform.localPosition; // ������ ��ġ�� �տ� �°� �����մϴ�.
                transform.localRotation = handTransform.localRotation; // ������ ȸ���� �տ� �°� �����մϴ�.
                isCarried = true; // ���⸦ ������ �ִ� ���·� �����մϴ�.
            }
        }
        // ���⸦ ������ �ִ� ���
        else if (isCarried)
        {
            // �÷��̾ ���콺 Ŭ�� �Ǵ� dropKey�� ������ ���⸦ ����߸��ϴ�.
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(dropKey))
            {
                transform.SetParent(null); // ������ �θ� �ʱ�ȭ�Ͽ� ���⸦ ����߸��ϴ�.
                isCarried = false; // ���⸦ ������ ���� ���� ���·� �����մϴ�.
            }
        }
    }
}