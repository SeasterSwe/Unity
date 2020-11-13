﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBase : MonoBehaviour
{
    private void Start()
    {
        float duration = GetComponent<ParticleSystem>().duration;
        StartCoroutine(DestroyParticle(duration));
        if (GetComponent<AudioSource>() != null)
            PitchSound();
    }

    void PitchSound()
    {
        var audioS = GetComponent<AudioSource>();
        audioS.pitch = Random.Range(0.6f, 1.3f);
    }

    IEnumerator DestroyParticle(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
