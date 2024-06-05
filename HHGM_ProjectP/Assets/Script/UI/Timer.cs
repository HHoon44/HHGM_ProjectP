using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeText;
    public Text scoreText1; // ù ��° ���� �ؽ�Ʈ
    public Text scoreText2; // �� ��° ���� �ؽ�Ʈ
    public Image player1WinsImage; // �÷��̾� 1 �¸� �̹���
    public Image player2WinsImage; // �÷��̾� 2 �¸� �̹���
    public Image tieImage; // ���� �̹���

    void Start()
    {
        StartCoroutine(Timeset(15.0f)); // Ÿ�̸� ����
    }

    void Update()
    {
        // �ʿ� �� �ٸ� ������Ʈ ������ �߰��� �� �ֽ��ϴ�.
    }

    IEnumerator Timeset(float time)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            timeText.text = "Time : " + time.ToString("F1"); // �Ҽ��� ù° �ڸ����� ǥ��
            yield return null;
        }

        time = 0;
        timeText.text = "Time : " + time.ToString("F1");
        CompareScores();
    }

    void CompareScores()
    {
        // �ؽ�Ʈ���� ������ �Ľ�
        int score1 = int.Parse(scoreText1.text);
        int score2 = int.Parse(scoreText2.text);

        // �̹��� �ʱ�ȭ (��� �̹����� ��Ȱ��ȭ)
        player1WinsImage.gameObject.SetActive(false);
        player2WinsImage.gameObject.SetActive(false);
        tieImage.gameObject.SetActive(false);

        // ������ ���ϰ� ������ �̹����� Ȱ��ȭ
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
