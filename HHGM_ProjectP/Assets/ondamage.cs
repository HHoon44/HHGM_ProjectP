using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ondamage : MonoBehaviour
{
    public int maxhealth;
    public int curhealth;
    Material mat;

    Rigidbody rigid;
    BoxCollider boxCollider;



    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponent<MeshRenderer>().material;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Punch")
        {
            curhealth -= 25;
            StartCoroutine(Damage());
        }
        if (other.tag == "Weapon")
        {
            curhealth -= 50;
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage() {

        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if(curhealth > 0)
        {
            mat.color = Color.white;

        }
        else
        {
            mat.color = Color.gray;
            Destroy(gameObject, 4);
        }
    }
}
