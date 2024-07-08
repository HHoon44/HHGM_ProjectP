using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon5;
    public GameObject weapon6;

    // 각 아이템에 대한 확률 (0과 1 사이의 값으로 합이 1이 되어야 합니다)
    private float[] probabilities = { 0.2f, 0.2f, 0.2f, 0.15f, 0.15f, 0.1f };

    void Start()
    {
        StartCoroutine(SpawnWeapon());
    }

    IEnumerator SpawnWeapon()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            float randomValue = Random.value;
            GameObject selectedItem = null;

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

            Instantiate(selectedItem, transform.position, Quaternion.identity);
        }
    }
}
