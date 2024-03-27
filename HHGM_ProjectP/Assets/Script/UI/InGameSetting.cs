using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSetting : MonoBehaviour
{
    private bool state;
    public GameObject setting;
    void Start()
    {
        state = false;
        setting.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            if (state == false) {
                setting.SetActive(true);
                state = true;
            }
            else if(state == true)
            {
                setting.SetActive(false);
                state = false;
            }
        }
    }
}
