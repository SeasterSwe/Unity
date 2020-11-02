using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed = 3;

    public float startHealth = 5;
    private float currentHealth;

    public AudioClip hurtSound;
    private AudioSource audioSource;

    public GameObject hitParticle;
    void Start()
    {
        currentHealth = startHealth;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Patroll();
    }

    void Patroll()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void ChangeDirection()
    {
        speed *= -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;
        if (tag == "Arrow")
        {
            collision.gameObject.transform.parent = this.gameObject.transform;
            Destroy(collision.gameObject.GetComponent<TrailRenderer>());
            TakeDmg();
        }
        else if (tag == "Sword")
        {
            TakeDmg();
            foreach (ContactPoint2D hit in collision.contacts)
            {
                Vector2 hitPoint = hit.point;
                Instantiate(hitParticle, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);
            }

        }
        void TakeDmg()
        {
            currentHealth -= 1;
            if (currentHealth <= 0)
            {
                Die();
                return;
            }
            ChangeDirection();
            GameObject.FindGameObjectWithTag("BlinkManager").GetComponent<BlinkScript>().Blink(gameObject, Color.white, 1, 0.1f);
            audioSource.clip = hurtSound;
            audioSource.Play();
        }

        void Die()
        {
            Destroy(gameObject);
        }
    }
}
