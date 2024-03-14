using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CamCon
{
    public class Playercam : MonoBehaviour
    {
        // 플레이어 위치를 넣을 공개변수
        public Transform target;
        // 카메라 위치넣을 변수
        public Vector3 offset;


        void Update()
        {
            // 현재 플레이어 위치에서 offset 만큼의 거리가 떨어진채로 고정됨
            transform.position = target.position + offset;
        }
    }
}