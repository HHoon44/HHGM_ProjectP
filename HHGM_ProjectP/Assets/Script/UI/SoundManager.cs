using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip[] audio_Clips;

    AudioSource bgm_player;
    AudioSource sfx_player;

     // public void PlaySound(string type)
    //{
      //  int index = 0;

//        switch (type)
  //      {
    //        case "hit": index = 0; break;
    //        case "run": index = 1; break;

      //  }
   // }
    void Awake()
    {
        bgm_player = GameObject.Find("BGM Player").GetComponent<AudioSource>();
        sfx_player = GameObject.Find("Sfx Player").GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
