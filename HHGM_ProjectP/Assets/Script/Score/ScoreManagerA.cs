using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerA : MonoBehaviour
{
    public int ScoreA;
    public Text ScoreTextA;

    void Update()
    {
        ScoreTextA.text = ScoreA.ToString();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("������ �浹����");

            // ������Ʈ �̸��� Ư�� ���ڿ��� ���ԵǾ� �ִ��� Ȯ��
            if (collision.gameObject.name.Contains("Large_Score"))
            {
                ScoreA += 3;
            }
            else if (collision.gameObject.name.Contains("Middle_Score"))
            {
                ScoreA += 2;
            }
            else
            {
                ScoreA += 1;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("������ �浹����");

            // ������Ʈ �̸��� Ư�� ���ڿ��� ���ԵǾ� �ִ��� Ȯ��
            if (collision.gameObject.name.Contains("Large_Score"))
            {
                ScoreA -= 3;
            }
            else if (collision.gameObject.name.Contains("Middle_Score"))
            {
                ScoreA -= 2;
            }
            else
            {
                ScoreA -= 1;
            }
        }
    }
}
