using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeText;
    public Text scoreText1; // 첫 번째 점수 텍스트
    public Text scoreText2; // 두 번째 점수 텍스트
    public Image player1WinsImage; // 플레이어 1 승리 이미지
    public Image player2WinsImage; // 플레이어 2 승리 이미지
    public Image tieImage; // 동점 이미지

    void Start()
    {
        StartCoroutine(Timeset(15.0f)); // 타이머 시작
    }

    void Update()
    {
        // 필요 시 다른 업데이트 로직을 추가할 수 있습니다.
    }

    IEnumerator Timeset(float time)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            timeText.text = "Time : " + time.ToString("F1"); // 소수점 첫째 자리까지 표기
            yield return null;
        }

        time = 0;
        timeText.text = "Time : " + time.ToString("F1");
        CompareScores();
    }

    void CompareScores()
    {
        // 텍스트에서 점수를 파싱
        int score1 = int.Parse(scoreText1.text);
        int score2 = int.Parse(scoreText2.text);

        // 이미지 초기화 (모든 이미지를 비활성화)
        player1WinsImage.gameObject.SetActive(false);
        player2WinsImage.gameObject.SetActive(false);
        tieImage.gameObject.SetActive(false);

        // 점수를 비교하고 적절한 이미지를 활성화
        if (score1 > score2)
        {
            player1WinsImage.gameObject.SetActive(true);
        }
        else if (score2 > score1)
        {
            player2WinsImage.gameObject.SetActive(true);
        }
        else
        {
            tieImage.gameObject.SetActive(true);
        }
    }
}
