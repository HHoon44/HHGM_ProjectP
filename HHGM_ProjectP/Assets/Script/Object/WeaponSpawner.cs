using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI�� �����ϱ� ���� �߰�

public class WeaponSpawner : MonoBehaviour
{
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon5;
    public GameObject weapon6;

    public GameObject specialWeapon1; // 180�� �� ������ Ư�� ���� 1
    public GameObject specialWeapon2; // 180�� �� ������ Ư�� ���� 2
    public GameObject specialWeapon3; // 180�� �� ������ Ư�� ���� 3
    public GameObject specialWeapon4; // 180�� �� ������ Ư�� ���� 4

    public Image specialWeaponImage1; // 180�� �� Ȱ��ȭ�� UI �̹��� 1
    public Image specialWeaponImage2; // 180�� �� Ȱ��ȭ�� UI �̹��� 2
    public Image specialWeaponImage3; // 180�� �� Ȱ��ȭ�� UI �̹��� 3
    public Image specialWeaponImage4; // 180�� �� Ȱ��ȭ�� UI �̹��� 4

    public Transform[] spawnPoints; // ���⸦ ������ ��ġ �迭

    // �� �����ۿ� ���� Ȯ�� (0�� 1 ������ ������ ���� 1�� �Ǿ�� �մϴ�)
    private float[] probabilities = { 0.2f, 0.2f, 0.2f, 0.15f, 0.15f, 0.1f };

    // �̺�Ʈ Ȯ�� (4���� �̺�Ʈ, ���� 1�� �Ǿ�� �մϴ�)
    private float[] eventProbabilities = { 0.25f, 0.25f, 0.25f, 0.25f };

    private bool isSpecialWeaponTime = false;
    private int selectedEvent = -1;

    void Start()
    {
        StartCoroutine(SpawnWeapon());
        StartCoroutine(ActivateSpecialEvent());
    }

    IEnumerator SpawnWeapon()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                GameObject selectedItem = null;

                if (isSpecialWeaponTime)
                {
                    switch (selectedEvent)
                    {
                        case 0:
                            selectedItem = specialWeapon1;
                            break;
                        case 1:
                            selectedItem = specialWeapon2;
                            break;
                        case 2:
                            selectedItem = specialWeapon3;
                            break;
                        case 3:
                            selectedItem = specialWeapon4;
                            break;
                    }
                }
                else
                {
                    float randomValue = Random.value;
                    if (randomValue < probabilities[0])
                    {
                        selectedItem = weapon1;
                    }
                    else if (randomValue < probabilities[0] + probabilities[1])
                    {
                        selectedItem = weapon2;
                    }
                    else if (randomValue < probabilities[0] + probabilities[1] + probabilities[2])
                    {
                        selectedItem = weapon3;
                    }
                    else if (randomValue < probabilities[0] + probabilities[1] + probabilities[2] + probabilities[3])
                    {
                        selectedItem = weapon4;
                    }
                    else if (randomValue < probabilities[0] + probabilities[1] + probabilities[2] + probabilities[3] + probabilities[4])
                    {
                        selectedItem = weapon5;
                    }
                    else
                    {
                        selectedItem = weapon6;
                    }
                }

                // ���⸦ �ش� ��ġ�� ����
                Instantiate(selectedItem, spawnPoints[i].position, Quaternion.identity);
            }
        }
    }

    IEnumerator ActivateSpecialEvent()
    {
        yield return new WaitForSeconds(180f); // 180�� ��ٸ���

        // Ȯ���� ���� �̺�Ʈ ����
        float randomValue = Random.value;
        GameObject specialWeapon = null;
        Image specialWeaponImage = null;

        if (randomValue < eventProbabilities[0])
        {
            selectedEvent = 0;
            specialWeapon = specialWeapon1;
            specialWeaponImage = specialWeaponImage1;
        }
        else if (randomValue < eventProbabilities[0] + eventProbabilities[1])
        {
            selectedEvent = 1;
            specialWeapon = specialWeapon2;
            specialWeaponImage = specialWeaponImage2;
        }
        else if (randomValue < eventProbabilities[0] + eventProbabilities[1] + eventProbabilities[2])
        {
            selectedEvent = 2;
            specialWeapon = specialWeapon3;
            specialWeaponImage = specialWeaponImage3;
        }
        else
        {
            selectedEvent = 3;
            specialWeapon = specialWeapon4;
            specialWeaponImage = specialWeaponImage4;
        }

        isSpecialWeaponTime = true;
        specialWeaponImage.gameObject.SetActive(true); // UI �̹��� Ȱ��ȭ

        // ��� ���⸦ 4���� ����
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(specialWeapon, spawnPoints[i].position, Quaternion.identity);
        }
    }
}
