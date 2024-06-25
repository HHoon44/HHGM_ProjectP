using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;

    // �� �����ۿ� ���� Ȯ�� (0�� 1 ������ ������ ���� 1�� �Ǿ�� �մϴ�)
    private float[] probabilities = { 0.5f, 0.3f, 0.2f };

    void Start()
    {
        StartCoroutine(SpawnItem());
    }

    IEnumerator SpawnItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            float randomValue = Random.value;
            GameObject selectedItem = null;

            if (randomValue < probabilities[0])
            {
                selectedItem = item1;
            }
            else if (randomValue < probabilities[0] + probabilities[1])
            {
                selectedItem = item2;
            }
            else
            {
                selectedItem = item3;
            }

            Instantiate(selectedItem, transform.position, Quaternion.identity);
        }
    }
}
