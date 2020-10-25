using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
   private AudioSource audio;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlayPlayerSound(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }
}
