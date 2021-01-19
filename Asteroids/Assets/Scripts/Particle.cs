using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public string ParticleName;
    public GameObject particle;
    private ParticleSystem particleSystem;

    public void PlayParticle(Vector3 playPos)
    {
        if (particle == null)
            particle = GameObject.Find(ParticleName);

        if (particleSystem == null)
            particleSystem = particle.GetComponent<ParticleSystem>();

        particle.transform.position = playPos;
        particleSystem.Play();
    }
}
