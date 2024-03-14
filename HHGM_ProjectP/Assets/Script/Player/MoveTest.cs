using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerMove { 
public class MoveTest : MonoBehaviour
{
    // 플레이어 스피드 변수와 좌표값 넣을 변수 생성
    public float speed;
    float hAxis;
    float vAxis;

    Vector3 moveVec;

   

    // Update is called once per frame
    void Update()
    {
        // 키보드입력으로 좌표값에 넣을 값 입력
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        // 벡터변수에 입력된 값 넣기
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        //입력된 만큼 이동
        transform.position += moveVec * speed * Time.deltaTime;
        //방향에따라 모델 회전
        transform.LookAt(transform.position + moveVec);
    }
}
}
