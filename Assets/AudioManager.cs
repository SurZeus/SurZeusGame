using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource mainAudio;
    public AudioSource inGameMusic;
    public AudioClip natureClip;
    public AudioClip mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playAudio(AudioClip audioClip)
    {
        mainAudio.clip = audioClip;
        mainAudio.Play();
    }
}
