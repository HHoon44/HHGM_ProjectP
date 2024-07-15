using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI를 제어하기 위해 추가

public class WeaponSpawner : MonoBehaviour
{
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon5;
    public GameObject weapon6;

    public GameObject specialWeapon1; // 180초 후 생성할 특정 무기 1
    public GameObject specialWeapon2; // 180초 후 생성할 특정 무기 2
    public GameObject specialWeapon3; // 180초 후 생성할 특정 무기 3
    public GameObject specialWeapon4; // 180초 후 생성할 특정 무기 4

    public Image specialWeaponImage1; // 180초 후 활성화할 UI 이미지 1
    public Image specialWeaponImage2; // 180초 후 활성화할 UI 이미지 2
    public Image specialWeaponImage3; // 180초 후 활성화할 UI 이미지 3
    public Image specialWeaponImage4; // 180초 후 활성화할 UI 이미지 4

    public Transform[] spawnPoints; // 무기를 스폰할 위치 배열

    // 각 아이템에 대한 확률 (0과 1 사이의 값으로 합이 1이 되어야 합니다)
    private float[] probabilities = { 0.2f, 0.2f, 0.2f, 0.15f, 0.15f, 0.1f };

    // 이벤트 확률 (4개의 이벤트, 합이 1이 되어야 합니다)
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

                // 무기를 해당 위치에 스폰
                Instantiate(selectedItem, spawnPoints[i].position, Quaternion.identity);
            }
        }
    }

    IEnumerator ActivateSpecialEvent()
    {
        yield return new WaitForSeconds(180f); // 180초 기다리기

        // 확률에 따라 이벤트 선택
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
        specialWeaponImage.gameObject.SetActive(true); // UI 이미지 활성화

        // 즉시 무기를 4곳에 스폰
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(specialWeapon, spawnPoints[i].position, Quaternion.identity);
        }
    }
}
