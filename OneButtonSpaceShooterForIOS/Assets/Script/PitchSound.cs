using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchSound : MonoBehaviour
{
    public static void pitchSound(GameObject obj, float pitchMin = 0.6f, float pitchMax = 1.6f)
    {
        AudioSource audioSource = obj.GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(pitchMin, pitchMax);
    }
}
