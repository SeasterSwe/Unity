using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float bulletSpeed;
    OutOfBound outOfBound;
    TrailRenderer trailRenderer;
    Particle particle;
    public string hitEffekt = "Yeah";
    private void Awake()
    {
        outOfBound = gameObject.AddComponent<OutOfBound>();
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        particle = gameObject.AddComponent<Particle>();
        particle.ParticleName = hitEffekt;
    }

    private void OnEnable()
    {
        rb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
    }
    private void Update()
    {
        if (outOfBound.OutOfBoundsX() || outOfBound.OutOfBounds())
        {
            DisableBullet();
        }
    }
    private void DisableBullet()
    {
        rb.velocity = Vector3.zero;
        trailRenderer.Clear();
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Enemy")
        {
            particle.PlayParticle(transform.position);
            DisableBullet();
        }
    }
}
