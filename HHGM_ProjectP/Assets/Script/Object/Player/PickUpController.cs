using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public float pickupRange = 2f; // 무기를 주울 수 있는 최대 거리
    public KeyCode dropKey = KeyCode.Mouse0; // 무기를 떨어뜨리는 키 설정
    public Transform handTransform; // 무기를 들 손의 위치

    private Transform player; // 플레이어의 위치
    private bool isCarried = false; // 무기를 가지고 있는지 여부

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // 무기를 주울 수 있는 거리에 있고, 아직 무기를 가지고 있지 않은 경우
        if (!isCarried && Vector3.Distance(transform.position, player.position) <= pickupRange)
        {
            // 플레이어가 마우스 클릭 또는 dropKey를 누르면 무기를 줍습니다.
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(dropKey))
            {
                transform.SetParent(player); // 무기를 플레이어의 자식으로 설정하여 들게 만듭니다.
                transform.localPosition = handTransform.localPosition; // 무기의 위치를 손에 맞게 설정합니다.
                transform.localRotation = handTransform.localRotation; // 무기의 회전을 손에 맞게 설정합니다.
                isCarried = true; // 무기를 가지고 있는 상태로 변경합니다.
            }
        }
        // 무기를 가지고 있는 경우
        else if (isCarried)
        {
            // 플레이어가 마우스 클릭 또는 dropKey를 누르면 무기를 떨어뜨립니다.
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(dropKey))
            {
                transform.SetParent(null); // 무기의 부모를 초기화하여 무기를 떨어뜨립니다.
                isCarried = false; // 무기를 가지고 있지 않은 상태로 변경합니다.
            }
        }
    }
}