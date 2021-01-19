using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPlayerSound : MonoBehaviour
{
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip clip, bool pitchSound = true, float volume = 0.13f)
    {
        audioSource.clip = clip;
        if (pitchSound)
            PitchSound.pitchSound(gameObject, 0.8f, 1.6f);

        audioSource.volume = volume;
        audioSource.Play();
    }
}
