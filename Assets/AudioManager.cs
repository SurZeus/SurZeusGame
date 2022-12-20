using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource mainAudio;
    public AudioSource miscAudio;
    public AudioSource inGameMusic;
    public AudioClip natureClip;
    public AudioClip mainMenu;
    public static AudioManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;  
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

   public IEnumerator playSoundWithDelay(AudioClip clip, float delay)
    {
       
        miscAudio.PlayOneShot(clip);
        yield return null;
    }
}
